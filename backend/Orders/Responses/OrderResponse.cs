using backend.Common;

namespace backend.Orders;

public class OrderResponse
{
    public int OrderId { get; set; }
    public int OperatorId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal Tip { get; set; }
    public decimal ServiceCharge { get; set; }
    public Currency Currency { get; set; }
    public decimal Discount { get; set; }
    public IEnumerable<ItemResponse> Items { get; set; } = [];
}