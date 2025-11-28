namespace backend.Payments;

public interface IPaymentService
{
    Task<Payment?> GetPaymentsByOrderIdAsync(int orderId);
    Task<Payment> CreateCashPaymentAsync(PaymentRequest request);
    Task<Payment> CreateCardPaymentAsync(PaymentRequest request);
}
