namespace backend.Products;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = "EUR";
    public decimal VatPercent { get; set; }

    public List<ProductVariation> Variations { get; set; } = new();
}
