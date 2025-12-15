const API_BASE_URL = 'http://localhost:5041';

export const orderService = {
    createOrder: async () => {
        const response = await fetch(`${API_BASE_URL}/api/orders`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                operatorId: 1,
                tip: 0,
                discount: 0,
                serviceCharge: 0,
                status: "Opened",
                currency: "Eur"
            })
        });

        if (!response.ok) {
            throw new Error('Failed to create order');
        }

        return await response.json();
    },

    getProducts: async () => {
        const locationId = 1;

        const response = await fetch(`${API_BASE_URL}/api/Product/${locationId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            }
        });

        if (!response.ok) {
            throw new Error('Failed to fetch products');
        }

        return await response.json();
    },

    addOrderItem: async (
        orderId: number,
        productId: number,
        currency: string,
        quantity: number,
        discount: number,
        vatPercentage: number
    ) => {
        const response = await fetch(
            `${API_BASE_URL}/api/orders/${orderId}/items`,
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    productId,
                    currency,
                    quantity,
                    discount,
                    vatPercentage
                })
            }
        );

        if (!response.ok) {
            throw new Error('Failed to add item to order');
        }

        return await response.json();
    },

    updateOrderItem: async (
        orderId: number,
        itemId: number,
        currency: string,
        quantity: number,
        discount: number,
        vatPercentage: number
    ) => {
        const response = await fetch(
            `${API_BASE_URL}/api/orders/${orderId}/items/${itemId}`,
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    currency,
                    quantity,
                    discount,
                    vatPercentage
                })
            }
        );

        if (!response.ok) {
            throw new Error('Failed to update order item');
        }

        return null;
    },

    deleteOrderItem: async (orderId: number, itemId: number) => {
        const response = await fetch(
            `${API_BASE_URL}/api/orders/${orderId}/items/${itemId}`,
            {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                }
            }
        );

        if (!response.ok) {
            throw new Error('Failed to delete order item');
        }

        return response.status !== 204 ? await response.json() : null;
    },

    getOrderItems: async (orderId: number) => {
        const response = await fetch(`${API_BASE_URL}/api/orders/${orderId}/items`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            }
        });

        if (!response.ok) {
            throw new Error('Failed to fetch order items');
        }

        return await response.json();
    }
};
