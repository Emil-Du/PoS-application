namespace backend.Payments;

public interface IPaymentRepository
{
    Task<Payment?> GetPaymentsByOrderIdAsync(int orderId);
    Task<Payment?> GetPaymentByIdAsync(int paymentId);
    Task<Payment> CreatePaymentAsync(PaymentRequest request);
}
