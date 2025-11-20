using Microsoft.AspNetCore.Mvc;

namespace backend.Customers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
        {
            var response = await _repository.GetCustomersAsync(page, pageSize);

            return Ok(response);
        }
    }
}
