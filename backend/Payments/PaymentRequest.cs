namespace backend.Payments;

public class PaymentRequest
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public String Method { get; set; } = null!;
    public String Currency { get; set; } = null!;

}
