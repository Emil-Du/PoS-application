namespace backend.Refunds;

public interface IRefundRepository
{
    Task<bool> RefundByPaymentIdAsync(int paymentId, string reason);
}
