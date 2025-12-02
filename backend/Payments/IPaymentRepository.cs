namespace backend.Payments;

public interface IPaymentRepository
{
    Task<Payment?> GetPaymentsByOrderIdAsync(int orderId);
    Task<Payment> CreateCashPaymentAsync(PaymentRequest request);
    Task<Payment> CreateCardPaymentAsync(PaymentRequest request, string? stripeChargeId = null); // papildyt pagal stripe
}
