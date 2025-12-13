using backend.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.Variations;

public class VariationRepository : IVariationRepository
{
    private readonly AppDbContext _context;

    public VariationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Variation?> GetVariationByIdAsync(int variationId)
    {
        return await _context.Variations.FindAsync(variationId);
    }

    public async Task<IEnumerable<Variation?>> GetVariationsByItemIdAsync(int itemId)
    {
        return await _context.ItemVariationSelections
            .Where(selection => selection.ItemId == itemId)
            .Select(selection => selection.Variation)
            .ToListAsync();
    }
}