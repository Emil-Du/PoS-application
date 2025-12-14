import { useState, useEffect } from "react";
import "./Home.css";
import { orderService } from "../services/orderService";

interface Item {
    productId: number;
    name: string;
    unitPrice: number;
    currency: string;
    vatPercent: number;
}

interface OrderItem extends Item {
    quantity: number;
}

export default function Home() {
    const [items, setItems] = useState<Item[]>([]);
    const [orderItems, setOrderItems] = useState<OrderItem[]>([]);
    const [selectedItem, setSelectedItem] = useState<Item | null>(null);
    const [quantity, setQuantity] = useState<number>(1);
    const [orderId, setOrderId] = useState<number | null>(null);
    const [creatingOrder, setCreatingOrder] = useState<boolean>(false);
    const [loadingItems, setLoadingItems] = useState<boolean>(true);

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                setLoadingItems(true);
                const products = await orderService.getProducts();
                setItems(products);
            } catch (err) {
                console.error('Failed to fetch products:', err);
                alert('Failed to load products. Please refresh the page.');
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
            setOrderId(result.orderId || result.id);

        } catch (err) {
            alert('Failed to create order. Please try again.');
            console.error('Create order error:', err);
        } finally {
            setCreatingOrder(false);
        }
    };

    const addToOrder = () => {
        if (!selectedItem) return;

        setOrderItems(prev => {
            const existing = prev.find(i => i.productId === selectedItem.productId);

            if (existing) {
                return prev.map(i =>
                    i.productId === selectedItem.productId
                        ? { ...i, quantity: i.quantity + quantity }
                        : i
                );
            }

            return [...prev, { ...selectedItem, quantity }];
        });

        setSelectedItem(null);
        setQuantity(1);
    };

    const removeFromOrder = (itemId: number) => {
        setOrderItems(prev => prev.filter(item => item.productId !== itemId));
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
                    {/* ITEMS */}
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
                                        <div>{item.currency} {item.unitPrice.toFixed(2)}</div>
                                    </button>
                                ))
                            )}
                        </div>
                    </div>

                    {/* ORDER */}
                    <div className="column column-small">
                        <p>ORDER</p>

                        {!orderId ? (
                            <button
                                className="create-order-button"
                                onClick={createOrder}
                                disabled={creatingOrder}
                            >
                                <span className="plus-sign">+</span>
                                <span>{creatingOrder ? 'Creating...' : 'Create Order'}</span>
                            </button>
                        ) : (
                            <>
                                <div className="scroll-container">
                                    {orderItems.length === 0 && (
                                        <div className="empty-state">No items added</div>
                                    )}

                                    {orderItems.map(item => (
                                        <div key={item.productId} className="order-row">
                                            <div className="order-row-name">{item.name}</div>
                                            <div className="order-row-quantity">x{item.quantity}</div>
                                            <div className="order-row-price">
                                                {item.currency} {(item.unitPrice * item.quantity).toFixed(2)}
                                            </div>
                                            <button
                                                className="remove-button"
                                                onClick={() => removeFromOrder(item.productId)}
                                                title="Remove item"
                                            >
                                                ×
                                            </button>
                                        </div>
                                    ))}
                                </div>

                                <div className="order-total">
                                    <span>Total</span>
                                    <span>{orderItems[0]?.currency || 'EUR'} {total.toFixed(2)}</span>
                                </div>

                                <button
                                    className="confirmation-button"
                                    disabled={orderItems.length === 0}
                                >
                                    Checkout
                                </button>
                            </>
                        )}
                    </div>
                </div>
            </div>

            {/* MODAL */}
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
                            onChange={(e) => setQuantity(Number(e.target.value))}
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