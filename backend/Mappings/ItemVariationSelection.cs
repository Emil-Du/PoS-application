using backend.Orders;
using backend.Variations;

namespace backend.Mappings;

public class ItemVariationSelection
{
    public int ItemId { get; set; }
    public Item Item { get; set; } = default!;
    public int VariationId { get; set; }
    public Variation Variation { get; set; } = default!;
}