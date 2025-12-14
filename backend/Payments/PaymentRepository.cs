namespace backend.Payments;

using backend.Database;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Payment?> GetPaymentsByOrderIdAsync(int orderId)
    {
        return await _context.Payments.FindAsync(orderId);
    }

    public async Task<Payment> CreatePaymentAsync(PaymentRequest request)
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

        return payment;
    }

}