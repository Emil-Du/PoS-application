const API_BASE_URL = 'http://localhost:5041';

export const paymentService = {
    createPayment: async (orderId: number, amount: number, method: string, currency: string) => {
        const response = await fetch(`${API_BASE_URL}/api/Payment/payment`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                orderId,
                amount,
                method,
                currency,
            }),
            credentials: 'include'
        });

        if (!response.ok) {
            throw new Error('Failed to create payment');
        }

        return await response.json();

    }
};
