namespace backend.Payments;

public class PaymentRequest
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; } = 0;
    public String Method { get; set; } = string.Empty;
    public String Currency { get; set; } = string.Empty;

}
