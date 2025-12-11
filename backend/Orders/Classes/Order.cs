using System.ComponentModel.DataAnnotations;
using System.Numerics;
using backend.Common;
using backend.Payments;

namespace backend.Orders;

public class Order
{
    public Order(int operatorId, Currency currency, decimal serviceCharge = 0, decimal discount = 0)
    {
        OperatorId = operatorId;
        ServiceCharge = serviceCharge;
        Discount = discount;
        Currency = currency;
        Payments = [];
    }

    public int OrderId { get; set; }
    public int OperatorId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal Tip { get; set; }
    public decimal ServiceCharge { get; set; }
    public decimal Discount { get; set; }
    public Currency Currency { get; set; }
    public IEnumerable<Payment> Payments { get; set; }
}

