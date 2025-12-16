namespace backend.Payments;

public class Payment
{
    public int PaymentId { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }

    public String Currency { get; set; } = null!;

    public String Method { get; set; } = null!;

}