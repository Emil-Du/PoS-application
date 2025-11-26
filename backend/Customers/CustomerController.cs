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
        public async Task<ActionResult<PaginatedResponse<Customer>>> GetCustomers([FromQuery] int page = 1, [FromQuery] int pageSize = 25, [FromQuery] string? search = null)
        {
            var result = await _service.GetCustomersAsync(page, pageSize, search);
            return Ok(result);
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<Customer>> GetCustomerById([FromRoute] int customerId)
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
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null)
            {
                _logger.LogWarning("CustomerDTO is null.");
                return BadRequest();
            }

            var createdCustomer = await _service.CreateCustomerAsync(customerDTO);

            return CreatedAtAction(
                nameof(GetCustomerById),
                new { customerId = createdCustomer.CustomerId },
                createdCustomer
            );
        }

        [HttpPatch("{customerId}")]
        public async Task<IActionResult> UpdateCustomerById([FromRoute] int customerId, [FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null)
            {
                _logger.LogWarning("CustomerDTO is null.");
                return BadRequest();
            }

            var updated = await _service.UpdateCustomerByIdAsync(customerId, customerDTO);
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
