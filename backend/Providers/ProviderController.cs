using Microsoft.AspNetCore.Mvc;
using backend.Common;
namespace backend.Providers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _service;

        private readonly ILogger<ProviderController> _logger;

        public ProviderController(IProviderService service, ILogger<ProviderController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<ProviderResponse>>> GetProvidersAsync([FromQuery] ProviderQuery query)
        {
            var providers = await _service.GetProvidersAsync(query);
            return Ok(providers);
        }

        [HttpGet("{providerId}")]
        public async Task<ActionResult<ProviderResponse>> GetProviderByIdAsync([FromRoute] int providerId)
        {
            var provider = await _service.GetProviderByIdAsync(providerId);

            if (provider == null)
            {
                _logger.LogWarning($"Provider with ID {providerId} not found.");
                return NotFound();
            }


            return Ok(provider);
        }
        [HttpPatch("{providerId}")]
        public async Task<IActionResult> UpdateQualificationsByIdAsync([FromRoute] int providerId, [FromBody] ProviderRequest request)
        {
            var success = await _service.UpdateProviderByIdAsync(providerId, request);

            if (!success)
            {
                _logger.LogWarning($"Failed to update provider with ID {providerId}.");
                return NotFound();
            }


            return NoContent();
        }
    }
}
