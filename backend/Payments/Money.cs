namespace backend.Payments;

public class Money
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
}
