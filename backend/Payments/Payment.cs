namespace backend.Payments;

public class Payment
{
    public int PaymentId { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; } = 0;

    public String Currency { get; set; } = string.Empty;

    public String Method { get; set; } = string.Empty;

}