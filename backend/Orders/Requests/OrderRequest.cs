using backend.Common;

namespace backend.Orders;

public class OrderRequest
{
    public int? OperatorId { get; set; }
    public decimal? Tip { get; set; }
    public decimal? Discount { get; set; }
    public decimal? ServiceCharge { get; set; }
    public OrderStatus? Status { get; set; }
    public Currency? Currency { get; set; }
}
