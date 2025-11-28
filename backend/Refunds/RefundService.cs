namespace backend.Refunds;
public class RefundService : IRefundService
{
    private readonly IRefundRepository _repository;

    public RefundService(IRefundRepository repository)
    {
        _repository = repository;
    }

    public async Task<RefundRequest> RefundPaymentByIdAsync(RefundRequest request)
    
    {
        return await _repository.RefundPaymentByIdAsync(request);
    }
}
