using backend.Companies;
using backend.Employees;
using backend.Locations;
using backend.Mappings;
using backend.Products;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using backend.Roles;
using backend.Payments;
using backend.Reservations;
using backend.Customers;
using backend.Orders;

namespace backend.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeServiceQualification>()
            .HasKey(q => new { q.EmployeeId, q.ServiceId });

        modelBuilder.Entity<ItemProductSelection>()
            .HasKey(q => new { q.ItemId, q.ProductId });


        modelBuilder.Entity<Company>().HasData(
            new Company
            {
                CompanyId = 1,
                Name = "Main Company",
                PhoneNumber = "123456789",
                Email = "main@company.com"
            }
        );

        modelBuilder.Entity<Location>().HasData(
            new Location
            {
                LocationId = 1,
                CompanyId = 1,
                Name = "Headquarters",
                Address = "123 Main St, City",
                PhoneNumber = "987654321",
                Email = "hq@company.com"
            },
            new Location
            {
                LocationId = 2,
                CompanyId = 1,
                Name = "Branch Office",
                Address = "Smth city",
                PhoneNumber = "222333444",
                Email = "branch@company.com"
            }
        );
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ProductId = 1,
                Name = "Chicken",
                UnitPrice = 7m,
                Currency = "eur",
                VatPercent = 21m
            },
            new Product
            {
                ProductId = 2,
                Name = "Potato",
                UnitPrice = 1m,
                Currency = "eur",
                VatPercent = 21m
            },
            new Product
            {
                ProductId = 3,
                Name = "Beef",
                UnitPrice = 5m,
                Currency = "eur",
                VatPercent = 21m
            }
        );

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeServiceQualification> EmployeeServiceQualifications { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemProductSelection> ItemProductSelections { get; set; }

    public DbSet<EmployeeRole> EmployeeRoles { get; set; }

}

// Bnadymas priverst veikt.
public class EmployeeRole
{
    public int Id { get; set; }
    public int RoleId { get; set; }
}