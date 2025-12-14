namespace backend.Providers;

using backend.Employees;
public interface IProviderRepository
{
    Task<Employee?> GetProviderByIdAsync(int providerId);
    Task<bool> UpdateProviderByIdAsync(int employeeId, ProviderRequest request);
    Task<List<Employee>> GetProvidersAsync(ProviderQuery query);
    Task<int> GetTotalCountAsync(ProviderQuery query);
}
