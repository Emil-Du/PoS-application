namespace backend.Payments;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;

    public PaymentService(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaymentResponse?> GetPaymentByOrderIdAsync(int orderId)
    {
        return await _repository.GetPaymentByOrderIdAsync(orderId);
    }

    public async Task<PaymentResponse> CreatePaymentAsync(PaymentRequest request)
    {
        return await _repository.CreatePaymentAsync(request);
    }

}
