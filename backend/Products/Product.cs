using backend.Common;
namespace backend.Products;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = default!;
    public decimal UnitPrice { get; set; }
    public required Currency Currency { get; set; }
    public decimal VatPercent { get; set; }
}