using backend.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.Products;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _context.Products.FindAsync(productId);
    }

    public async Task<IEnumerable<Product?>> GetProductsByItemIdAsync(int itemId)
    {
        return await _context.ItemProductSelections
            .Where(selection => selection.ItemId == itemId)
            .Select(selection => selection.Variation)
            .ToListAsync();
    }
}