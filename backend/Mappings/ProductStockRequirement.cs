using backend.Inventory;
using backend.Products;

namespace backend.Mappings;

public class ProductStockRequirement
{
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int StockId { get; set; }
    public Stock Stock { get; set; } = null!;
    public int Count { get; set; }
}