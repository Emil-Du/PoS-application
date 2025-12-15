using System.Threading.Tasks;
using backend.Database;
using backend.Orders;
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

    public async Task<IEnumerable<Product?>> GetProductsByItemIdsAsync(IEnumerable<int> itemIds)
    {
        return await _context.Products.Where(product => itemIds.Contains(product.ProductId)).ToListAsync();
    }
}