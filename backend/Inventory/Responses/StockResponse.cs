namespace backend.Inventory;

public class StockResponse
{
    public int StockId { get; set; }
    public string Name { get; set; } = String.Empty;
    public int Count { get; set; }
}