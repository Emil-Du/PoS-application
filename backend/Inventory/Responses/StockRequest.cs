namespace backend.Inventory;

public class StockRequest
{
    public string Name { get; set; } = default!;
    public int Count { get; set; }
}