namespace backend.Products;

public interface IProductRepository
{
    public Task<Product?> GetProductByIdAsync(int productId);
}