using backend.Common;
namespace backend.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductResponse>> GetProductsByLocationAsync(int locationId)
        {
            var products = await _repository.GetProductsByLocationAsync(locationId);

            return products.Select(p => new ProductResponse
            {
                ProductId = p.ProductId,
                Name = p.Name,
                UnitPrice = p.UnitPrice,
                Currency = p.Currency,
                VatPercent = p.VatPercent
            }).ToList();
        }



    }
}