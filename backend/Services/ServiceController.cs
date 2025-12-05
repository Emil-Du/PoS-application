using backend.Common;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<ActionResult<List<ServiceResponse>>> GetServicesAsync([FromQuery] ServiceQuery query)
        {
            var services = await _service.GetServicesAsync(query);
            return Ok(services);
        }

        [HttpGet("{serviceId}")]
        public async Task<ActionResult<ServiceResponse>> GetServiceByIdAsync([FromRoute] int serviceId)
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
        public async Task<IActionResult> UpdateServiceByIdAsync([FromRoute] int serviceId, [FromBody] ServiceRequest request)
        {
            if (request == null)
            {
                _logger.LogWarning("Failed to update service: invalid request.");
                return BadRequest();
            }

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