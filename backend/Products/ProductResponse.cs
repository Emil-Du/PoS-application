namespace backend.Products;

using backend.Common;
public class ProductResponse
{
    public int ProductId { get; set; }
    public string Name { get; set; } = default!;
    public decimal UnitPrice { get; set; }
    public required Currency Currency { get; set; }
    public decimal VatPercent { get; set; }

}
