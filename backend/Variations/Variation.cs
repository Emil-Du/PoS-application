namespace backend.Variations;

public class Variation
{
    public int VariationId { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; } = String.Empty;
    public decimal PriceDifference { get; set; }
}