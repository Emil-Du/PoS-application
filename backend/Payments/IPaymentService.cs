namespace backend.Payments;

public interface IPaymentService
{
    Task<PaymentResponse?> GetPaymentByOrderIdAsync(int orderId);
    Task<PaymentResponse> CreatePaymentAsync(PaymentRequest request);
}
