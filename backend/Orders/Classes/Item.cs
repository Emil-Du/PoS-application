using backend.Common;

namespace backend.Orders;

public class Item
{
    public int ItemId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public required Currency Currency { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
    public decimal VATPercentage { get; set; }
    public ItemStatus Status { get; set; }
}