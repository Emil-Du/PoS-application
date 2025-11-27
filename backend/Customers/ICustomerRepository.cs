namespace backend.Customers
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomersAsync(CustomerQuery query);
        Task<int> GetTotalCountAsync(CustomerQuery query);
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int id);

    }
}