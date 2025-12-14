namespace backend.Payments;

public interface IPaymentService
{
    Task<Payment?> GetPaymentsByOrderIdAsync(int orderId);
    Task<Payment> CreatePaymentAsync(PaymentRequest request);
}
