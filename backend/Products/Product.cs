namespace backend.Products;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = default!;
    public decimal UnitPrice { get; set; }
    public string Currency { get; set; } = "eur";
    public decimal VatPercent { get; set; }
}