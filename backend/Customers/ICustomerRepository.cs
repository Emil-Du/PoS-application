namespace backend.Customers
{
    public interface ICustomerRepository
    {
        Task<object> GetCustomersAsync(int page, int pageSize);
    }
}