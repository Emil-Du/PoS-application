using backend.Orders;

namespace backend.Products;

public interface IProductRepository
{
    public Task<Product?> GetProductByIdAsync(int productId);
    public IEnumerable<Product?> GetProductsByItems(IEnumerable<Item> items);
    public Task<IEnumerable<Product>> GetProductsByLocationAsync(int locationId);
}