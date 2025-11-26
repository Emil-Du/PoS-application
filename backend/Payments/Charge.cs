namespace backend;

public class Charge
{
    public int OrderId { get; set; }
    public String StripeChargeId { get; set; } = string.Empty;
}
