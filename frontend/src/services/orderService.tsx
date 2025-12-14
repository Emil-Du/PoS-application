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
        const locationId = 1; // Hardcoded for now, will be from session later

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
    }
};