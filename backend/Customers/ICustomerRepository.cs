namespace backend.Customers
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomersAsync(CustomerQuery query);
        Task<int> GetTotalCountAsync(CustomerQuery query);
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerByIdAsync(Customer customer);
        Task<bool> DeleteCustomerByIdAsync(int customerId);

    }
}