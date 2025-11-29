using Microsoft.AspNetCore.Mvc;
using backend.Common;
namespace backend.Providers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _service;

        public ProviderController(IProviderService service)
        {
            _service = service;
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
                return NotFound();

            return Ok(provider);
        }
        [HttpPatch("{providerId}")]
        public async Task<IActionResult> UpdateQualifications(int providerId, ProviderRequest request)
        {
            var success = await _service.UpdateProviderByIdAsync(providerId, request);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
