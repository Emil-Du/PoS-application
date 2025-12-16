namespace backend.Orders;

public class OrderTaxesResponse
{
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
}