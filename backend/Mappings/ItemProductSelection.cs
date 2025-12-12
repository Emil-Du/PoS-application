using backend.Orders;
using backend.Products;

namespace backend.Mappings;

public class ItemProductSelection
{
    public int ItemId { get; set; }
    public Item Item { get; set; } = default!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = default!;
}