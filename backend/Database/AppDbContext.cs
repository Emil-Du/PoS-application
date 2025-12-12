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
using backend.Variations;

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

        modelBuilder.Entity<ItemVariationSelection>()
            .HasKey(q => new { q.ItemId, q.VariationId });



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
    public DbSet<ItemVariationSelection> ItemVariationSelections { get; set; }
    public DbSet<Variation> Variations { get; set; }
    public DbSet<EmployeeRole> EmployeeRoles { get; set; }

}

// Bnadymas priverst veikt.
public class EmployeeRole
{
    public int Id { get; set; }
    public int RoleId { get; set; }
}