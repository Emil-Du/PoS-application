namespace backend.Products;

public interface IProductRepository
{
    public Task<Product?> GetProductByIdAsync(int productId);
    public Task<IEnumerable<Product?>> GetProductsByItemIdAsync(int itemId);
}