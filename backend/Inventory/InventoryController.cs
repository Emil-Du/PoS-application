using backend.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace backend.Inventory
{
    [ApiController]
    [Route("api/location/{locationId}/inventory")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockResponse>>> GetStockList(int locationId)
        {
            try
            {
                return Ok(await _service.GetStockListByLocationIdAsync(locationId));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<StockResponse>> CreateStock(int locationId, [FromBody] StockRequest request)
        {
            try
            {
                var stock = await _service.AddStockToListAsync(locationId, request);

                return Created(stock.StockId.ToString(), stock);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> UpdateStock(int stockId, [FromBody] StockRequest request)
        {
            try
            {
                await _service.UpdateStockByIdAsync(stockId, request);

                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{stockId}")]
        public async Task<IActionResult> DeleteStock(int stockId)
        {
            try
            {
                await _service.RemoveStockByIdAsync(stockId);

                return StatusCode(204);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
