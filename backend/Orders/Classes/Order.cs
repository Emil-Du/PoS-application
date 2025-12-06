using System.ComponentModel.DataAnnotations;
using System.Numerics;
using backend.Common;
using backend.Payments;

namespace backend.Orders;

public class Order
{
    public Order(int operatorId, decimal serviceCharge, decimal discount, Currency currency)
    {
        OperatorId = operatorId;
        ServiceCharge = serviceCharge;
        Discount = discount;
        Currency = currency;
        Time = DateTime.Now.Ticks;
        Payments = [];
    }

    public int OrderId { get; set; }
    public int OperatorId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal Tip { get; set; }
    public decimal ServiceCharge { get; set; }
    public decimal Discount { get; set; }
    public Currency Currency { get; set; }
    public BigInteger Time { get; set; }
    public IEnumerable<Payment> Payments { get; set; }
}

