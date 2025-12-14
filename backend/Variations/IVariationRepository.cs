namespace backend.Variations;

public interface IVariationRepository
{
    public Task<Variation?> GetVariationByIdAsync(int variationId);
    public Task<IEnumerable<Variation?>> GetVariationsByItemIdAsync(int itemId);
}