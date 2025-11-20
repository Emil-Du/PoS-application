using Microsoft.EntityFrameworkCore;
using backend.Customers;

namespace backend.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                CustomerId = 1,
                Name = "Alice",
                Email = "alice@example.com",
                PhoneNumber = "123456789"
            },
            new Customer
            {
                CustomerId = 2,
                Name = "Bob",
                Email = "bob@example.com",
                PhoneNumber = "987654321"
            }
        );
    }
}
