namespace backend.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetProductsByLocationAsync(int locationId);
    }
}