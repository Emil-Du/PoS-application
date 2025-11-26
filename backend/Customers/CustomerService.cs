namespace backend.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository repository, ILogger<CustomerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<object> GetCustomersAsync(int page, int pageSize)
        {
            return await _repository.GetCustomersAsync(page, pageSize);
        }

        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await _repository.GetCustomerByIdAsync(customerId);
        }

        public async Task<Customer> CreateCustomerAsync(CustomerDTO customerDTO)
        {
            return await _repository.CreateCustomerAsync(customerDTO);
        }

        public async Task<bool> UpdateCustomerByIdAsync(int customerId, CustomerDTO customerDTO)
        {
            return await _repository.UpdateCustomerByIdAsync(customerId, customerDTO);
        }

        public async Task<bool> DeleteCustomerByIdAsync(int customerId)
        {
            return await _repository.DeleteCustomerByIdAsync(customerId);
        }
    }
}
