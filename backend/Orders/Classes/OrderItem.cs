namespace backend.Orders;

public class OrderItem
{
    public int Id {get; set; }
    public int OrderId {get; set; }
    public decimal UnitPrice {get; set; }
    public required string Currency {get; set; }
    public decimal Quantity {get; set; }
    public decimal Discount {get; set; }
    public decimal VATPercentage {get; set; }
}