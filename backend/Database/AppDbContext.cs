using backend.Companies;
using backend.Employees;
using backend.Locations;
using backend.Mappings;
using backend.Products;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using backend.Auth;
using backend.Reservations;
using backend.Customers;

namespace backend.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeServiceQualification> EmployeeServiceQualifications { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Reservation> Reservations { get; set; }


}
