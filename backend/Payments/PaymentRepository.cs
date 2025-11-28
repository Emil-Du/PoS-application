namespace backend.Payments;

using backend.Database;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Payment> GetPaymentsByOrderIdAsync(int orderId)
    {
        return await _context.Payments.FindAsync(orderId); 
    }

    public async Task<Payment> CreateCashPaymentAsync(PaymentRequest request)
    {
        var payment = new Payment
        {
            OrderId = request.OrderId,
            Method = "cash",
            Amount = request.Amount,
            Currency = request.Amount.Currency
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return payment;
    }

    public async Task<Payment> CreateCardPaymentAsync(PaymentRequest request, string? stripeChargeId = null) //papildyt pagal stripe
    {
        var payment = new Payment
        {
            OrderId = request.OrderId,
            Method = "card",
            Amount = request.Amount,
            Currency = request.Amount.Currency,
            StripeChargeId = stripeChargeId
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return payment;
    }
}