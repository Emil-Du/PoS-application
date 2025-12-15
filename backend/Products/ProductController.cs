using backend.Employees;
using backend.Common;
using Microsoft.AspNetCore.Mvc;

namespace backend.Products
{
    [ApiController]
    [Route("api/[controller]")]
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