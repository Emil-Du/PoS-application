namespace backend.Providers;

using backend.Common;
public interface IProviderService
{
    Task<ProviderResponse?> GetProviderByIdAsync(int providerId);
    Task<bool> UpdateProviderByIdAsync(int providerId, ProviderRequest request);
    Task<PaginatedResponse<ProviderResponse>> GetProvidersAsync(ProviderQuery query);
}
