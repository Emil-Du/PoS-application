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

    public async Task<IEnumerable<Product>> GetProductsByLocationAsync(int locationId)
    {
        return await _context.Products
        .Include(p => p.Location)
        .Where(p => p.LocationId == locationId)
        .ToListAsync();
    }
}