import { useState, useEffect } from "react";
import "./Home.css";
import { orderService } from "../services/orderService";
import { paymentService } from "../services/paymentService";
import { useEmployee } from '../contexts/EmployeeContext';
import { useNavigate } from "react-router-dom";
import Navbar from "../components/NavBar";


interface Item {
    productId: number;
    name: string;
    unitPrice: number;
    currency: string;
    vatPercent: number;
}

interface OrderItem extends Item {
    quantity: number;
    itemId: number;
}

interface OrderTotals {
    subtotal: number;
    tax: number;
    total: number;
}

export default function Home() {
    const { employee } = useEmployee();
    const navigate = useNavigate();
    const [items, setItems] = useState<Item[]>([]);
    const [orderItems, setOrderItems] = useState<OrderItem[]>([]);
    const [selectedItem, setSelectedItem] = useState<Item | null>(null);
    const [quantity, setQuantity] = useState<number>(1);
    const [orderId, setOrderId] = useState<number | null>(null);
    const [creatingOrder, setCreatingOrder] = useState<boolean>(false);
    const [loadingItems, setLoadingItems] = useState<boolean>(true);
    const [paymentMethod, setPaymentMethod] = useState<string>("Card");
    const [processingPayment, setProcessingPayment] = useState<boolean>(false);
    const [paymentSuccess, setPaymentSuccess] = useState<boolean | null>(null);
    const [orderTotals, setOrderTotals] = useState<OrderTotals | null>(null);
    const [loadingTotals, setLoadingTotals] = useState<boolean>(false);
    const [cancellingOrder, setCancellingOrder] = useState<boolean>(false);


    useEffect(() => {
        if (!employee) {
            navigate("/");
        }
    }, [employee, navigate]);


    useEffect(() => {
        const savedOrder = sessionStorage.getItem('currentOrder');
        if (savedOrder) {
            try {
                const parsed = JSON.parse(savedOrder);
                setOrderId(parsed.orderId);
                setOrderItems(parsed.orderItems);
                setPaymentMethod(parsed.paymentMethod);
            } catch (err) {
                console.error('Failed to parse saved order:', err);
                sessionStorage.removeItem('currentOrder');
            }
        }
    }, []);


    useEffect(() => {
        if (orderId) {
            sessionStorage.setItem('currentOrder', JSON.stringify({
                orderId,
                orderItems,
                paymentMethod
            }));
        } else {
            sessionStorage.removeItem('currentOrder');
        }
    }, [orderId, orderItems, paymentMethod]);



    useEffect(() => {
        if (!employee) return;

        const fetchProducts = async () => {
            try {
                setLoadingItems(true);
                const products = await orderService.getProducts(employee.locationId);
                setItems(products);
            } catch (err) {
                console.error("Failed to fetch products:", err);
                alert("Failed to load products. Please refresh the page.");
            } finally {
                setLoadingItems(false);
            }
        };

        fetchProducts();
    }, [employee]);

    useEffect(() => {
        const fetchTotals = async () => {
            if (!orderId || orderItems.length === 0) {
                setOrderTotals(null);
                return;
            }

            try {
                setLoadingTotals(true);
                const totals = await orderService.getTotals(orderId);
                setOrderTotals(totals);
            } catch (err) {
                console.error("Failed to fetch totals:", err);
                setOrderTotals(null);
            } finally {
                setLoadingTotals(false);
            }
        };

        fetchTotals();
    }, [orderId, orderItems]);


    const createOrder = async () => {
        if (!employee) return;
        if (items.length === 0) {
            alert("No products available to determine currency.");
            return;
        }

        try {
            setCreatingOrder(true);

            const currency = items[0].currency;

            const result = await orderService.createOrder(
                employee.employeeId,
                0,                   // tip
                0,                   // discount
                0,                   // serviceCharge
                "Opened",
                currency
            );

            setOrderId(result.orderId);
        } catch (err) {
            console.error("Create order error:", err);
            alert("Failed to create order. Please try again.");
        } finally {
            setCreatingOrder(false);
        }
    };


    const addToOrder = async () => {
        if (!selectedItem || !orderId) return;

        try {
            const existingItem = orderItems.find(
                i => i.productId === selectedItem.productId
            );

            if (existingItem) {
                const backendItems = await orderService.getOrderItems(orderId);
                const backendItem = backendItems.find(
                    (item: any) => item.itemId === existingItem.itemId
                );

                const currentQuantity = backendItem ? backendItem.quantity : existingItem.quantity;
                const newQuantity = currentQuantity + quantity;

                await orderService.updateOrderItem(
                    orderId,
                    existingItem.itemId,
                    existingItem.currency,
                    newQuantity,
                    0,
                    existingItem.vatPercent
                );

                setOrderItems(prev =>
                    prev.map(i =>
                        i.itemId === existingItem.itemId
                            ? { ...i, quantity: newQuantity }
                            : i
                    )
                );
            } else {
                const result = await orderService.addOrderItem(
                    orderId,
                    selectedItem.productId,
                    selectedItem.currency,
                    quantity,
                    0,
                    selectedItem.vatPercent
                );

                setOrderItems(prev => [
                    ...prev,
                    {
                        ...selectedItem,
                        quantity,
                        itemId: result.itemId
                    }
                ]);
            }

            setSelectedItem(null);
            setQuantity(1);
        } catch (err) {
            console.error("Failed to add/update item:", err);
            alert("Failed to update order item.");
        }
    };

    const removeFromOrder = async (itemId: number) => {
        if (!orderId) return;

        try {
            await orderService.deleteOrderItem(orderId, itemId);
            setOrderItems(prev => prev.filter(item => item.itemId !== itemId));
        } catch (err) {
            console.error("Failed to remove item:", err);
            alert("Failed to remove item from order.");
        }
    };

    const handleCancelOrder = async () => {
        if (!orderId) return;

        const confirmCancel = window.confirm("Are you sure you want to cancel this order?");
        if (!confirmCancel) return;

        try {
            setCancellingOrder(true);
            await orderService.cancelOrder(orderId);

            setOrderId(null);
            setOrderItems([]);
            setOrderTotals(null);
            setPaymentMethod("Card");
            setPaymentSuccess(null);
        } catch (err) {
            console.error("Failed to cancel order:", err);
            alert("Failed to cancel order. Please try again.");
        } finally {
            setCancellingOrder(false);
        }
    };

    const handleCheckout = async () => {
        if (!orderId || orderItems.length === 0 || !orderTotals) return;

        try {
            setProcessingPayment(true);
            setPaymentSuccess(null);

            const currency = orderItems[0]?.currency || "Eur";

            await paymentService.createPayment(
                orderId,
                orderTotals.total,
                paymentMethod,
                currency
            );

            await orderService.closeOrder(orderId);

            setPaymentSuccess(true);

            setTimeout(() => {
                setOrderId(null);
                setOrderItems([]);
                setPaymentSuccess(null);
                setPaymentMethod("Card");
                setOrderTotals(null);
            }, 2000);
        } catch (err) {
            console.error("Payment failed:", err);
            setPaymentSuccess(false);
        } finally {
            setProcessingPayment(false);
        }
    };

    return (
        <div className="layout">
            <Navbar />

            <div className="module">
                <div className="module-header">
                    <p>Add items</p>
                </div>

                <div className="columns">
                    <div className="column column-large">
                        <p>ITEMS</p>

                        <div className="scroll-container">
                            {loadingItems ? (
                                <div className="empty-state">Loading products...</div>
                            ) : items.length === 0 ? (
                                <div className="empty-state">No products available</div>
                            ) : (
                                items.map(item => (
                                    <button
                                        key={item.productId}
                                        className="list-button"
                                        onClick={() => setSelectedItem(item)}
                                        disabled={!orderId}
                                    >
                                        <div>{item.name}</div>
                                        <div>
                                            {item.currency} {item.unitPrice.toFixed(2)}
                                        </div>
                                    </button>
                                ))
                            )}
                        </div>
                    </div>

                    <div className="column column-small">
                        <p>ORDER</p>

                        {!orderId ? (
                            <button
                                className="create-order-button"
                                onClick={createOrder}
                                disabled={creatingOrder}
                            >
                                <span className="plus-sign">+</span>
                                <span>
                                    {creatingOrder ? "Creating..." : "Create Order"}
                                </span>
                            </button>
                        ) : (
                            <>
                                <div className="order-info">
                                    <small>Order ID: {orderId}</small>
                                </div>

                                <div className="scroll-container">
                                    {orderItems.length === 0 && (
                                        <div className="empty-state">No items added</div>
                                    )}

                                    {orderItems.map(item => (
                                        <div key={item.itemId} className="order-row">
                                            <div className="order-row-name">{item.name}</div>
                                            <div className="order-row-quantity">x{item.quantity}</div>
                                            <div className="order-row-price">
                                                {item.currency} {(item.unitPrice * item.quantity).toFixed(2)}
                                            </div>
                                            <button
                                                className="remove-button"
                                                onClick={() => removeFromOrder(item.itemId)}
                                                title="Remove item"
                                            >
                                                ×
                                            </button>
                                        </div>
                                    ))}
                                </div>

                                {orderTotals && (
                                    <>
                                        <div className="order-subtotals">
                                            <div>
                                                <span>Subtotal:</span>
                                                <span>{orderItems[0]?.currency || "Eur"} {orderTotals.subtotal.toFixed(2)}</span>
                                            </div>
                                            <div>
                                                <span>Tax:</span>
                                                <span>{orderItems[0]?.currency || "Eur"} {orderTotals.tax.toFixed(2)}</span>
                                            </div>
                                        </div>
                                        <div className="order-total">
                                            <span>Total</span>
                                            <span>{orderItems[0]?.currency || "Eur"} {orderTotals.total.toFixed(2)}</span>
                                        </div>
                                    </>
                                )}

                                {loadingTotals && (
                                    <div className="order-total">
                                        <span>Loading totals...</span>
                                    </div>
                                )}

                                <div className="payment-method">
                                    <label htmlFor="payment-select">Payment Method:</label>
                                    <select
                                        id="payment-select"
                                        value={paymentMethod}
                                        onChange={(e) => setPaymentMethod(e.target.value)}
                                        disabled={processingPayment}
                                    >
                                        <option value="Card">Card</option>
                                        <option value="Cash">Cash</option>
                                    </select>
                                </div>

                                {paymentSuccess === true && (
                                    <div className="payment-status success">
                                        Payment successful!
                                    </div>
                                )}

                                {paymentSuccess === false && (
                                    <div className="payment-status error">
                                        Payment failed. Please try again.
                                    </div>
                                )}

                                <div className="button-group">
                                    <button
                                        className="confirmation-button"
                                        onClick={handleCheckout}
                                        disabled={orderItems.length === 0 || processingPayment || !orderTotals || cancellingOrder}
                                    >
                                        {processingPayment ? "Processing..." : "Checkout"}
                                    </button>

                                    <button
                                        className="cancel-order-button"
                                        onClick={handleCancelOrder}
                                        disabled={cancellingOrder || processingPayment}
                                    >
                                        {cancellingOrder ? "Cancelling..." : "Cancel Order"}
                                    </button>
                                </div>
                            </>
                        )}
                    </div>
                </div>
            </div>

            {selectedItem && (
                <div className="modal-overlay">
                    <div className="modal">
                        <p className="modal-title">{selectedItem.name}</p>
                        <p className="modal-price">
                            Price: {selectedItem.currency} {selectedItem.unitPrice.toFixed(2)}
                        </p>

                        <input
                            type="number"
                            min={1}
                            value={quantity}
                            onChange={e => setQuantity(Number(e.target.value))}
                        />

                        <div className="modal-actions">
                            <button onClick={addToOrder}>Add</button>
                            <button onClick={() => setSelectedItem(null)}>Cancel</button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}