using backend.Database;
using Microsoft.EntityFrameworkCore;


namespace backend.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetCustomersAsync(int page, int pageSize)
        {
            var customers = await _context.Customers
                .OrderBy(c => c.CustomerId)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var total = await _context.Customers.CountAsync();

            return new { Customers = customers, Total = total };
        }
        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        public async Task<Customer> CreateCustomerAsync(CustomerDTO customerDTO)
        {
            var customer = new Customer
            {
                Name = customerDTO.Name,
                Email = customerDTO.Email,
                PhoneNumber = customerDTO.PhoneNumber
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
        public async Task<bool> UpdateCustomerByIdAsync(int customerId, CustomerDTO customerDTO)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return false;
            }

            customer.Name = customerDTO.Name;
            customer.Email = customerDTO.Email;
            customer.PhoneNumber = customerDTO.PhoneNumber;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCustomerByIdAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
