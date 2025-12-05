namespace backend.Refunds;

public interface IRefundService
{
    Task<RefundRequest> RefundPaymentByIdAsync(RefundRequest request);
}
