namespace backend.Customers
{
    public interface ICustomerService
    {
        Task<PaginatedResponse<CustomerResponse>> GetCustomersAsync(CustomerQuery query);
        Task<CustomerResponse?> GetCustomerByIdAsync(int id);
        Task<CustomerResponse> CreateCustomerAsync(CustomerRequest request);
        Task<bool> UpdateCustomerByIdAsync(int id, CustomerRequest request);
        Task<bool> DeleteCustomerByIdAsync(int id);

    }
}
