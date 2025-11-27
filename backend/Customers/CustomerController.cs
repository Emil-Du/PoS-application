using Microsoft.AspNetCore.Mvc;

namespace backend.Customers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService service, ILogger<CustomerController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<CustomerResponse>>> GetCustomers([FromQuery] CustomerQuery query)
        {
            var result = await _service.GetCustomersAsync(query);
            return Ok(result);
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerById([FromRoute] int customerId)
        {
            var response = await _service.GetCustomerByIdAsync(customerId);
            if (response == null)
            {
                _logger.LogWarning($"Customer with ID {customerId} not found.");
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResponse>> CreateCustomer([FromBody] CustomerRequest customerRequest)
        {
            if (customerRequest == null)
            {
                _logger.LogWarning("Failed to create customer: invalid request.");
                return BadRequest();
            }

            var createdCustomer = await _service.CreateCustomerAsync(customerRequest);

            return CreatedAtAction(
                nameof(GetCustomerById),
                new { customerId = createdCustomer.CustomerId },
                createdCustomer
            );
        }

        [HttpPatch("{customerId}")]
        public async Task<IActionResult> UpdateCustomerById([FromRoute] int customerId, [FromBody] CustomerRequest customerRequest)
        {
            if (customerRequest == null)
            {
                _logger.LogWarning("Failed to update customer: invalid request.");
                return BadRequest();
            }

            var updated = await _service.UpdateCustomerByIdAsync(customerId, customerRequest);
            if (!updated)
            {
                _logger.LogWarning($"Customer with ID {customerId} not found.");
                return NotFound();
            }

            return NoContent();
        }
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomerById([FromRoute] int customerId)
        {
            var deleted = await _service.DeleteCustomerByIdAsync(customerId);
            if (!deleted)
            {
                _logger.LogWarning($"Customer with ID {customerId} not found.");
                return NotFound();
            }

            return NoContent();
        }

    }
}
