namespace backend.Customers
{
    public interface ICustomerService
    {
        Task<object> GetCustomersAsync(int page, int pageSize);
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task<Customer> CreateCustomerAsync(CustomerDTO customerDTO);
        Task<bool> UpdateCustomerByIdAsync(int customerId, CustomerDTO customerDTO);
        Task<bool> DeleteCustomerByIdAsync(int customerId);
    }
}
