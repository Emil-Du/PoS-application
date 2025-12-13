namespace backend.Payments;

public interface IPaymentRepository
{
    Task<Payment?> GetPaymentsByOrderIdAsync(int orderId);
    Task<Payment> CreatePaymentAsync(PaymentRequest request);

}
