namespace backend.Payments;

public class PaymentRequest
{
    public int OrderId { get; set; }
    public Money Amount { get; set; } = null!;

}
