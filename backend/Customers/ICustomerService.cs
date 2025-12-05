namespace backend.Customers
{
    public interface ICustomerService
    {
        Task<PaginatedResponse<CustomerResponse>> GetCustomersAsync(CustomerQuery query);
        Task<CustomerResponse?> GetCustomerByIdAsync(int customerId);
        Task<CustomerResponse> CreateCustomerAsync(CustomerRequest request);
        Task<bool> UpdateCustomerByIdAsync(int customerId, CustomerRequest request);
        Task<bool> DeleteCustomerByIdAsync(int customerId);

    }
}
