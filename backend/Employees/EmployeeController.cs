using backend.Employees;
using backend.Common;
using Microsoft.AspNetCore.Mvc;

namespace backend.Customers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService service, ILogger<EmployeeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<EmployeeResponse>>> GetEmployeesAsync([FromQuery] EmployeeQuery query)
        {
            var employees = await _service.GetEmployeesAsync(query);
            return Ok(employees);
        }

        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync([FromRoute] int employeeId)
        {
            var response = await _service.GetEmployeeByIdAsync(employeeId);
            if (response == null)
            {
                _logger.LogWarning($"Employee with ID {employeeId} not found.");
                return NotFound();
            }

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<EmployeeResponse>> CreateEmployeeAsync([FromBody] EmployeeRequest request)
        {
            if (request == null)
            {
                _logger.LogWarning("Failed to create employee: invalid request.");
                return BadRequest();
            }

            var createdEmployee = await _service.CreateEmployeeAsync(request);

            return CreatedAtAction(
                nameof(GetEmployeeByIdAsync),
                new { employeeId = createdEmployee.EmployeeId },
                createdEmployee
            );
        }

        [HttpPatch("{employeeId}")]
        public async Task<IActionResult> UpdateEmployeeByIdAsync([FromRoute] int employeeId, [FromBody] EmployeeRequest request)
        {
            if (request == null)
            {
                _logger.LogWarning("Failed to update employee: invalid request.");
                return BadRequest();
            }

            var updated = await _service.UpdateEmployeeByIdAsync(employeeId, request);
            if (!updated)
            {
                _logger.LogWarning($"Employee with ID {employeeId} not found.");
                return NotFound();
            }

            return NoContent();
        }

    }
}