namespace backend.Refunds;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class RefundController : ControllerBase
{

    private readonly IRefundRepository _refundRepository;
    private readonly ILogger<RefundController> _logger;

    public RefundController(IRefundRepository refundRepository, ILogger<RefundController> logger)
    {
        _refundRepository = refundRepository;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> RefundPayment([FromBody] RefundRequest request)
    {
        var success = await _refundRepository.RefundByPaymentIdAsync(request.PaymentId, request.Reason);

        if (!success)
        {
            _logger.LogWarning("Refund failed for Payment {PaymentId}", request.PaymentId);
            return NotFound();
        }

        _logger.LogInformation("Refund successful for Payment {PaymentId}", request.PaymentId);
        return NoContent();
    }
}
