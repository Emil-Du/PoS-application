namespace backend.Payments;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class PaymentController : ControllerBase
{
    private readonly IPaymentService _service;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(IPaymentService service, ILogger<PaymentController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("order/{orderId}/payment")]
    public async Task<IActionResult> GetPaymentByOrder(int orderId)
    {
        var payments = await _service.GetPaymentByOrderIdAsync(orderId);

        if (payments == null)
        {
            _logger.LogWarning("No payments found for Order {OrderId}", orderId);
            return NotFound();
        }

        return Ok(payments);
    }

    [HttpPost("payment")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest request)
    {
        var payment = await _service.CreatePaymentAsync(request);

        return Ok(payment);
    }

}
