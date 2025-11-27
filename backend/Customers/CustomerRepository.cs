using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Customer>> GetCustomersAsync(CustomerQuery query)
        {
            var dbQuery = _context.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                dbQuery = dbQuery.Where(c =>
                    c.Name.Contains(query.Search) ||
                    c.Email.Contains(query.Search) ||
                    c.PhoneNumber.Contains(query.Search));
            }

            return await dbQuery
                .OrderBy(c => c.CustomerId)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(CustomerQuery query)
        {
            var dbQuery = _context.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                dbQuery = dbQuery.Where(c =>
                    c.Name.Contains(query.Search) ||
                    c.Email.Contains(query.Search) ||
                    c.PhoneNumber.Contains(query.Search));
            }

            return await dbQuery.CountAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            var existing = await _context.Customers.FindAsync(customer.CustomerId);
            if (existing == null) return false;

            existing.Name = customer.Name;
            existing.Email = customer.Email;
            existing.PhoneNumber = customer.PhoneNumber;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
