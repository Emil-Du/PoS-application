import { useState, useEffect } from "react";
import "./Home.css";
import { orderService } from "../services/orderService";
import { paymentService } from "../services/paymentService";

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

export default function Home() {
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

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                setLoadingItems(true);
                const products = await orderService.getProducts();
                setItems(products);
            } catch (err) {
                console.error("Failed to fetch products:", err);
                alert("Failed to load products. Please refresh the page.");
            } finally {
                setLoadingItems(false);
            }
        };

        fetchProducts();
    }, []);

    const createOrder = async () => {
        try {
            setCreatingOrder(true);
            const result = await orderService.createOrder();
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

    const handleCheckout = async () => {
        if (!orderId || orderItems.length === 0) return;

        try {
            setProcessingPayment(true);
            setPaymentSuccess(null);

            const currency = orderItems[0]?.currency || "Eur";

            await paymentService.createPayment(
                orderId,
                total,
                paymentMethod,
                currency
            );

            setPaymentSuccess(true);

            setTimeout(() => {
                setOrderId(null);
                setOrderItems([]);
                setPaymentSuccess(null);
                setPaymentMethod("Card");
            }, 2000);
        } catch (err) {
            console.error("Payment failed:", err);
            setPaymentSuccess(false);
        } finally {
            setProcessingPayment(false);
        }
    };

    const total = orderItems.reduce(
        (sum, item) => sum + item.unitPrice * item.quantity,
        0
    );

    return (
        <div className="layout">
            <div className="sidebar-placeholder"></div>

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

                                <div className="order-total">
                                    <span>Total</span>
                                    <span>{orderItems[0]?.currency || "Eur"} {total.toFixed(2)}</span>
                                </div>

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
                                        ✓ Payment successful!
                                    </div>
                                )}

                                {paymentSuccess === false && (
                                    <div className="payment-status error">
                                        ✗ Payment failed. Please try again.
                                    </div>
                                )}

                                <button
                                    className="confirmation-button"
                                    onClick={handleCheckout}
                                    disabled={orderItems.length === 0 || processingPayment}
                                >
                                    {processingPayment ? "Processing..." : "Checkout"}
                                </button>
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