namespace backend.Customers
{
    public interface ICustomerService
    {
        Task<PaginatedResponse<Customer>> GetCustomersAsync(int page = 1, int pageSize = 25, string? search = null);
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task<Customer> CreateCustomerAsync(CustomerDTO customerDTO);
        Task<bool> UpdateCustomerByIdAsync(int customerId, CustomerDTO customerDTO);
        Task<bool> DeleteCustomerByIdAsync(int customerId);
    }
}
