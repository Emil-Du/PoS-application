namespace backend.Services;

public class BasePrice
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "eur";
}