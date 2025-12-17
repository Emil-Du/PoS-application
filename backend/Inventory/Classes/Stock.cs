namespace backend.Inventory;

public class Stock
{
    public int StockId { get; set; }
    public int LocationId { get; set; }
    public string Name { get; set; } = String.Empty;
    public int Count { get; set; }
}