namespace backend.Payments;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]

public class PaymentController : ControllerBase
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(IPaymentRepository paymentRepository, ILogger<PaymentController> logger)
    {
        _paymentRepository = paymentRepository;
        _logger = logger;
    }

    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetPaymentsByOrder(int orderId)
    {
        var payments = await _paymentRepository.GetPaymentsByOrderIdAsync(orderId);

        _logger.LogInformation($"Fetched payments for Order ID {orderId}");
        return Ok(payments);
    }

    [HttpGet("{paymentId}")]
    public async Task<IActionResult> GetPaymentById(int paymentId)
    {
        var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);
        if (payment == null) return NotFound();
        return Ok(payment);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest request)   
    {
        var payment = await _paymentRepository.CreatePaymentAsync(request);
        _logger.LogInformation("Payment created with ID {PaymentId} for Order {OrderId}", payment.PaymentId, payment.OrderId);
        return CreatedAtAction(nameof(GetPaymentById), new { id = payment.PaymentId }, payment);
    }
}
