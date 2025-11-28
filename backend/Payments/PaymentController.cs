namespace backend.Payments;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]

public class PaymentController : ControllerBase
{
    private readonly IPaymentService _service;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(IPaymentService service, ILogger<PaymentController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetPaymentsByOrder(int orderId)
    {
        var payments = await _service.GetPaymentsByOrderIdAsync(orderId);

        if (payments == null)
        {
            _logger.LogWarning("No payments found for Order {OrderId}", orderId);
            return NotFound();
        }

        return Ok(payments);
    }

     [HttpPost("payment/cash")]
    public async Task<IActionResult> CreateCashPayment([FromBody] PaymentRequest request)
    {
        var payment = await _service.CreateCashPaymentAsync(request);

        return Ok(payment);
    }

    [HttpPost("payment/card")]
    public async Task<IActionResult> CreateCardPayment([FromBody] PaymentRequest request)
    {
        var payment = await _service.CreateCardPaymentAsync(request);
        
        return Ok(payment);
    }
}
