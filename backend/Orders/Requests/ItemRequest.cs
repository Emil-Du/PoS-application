using backend.Common;

namespace backend.Orders;

public class ItemRequest
{
    public ICollection<int> ProductIds { get; set; } = [];
    public Currency? Currency { get; set; }
    public int? Quantity { get; set; }
    public decimal? Discount { get; set; }
    public decimal? VATPercentage { get; set; }
}