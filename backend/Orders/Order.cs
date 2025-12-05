using System.Collections.Concurrent;
using System.Numerics;
using backend.Payments;

namespace backend.Orders;

public class Order
{
    public int Id { get; set; }
    public int OperatorId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal Tip { get; set; }
    public decimal ServiceCharge { get; set; }
    public decimal Discount { get; set; }
    public Currency Currency { get; set; }
    public BigInteger Time { get; set; }
    public required List<Payment> Payments { get; set; }
}

