namespace backend.Refunds;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = Roles.Roles.SuperAdmin + "," + Roles.Roles.Manager + "," + Roles.Roles.Employee)]
public class RefundController : ControllerBase
{

    private readonly IRefundService _service;
    private readonly ILogger<RefundController> _logger;

    public RefundController(IRefundService service, ILogger<RefundController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> RefundPayment([FromBody] RefundRequest request)
    {
        var success = await _service.RefundPaymentByIdAsync(request);

        if (success == null)
        {
            _logger.LogWarning("Refund failed for Payment {PaymentId}", request.PaymentId);
            return NotFound();
        }

        return NoContent();
    }
}
