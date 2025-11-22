namespace backend.Customers
{
    public interface ICustomerRepository
    {
        Task<object> GetCustomersAsync(int page, int pageSize);
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(CustomerDTO customerDTO);
        Task<bool> UpdateCustomerAsync(int customerId, CustomerDTO customerDTO);
        Task<bool> DeleteCustomerByIdAsync(int customerId);
    }
}