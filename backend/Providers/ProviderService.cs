using backend.Common;

namespace backend.Providers
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _repository;

        public ProviderService(IProviderRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProviderResponse?> GetProviderByIdAsync(int providerId)
        {
            var emp = await _repository.GetProviderByIdAsync(providerId);

            if (emp == null)
                return null;

            return new ProviderResponse
            {
                ProviderId = emp.EmployeeId,
                EmployeeId = emp.EmployeeId,
                Name = $"{emp.FirstName} {emp.LastName}",
                QualifiedServiceIds = emp.Qualifications
                    .Select(q => q.ServiceId)
                    .ToList()
            };
        }

        public async Task<PaginatedResponse<ProviderResponse>> GetProvidersAsync(ProviderQuery query)
        {
            var providers = await _repository.GetProvidersAsync(query);
            var total = await _repository.GetTotalCountAsync(query);

            var data = providers.Select(emp => new ProviderResponse
            {
                EmployeeId = emp.EmployeeId,
                ProviderId = emp.EmployeeId,
                Name = $"{emp.FirstName} {emp.LastName}",
                QualifiedServiceIds = emp.Qualifications
                    .Select(q => q.ServiceId)
                    .ToList()
            }).ToList();


            return new PaginatedResponse<ProviderResponse>
            {
                Page = query.Page,
                PageSize = query.PageSize,
                Total = total,
                Data = data
            };
        }


        public async Task<bool> UpdateProviderByIdAsync(int providerId, ProviderRequest request)
        {
            return await _repository.UpdateProviderByIdAsync(providerId, request);
        }

    }
}