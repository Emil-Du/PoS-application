namespace backend.Payments;

using backend.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = Roles.Roles.SuperAdmin + "," + Roles.Roles.Manager + "," + Roles.Roles.Employee)]
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
        try
        {
            var payment = await _service.CreatePaymentAsync(request);

            return Ok(payment);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (OrderNotOpenException)
        {
            return BadRequest("Order is not open.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
