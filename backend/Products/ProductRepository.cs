using backend.Database;

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
        return _context.ItemProductSelections
            .Where(selection => selection.ItemId == itemId)
            .Select(selection => _context.Products.Find(selection.ProductId));
    }
}