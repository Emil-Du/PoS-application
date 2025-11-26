namespace backend.Refunds;

public class RefundRequest
{
    public int PaymentId { get; set; }
    public string Reason { get; set; } = string.Empty;
}
