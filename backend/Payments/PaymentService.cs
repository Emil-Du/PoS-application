namespace backend.Payments;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;

    public PaymentService(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Payment?> GetPaymentsByOrderIdAsync(int orderId)
    {
        return await _repository.GetPaymentsByOrderIdAsync(orderId);
    }

    public async Task<Payment> CreateCashPaymentAsync(PaymentRequest request)
    {
        return await _repository.CreateCashPaymentAsync(request);
    }

    public async Task<Payment> CreateCardPaymentAsync(PaymentRequest request) //papildyt pagal stripe
    {
        return await _repository.CreateCardPaymentAsync(request);
    }
}
