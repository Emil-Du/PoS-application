namespace backend.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<CustomerResponse>> GetCustomersAsync(CustomerQuery query)
        {
            var customers = await _repository.GetCustomersAsync(query);
            var total = await _repository.GetTotalCountAsync(query);

            var data = customers.Select(c => new CustomerResponse
            {
                CustomerId = c.CustomerId,
                Name = c.Name,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            }).ToList();

            return new PaginatedResponse<CustomerResponse>
            {
                Page = query.Page,
                PageSize = query.PageSize,
                Total = total,
                Data = data
            };
        }

        public async Task<CustomerResponse?> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _repository.GetCustomerByIdAsync(customerId);
            if (customer == null) return null;

            return new CustomerResponse
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
        }

        public async Task<CustomerResponse> CreateCustomerAsync(CustomerRequest request)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            var created = await _repository.CreateCustomerAsync(customer);

            return new CustomerResponse
            {
                CustomerId = created.CustomerId,
                Name = created.Name,
                Email = created.Email,
                PhoneNumber = created.PhoneNumber
            };
        }

        public async Task<bool> UpdateCustomerByIdAsync(int customerId, CustomerRequest request)
        {
            var customer = new Customer
            {
                CustomerId = customerId,
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            return await _repository.UpdateCustomerByIdAsync(customer);
        }

        public async Task<bool> DeleteCustomerByIdAsync(int customerId)
        {
            return await _repository.DeleteCustomerByIdAsync(customerId);
        }
    }
}
