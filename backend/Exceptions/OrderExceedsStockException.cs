using System.Security;

namespace backend.Exceptions;

public class OrderExceedsStockException : Exception
{
    public OrderExceedsStockException() : base() {}
}