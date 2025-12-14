using backend.Common;
using backend.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace backend.Orders
{
    [ApiController]
    [Route("api/orders")]
    // [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> OpenNewOrder([FromBody] OrderRequest request)
        {
            try
            {
                var order = await _service.OpenOrderAsync(request);

                return Created(order.OrderId.ToString(), order);
            }
            catch (BadHttpRequestException)
            {
                return BadRequest();
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

        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrderById(int orderId)
        {
            try
            {
                var order = await _service.GetOrderByIdAsync(orderId);
                return Ok(order);
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

        [HttpPatch("{orderId}")]
        public async Task<IActionResult> UpdateOrderFields(int orderId, [FromBody] OrderRequest request)
        {
            try
            {
                await _service.UpdateOrderAsync(orderId, request);

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

        [HttpPost("{orderId}/close")]
        public async Task<IActionResult> CloseOrder(int orderId)
        {
            try
            {
                await _service.CloseOrderAsync(orderId);

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

        [HttpPost("{orderId}/cancel")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            try
            {
                await _service.CancelOrderAsync(orderId);

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

        [HttpGet("{orderId}/receipt")]
        public async Task<IActionResult> GetReceipt(int orderId)
        {
            try
            {
                return Ok(await _service.GetReceiptAsync(orderId));
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{orderId}/items")]
        public async Task<IActionResult> GetOrderItems(int orderId)
        {
            try
            {
                var items = await _service.GetItemsByOrderAsync(orderId);
                return Ok(items);
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

        [HttpPost("{orderId}/items")]
        public async Task<IActionResult> AddItem(int orderId, [FromBody] ItemRequest request)
        {
            try
            {
                var createdItem = await _service.AddItemAsync(orderId, request);
                return Created(createdItem.ItemId.ToString(), createdItem);
            }
            catch (BadHttpRequestException)
            {
                return BadRequest();
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

        [HttpPatch("{orderId}/items/{itemId}")]
        public async Task<IActionResult> UpdateItem(int orderId, int itemId, [FromBody] ItemRequest item)
        {
            try
            {
                await _service.UpdateItemAsync(orderId, itemId, item);

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

        [HttpDelete("{orderId}/items/{itemId}")]
        public async Task<IActionResult> RemoveItem(int orderId, int itemId)
        {
            try
            {
                await _service.RemoveItemAsync(orderId, itemId);
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