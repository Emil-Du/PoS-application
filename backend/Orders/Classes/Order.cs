using backend.Common;
using backend.Employees;

namespace backend.Orders;

public class Order
{
    public int OrderId { get; set; }
    public int OperatorId { get; set; }
    public Employee Operator { get; set; } = null!;
    public OrderStatus Status { get; set; }
    public decimal Tip { get; set; }
    public decimal ServiceCharge { get; set; }
    public decimal Discount { get; set; }
    public Currency Currency { get; set; }

    public ICollection<Item> Items { get; set; } = [];
}

