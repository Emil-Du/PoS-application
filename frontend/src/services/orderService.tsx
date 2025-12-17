const API_BASE_URL = 'http://localhost:5041';

export interface OrderQuery {
  page?: number;
  pageSize?: number;
  status?: string;
}

export interface PaginatedResponse<T> {
  items: T[];
  totalCount: number;
}

export const orderService = {
    getOrders: async (locationId: number) => {
        const response = await fetch(`${API_BASE_URL}/api/orders?locationId=${locationId}`, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' },
        credentials: 'include'
        });

        if (!response.ok) {
        throw new Error('Failed to fetch orders');
        }

        return await response.json();
    },

    getOrderById: async (orderId: number) => {
        const response = await fetch(`${API_BASE_URL}/api/orders/${orderId}`, {
            method: "GET",
            headers: { "Content-Type": "application/json" },
            credentials: "include",
        });

        if (!response.ok) {
            throw new Error("Failed to fetch order");
        }

        return response.json();
    },

    updateOrder: async (
        orderId: number,
        request: { status?: string; discount?: number; tip?: number; serviceCharge?: number }
        ) => {
        const res = await fetch(`${API_BASE_URL}/api/orders/${orderId}`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            credentials: "include",
            body: JSON.stringify(request),
        });

        if (!res.ok) {
            const msg = await res.text();
            throw new Error(msg || "Failed to update order");
        }

        return res.json();
    },

    createOrder: async (
        operatorId: number,
        tip: number,
        discount: number,
        serviceCharge: number,
        status: string,
        currency: string
    ) => {
        const response = await fetch(`${API_BASE_URL}/api/orders`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include',
            body: JSON.stringify({
                operatorId,
                tip,
                discount,
                serviceCharge,
                status,
                currency
            })
        });

        if (!response.ok) {
            throw new Error('Failed to create order');
        }

        return await response.json();
    },

    getProducts: async (locationId: number) => {
        const response = await fetch(`${API_BASE_URL}/api/Product/${locationId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include'
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
                credentials: 'include',
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
                credentials: 'include',
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
                },
                credentials: 'include'
            }
        );

        if (!response.ok) {
            throw new Error('Failed to delete order item');
        }

        return response.status !== 204 ? await response.json() : null;
    },

    getOrderItems: async (orderId: number) => {
        const response = await fetch(
            `${API_BASE_URL}/api/orders/${orderId}/items`,
            {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
                credentials: 'include'
            }
        );

        if (!response.ok) {
            throw new Error('Failed to fetch order items');
        }

        return await response.json();
    },

    closeOrder: async (orderId: number) => {
        const response = await fetch(`${API_BASE_URL}/api/orders/${orderId}/close`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include',
        });

        if (!response.ok) {
            throw new Error('Failed to close order');
        }

        return null;
    },

    cancelOrder: async (orderId: number) => {
        const response = await fetch(`${API_BASE_URL}/api/orders/${orderId}/cancel`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include',
        });

        if (!response.ok) {
            throw new Error('Failed to cancel order');
        }

        return null;
    },

    getTotals: async (orderId: number) => {
        const response = await fetch(
            `${API_BASE_URL}/api/orders/${orderId}/taxes`,
            {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
                credentials: 'include',
            }
        );

        if (!response.ok) {
            throw new Error('Failed to fetch order totals');
        }

        return await response.json();
    }
};
