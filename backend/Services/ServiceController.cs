using backend.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Roles.SuperAdmin + "," + Roles.Roles.Manager + "," + Roles.Roles.Employee)]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _service;
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(IServiceService service, ILogger<ServiceController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceResponse>>> GetServices([FromQuery] ServiceQuery query)
        {
            var services = await _service.GetServicesAsync(query);
            return Ok(services);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> CreateService([FromBody] CreateServiceRequest request)
        {
            var createdService = await _service.CreateServiceAsync(request);

            return CreatedAtAction(
                nameof(GetServiceById),
                new { serviceId = createdService.ServiceId },
                createdService
            );
        }

        [HttpGet("{serviceId}")]
        public async Task<ActionResult<ServiceResponse>> GetServiceById([FromRoute] int serviceId)
        {
            var response = await _service.GetServiceByIdAsync(serviceId);
            if (response == null)
            {
                _logger.LogWarning($"Service with ID {serviceId} not found.");
                return NotFound();
            }

            return Ok(response);
        }


        [HttpPatch("{serviceId}")]
        public async Task<IActionResult> UpdateServiceById([FromRoute] int serviceId, [FromBody] ServiceRequest request)
        {
            var updated = await _service.UpdateServiceByIdAsync(serviceId, request);
            if (!updated)
            {
                _logger.LogWarning($"Service with ID {serviceId} not found.");
                return NotFound();
            }

            return NoContent();
        }

    }
}