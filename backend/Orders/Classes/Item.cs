using System.ComponentModel.DataAnnotations.Schema;
using backend.Common;
using backend.Products;
using backend.Variations;

namespace backend.Orders;

public class Item
{
    public int ItemId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public required Currency Currency { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
    public decimal VATPercentage { get; set; }
    public IEnumerable<Variation> Variations { get; set; } = [];
}