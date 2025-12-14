using Microsoft.Extensions.Logging;
using backend.Products;
namespace backend.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repository;

        public ServiceService(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ServiceResponse>> GetServicesAsync(ServiceQuery query)
        {
            var services = await _repository.GetServicesAsync(query);

            return services.Select(s => new ServiceResponse
            {
                ServiceId = s.ServiceId,
                ProductId = s.ProductId,
                CompanyId = s.Location.CompanyId,
                Title = s.Product.Name,
                BasePrice = new BasePrice
                {
                    Amount = s.Product.UnitPrice,
                    Currency = s.Product.Currency
                },
                DurationMinutes = s.DurationMinutes,
                Status = s.Status
            }).ToList();
        }

        public async Task<ServiceResponse?> GetServiceByIdAsync(int serviceId)
        {
            var s = await _repository.GetServiceByIdAsync(serviceId);
            if (s == null) return null;

            return new ServiceResponse
            {
                ServiceId = s.ServiceId,
                ProductId = s.ProductId,
                CompanyId = s.Location.CompanyId,
                Title = s.Product.Name,
                BasePrice = new BasePrice
                {
                    Amount = s.Product.UnitPrice,
                    Currency = s.Product.Currency
                },
                DurationMinutes = s.DurationMinutes,
                Status = s.Status
            };
        }
        public async Task<ServiceResponse> CreateServiceAsync(CreateServiceRequest request)
        {
            var service = new Service
            {
                ProductId = request.ProductId,
                LocationId = request.LocationId,
                Status = request.Status,
                DurationMinutes = request.DurationMinutes
            };

            var createdService = await _repository.CreateServiceAsync(service);

            return new ServiceResponse
            {
                ServiceId = createdService.ServiceId,
                ProductId = createdService.ProductId,
                CompanyId = createdService.Location.CompanyId,
                Title = createdService.Product.Name,
                BasePrice = new BasePrice
                {
                    Amount = createdService.Product.UnitPrice,
                    Currency = createdService.Product.Currency
                },
                DurationMinutes = createdService.DurationMinutes,
                Status = createdService.Status
            };
        }


        public async Task<bool> UpdateServiceByIdAsync(int serviceId, ServiceRequest request)
        {
            var service = new Service
            {
                ServiceId = serviceId,
                Status = request.Status,
                DurationMinutes = request.DurationMinutes,
                ProductId = request.ProductId,
                Product = new Product
                {
                    UnitPrice = request.BasePrice.Amount,
                    Currency = request.BasePrice.Currency,
                    Name = request.Title
                }
            };

            return await _repository.UpdateServiceAsync(service);
        }

    }
}
