namespace backend.Services;

public interface IServiceService
{
    Task<List<ServiceResponse>> GetServicesAsync(ServiceQuery query);
    Task<ServiceResponse?> GetServiceByIdAsync(int serviceId);
    Task<bool> UpdateServiceByIdAsync(int serviceId, ServiceRequest request);

}