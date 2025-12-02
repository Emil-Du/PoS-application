using backend.Database;

namespace backend.Refunds;

public class RefundRepository : IRefundRepository
{
    private readonly AppDbContext _context;

    public RefundRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<RefundRequest> RefundPaymentByIdAsync(RefundRequest refundRequest)
    {

            var payment = await _context.Payments.FindAsync(refundRequest.PaymentId);
            if (payment == null)
            {
                throw new Exception("Payment not found");
            }
            
            return refundRequest;
    }
}
