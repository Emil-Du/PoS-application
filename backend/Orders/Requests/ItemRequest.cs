using backend.Common;

namespace backend.Orders;

public class ItemRequest
{
    public int? ProductId { get; set; }
    public IEnumerable<int>? VariationIds { get; set; } = [];
    public Currency? Currency { get; set; }
    public int? Quantity { get; set; }
    public decimal? Discount { get; set; }
    public decimal? VATPercentage { get; set; }
}