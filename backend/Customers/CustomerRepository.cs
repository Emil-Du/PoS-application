using backend.Database;
using Microsoft.EntityFrameworkCore;



namespace backend.Customers
{
    public class CustomerRepository
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

    }
}
