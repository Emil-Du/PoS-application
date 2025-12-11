using backend.Common;

namespace backend.Orders;

public class OrderRequest
{
    public int? OperatorId;
    public decimal? Tip;
    public decimal? Discount;
    public decimal? ServiceCharge;
    public OrderStatus? Status;
    public Currency? Currency;

    public OrderRequest(decimal? tip = null, decimal? discount = null, decimal? serviceCharge = null, OrderStatus? status = null, Currency? currency = null)
    {
        Tip = tip;
        Discount = discount;
        ServiceCharge = serviceCharge;
        Status = status;
        Currency = currency;
    }
}