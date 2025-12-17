using backend.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace backend.Products
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Roles.SuperAdmin + "," + Roles.Roles.Manager + "," + Roles.Roles.Employee)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService service, ILogger<ProductController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{locationId}")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByLocation([FromRoute] int locationId)
        {
            var products = await _service.GetProductsByLocationAsync(locationId);
            return Ok(products);
        }
    }
}