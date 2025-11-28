namespace backend.Refunds;

public interface IRefundRepository
{
    Task<RefundRequest> RefundPaymentByIdAsync(RefundRequest refundRequest);
}
