namespace backend.Payments;

public class PaymentResponse
{
    public int PaymentId { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; } = null!;
    public string Currency { get; set; } = null!;
}
