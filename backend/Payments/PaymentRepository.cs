namespace backend.Payments;

using backend.Database;
using Microsoft.EntityFrameworkCore;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentResponse?> GetPaymentByOrderIdAsync(int orderId)
    {   
        return await _context.Payments
        .Where(p => p.OrderId == orderId)
        .Select(p => new PaymentResponse
        {
            PaymentId = p.PaymentId,
            OrderId = p.OrderId,
            Method = p.Method,
            Amount = p.Amount,
            Currency = p.Currency
        })
        .FirstOrDefaultAsync(); 
    }

    public async Task<PaymentResponse> CreatePaymentAsync(PaymentRequest request)
    {
        var payment = new Payment
        {
            OrderId = request.OrderId,
            Method = request.Method,
            Amount = request.Amount,
            Currency = request.Currency
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return new PaymentResponse
        {
            PaymentId = payment.PaymentId,
            OrderId = payment.OrderId,
            Method = payment.Method,
            Amount = payment.Amount,
            Currency = payment.Currency
        };
    }

}