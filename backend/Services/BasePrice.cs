using backend.Common;
namespace backend.Services;

public class BasePrice
{
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
}