namespace backend.Customers
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomersAsync(int page, int pageSize, string? search = null);
        Task<int> GetTotalCountAsync(string? search = null);
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task<Customer> CreateCustomerAsync(CustomerDTO customerDTO);
        Task<bool> UpdateCustomerByIdAsync(int customerId, CustomerDTO customerDTO);
        Task<bool> DeleteCustomerByIdAsync(int customerId);
    }
}