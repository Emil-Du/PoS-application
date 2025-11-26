using backend.Database;

namespace backend.Refunds;

public class RefundRepository : IRefundRepository
{
    private readonly AppDbContext _context;

    public RefundRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<bool> RefundByPaymentIdAsync(int paymentId, string reason)
    {
        var payment = await _context.Payments.FindAsync(paymentId);
        if (payment == null)
        {
            return false;
        }

        // pakeicia order status i refunded

        await _context.SaveChangesAsync();

        return true;
    }
}
