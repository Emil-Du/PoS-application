import { useState } from "react";
import "./Home.css";

interface Item {
    id: number;
    name: string;
    price: number;
}

interface OrderItem extends Item {
    quantity: number;
}

const itemsFromDb: Item[] = [
    { id: 1, name: "Coffee", price: 2.5 },
    { id: 2, name: "Tea", price: 2.0 },
    { id: 3, name: "Sandwich", price: 5.0 },
    { id: 4, name: "Cake", price: 3.5 },
    { id: 5, name: "Croissant", price: 3.0 },
    { id: 6, name: "Muffin", price: 2.75 },
    { id: 7, name: "Bagel", price: 2.25 },
    { id: 8, name: "Latte", price: 4.0 },
    { id: 9, name: "Cappuccino", price: 4.5 }
];

export default function Home() {
    const [orderItems, setOrderItems] = useState<OrderItem[]>([]);
    const [selectedItem, setSelectedItem] = useState<Item | null>(null);
    const [quantity, setQuantity] = useState<number>(1);

    const addToOrder = () => {
        if (!selectedItem) return;

        setOrderItems(prev => {
            const existing = prev.find(i => i.id === selectedItem.id);

            if (existing) {
                return prev.map(i =>
                    i.id === selectedItem.id
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
        setOrderItems(prev => prev.filter(item => item.id !== itemId));
    };

    const total = orderItems.reduce(
        (sum, item) => sum + item.price * item.quantity,
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
                            {itemsFromDb.map(item => (
                                <button
                                    key={item.id}
                                    className="list-button"
                                    onClick={() => setSelectedItem(item)}
                                >
                                    <div>{item.name}</div>
                                    <div>${item.price.toFixed(2)}</div>
                                </button>
                            ))}
                        </div>
                    </div>

                    {/* ORDER */}
                    <div className="column column-small">
                        <p>ORDER</p>

                        <div className="scroll-container">
                            {orderItems.length === 0 && (
                                <div className="empty-state">No items added</div>
                            )}

                            {orderItems.map(item => (
                                <div key={item.id} className="order-row">
                                    <div className="order-row-name">{item.name}</div>
                                    <div className="order-row-quantity">x{item.quantity}</div>
                                    <div className="order-row-price">${(item.price * item.quantity).toFixed(2)}</div>
                                    <button
                                        className="remove-button"
                                        onClick={() => removeFromOrder(item.id)}
                                        title="Remove item"
                                    >
                                        ×
                                    </button>
                                </div>
                            ))}
                        </div>

                        <div className="order-total">
                            <span>Total</span>
                            <span>${total.toFixed(2)}</span>
                        </div>

                        <button
                            className="confirmation-button"
                            disabled={orderItems.length === 0}
                        >
                            Checkout
                        </button>
                    </div>
                </div>
            </div>

            {/* MODAL */}
            {selectedItem && (
                <div className="modal-overlay">
                    <div className="modal">
                        <p className="modal-title">{selectedItem.name}</p>
                        <p className="modal-price">
                            Price: ${selectedItem.price.toFixed(2)}
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