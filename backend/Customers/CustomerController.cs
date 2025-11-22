using Microsoft.AspNetCore.Mvc;

namespace backend.Customers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerRepository repository, ILogger<CustomerController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
        {
            var response = await _repository.GetCustomersAsync(page, pageSize);
            _logger.LogInformation($"Fetched {pageSize} entries, from page {page}");

            return Ok(response);
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<Customer>> GetCustomerById([FromRoute] int customerId)
        {
            var response = await _repository.GetCustomerByIdAsync(customerId);
            if (response == null)
            {
                _logger.LogWarning($"Customer with ID {customerId} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Fetched customer with ID {customerId}");
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

            var createdCustomer = await _repository.CreateCustomerAsync(customerDTO);

            _logger.LogInformation($"Created new customer with ID {createdCustomer.CustomerId}");

            return CreatedAtAction(
                nameof(GetCustomerById),
                new { customerId = createdCustomer.CustomerId },
                createdCustomer
            );
        }

        [HttpPatch("{customerId}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int customerId, [FromBody] CustomerDTO customerDTO)
        {
            if (customerDTO == null)
            {
                _logger.LogWarning("CustomerDTO is null.");
                return BadRequest();
            }

            var updated = await _repository.UpdateCustomerAsync(customerId, customerDTO);
            if (!updated)
            {
                _logger.LogWarning($"Customer with ID {customerId} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Updated customer with ID {customerId}");
            return NoContent();
        }
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomerById([FromRoute] int customerId)
        {
            var deleted = await _repository.DeleteCustomerByIdAsync(customerId);
            if (!deleted)
            {
                _logger.LogWarning($"Customer with ID {customerId} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Deleted customer with ID {customerId}");
            return NoContent();
        }

    }
}
