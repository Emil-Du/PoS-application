using backend.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace backend.Employees
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
        [Authorize(Roles = Roles.Roles.SuperAdmin + "," + Roles.Roles.Manager + "," + Roles.Roles.Employee)]
        public async Task<ActionResult<PaginatedResponse<EmployeeResponse>>> GetEmployees([FromQuery] EmployeeQuery query)
        {
            var employees = await _service.GetEmployeesAsync(query);
            return Ok(employees);
        }

        [HttpGet("{employeeId}")]
        [Authorize(Roles = Roles.Roles.SuperAdmin + "," + Roles.Roles.Manager + "," + Roles.Roles.Employee)]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeById([FromRoute] int employeeId)
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
        [Authorize(Roles = Roles.Roles.SuperAdmin + "," + Roles.Roles.Manager)]
        public async Task<ActionResult<EmployeeResponse>> CreateEmployee([FromBody] EmployeeRequest request)
        {
            var createdEmployee = await _service.CreateEmployeeAsync(request);

            return CreatedAtAction(
                nameof(GetEmployeeById),
                new { employeeId = createdEmployee.EmployeeId },
                createdEmployee
            );
        }

        [HttpPatch("{employeeId}")]
        [Authorize(Roles = Roles.Roles.SuperAdmin + "," + Roles.Roles.Manager)]
        public async Task<IActionResult> UpdateEmployeeById([FromRoute] int employeeId, [FromBody] EmployeeRequest request)
        {
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