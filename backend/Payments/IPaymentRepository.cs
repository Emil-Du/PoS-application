namespace backend.Payments;

public interface IPaymentRepository
{
    Task<PaymentResponse?> GetPaymentByOrderIdAsync(int orderId);
    Task<PaymentResponse> CreatePaymentAsync(PaymentRequest request);

}
