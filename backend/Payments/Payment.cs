namespace backend.Payments;

public class Payment
{
    public int PaymentId { get; set; }
    public int OrderId { get; set; }
    public Money Amount { get; set; } = null!;

    public String Currency { get; set; } = string.Empty;

    public String Method { get; set; } = string.Empty;

    public int PayerCustomerId { get; set; }
    public String? StripeChargeId { get; set; }
}