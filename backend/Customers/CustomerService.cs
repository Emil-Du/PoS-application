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

        public async Task<PaginatedResponse<Customer>> GetCustomersAsync(int page = 1, int pageSize = 25, string? search = null)
        {
            var customers = await _repository.GetCustomersAsync(page, pageSize, search);
            var total = await _repository.GetTotalCountAsync(search);

            return new PaginatedResponse<Customer>
            {
                Page = page,
                PageSize = pageSize,
                Total = total,
                Data = customers
            };
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
