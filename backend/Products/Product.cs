using backend.Common;
using backend.Locations;
using backend.Mappings;
namespace backend.Products;

public class Product
{
    public int ProductId { get; set; }

    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;
    public string Name { get; set; } = default!;
    public decimal UnitPrice { get; set; }
    public required Currency Currency { get; set; }
    public decimal VatPercent { get; set; }
    public IEnumerable<ProductStockRequirement> Requirements { get; set; } = default!;
}