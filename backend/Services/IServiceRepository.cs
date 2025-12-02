namespace backend.Services;

public interface IServiceRepository
{
    Task<List<Service>> GetServicesAsync(ServiceQuery query);
    Task<Service?> GetServiceByIdAsync(int serviceId);
    Task<bool> UpdateServiceAsync(Service service);

}