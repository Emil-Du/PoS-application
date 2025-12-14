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
        //turetu but Charge table
        var payment = await _context.Payments.FindAsync(refundRequest.PaymentId);
        if (payment == null)
        {
            throw new Exception("Payment not found");
        }

        //Business logic for changing Order status to refunded
        var order = await _context.Orders.FindAsync(payment.OrderId) ?? throw new Exception(); //add NotFoundException

        order.Status = Orders.OrderStatus.Refunded;

        await _context.Orders.AddAsync(order);

        return refundRequest;
    }
}
