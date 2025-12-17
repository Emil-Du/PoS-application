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
using backend.Common;
using backend.Inventory;

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

        modelBuilder.Entity<EmployeeRole>()
            .HasKey(er => new { er.EmployeeId, er.RoleId });

        modelBuilder.Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        modelBuilder.Entity<Product>()
        .Property(p => p.Currency)
        .HasConversion<string>();




        modelBuilder.Entity<Permission>().HasData(
            Enum.GetValues<PermissionFlag>()
                .Select((flag, index) => new Permission
                {
                    PermissionId = index + 1,
                    Name = flag
                })
        );

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
                Name = "FOOD",
                Address = "123 Main St, City",
                PhoneNumber = "987654321",
                Email = "hq@company.com"
            },
            new Location
            {
                LocationId = 2,
                CompanyId = 1,
                Name = "SERVICES",
                Address = "Smth city",
                PhoneNumber = "222333444",
                Email = "branch@company.com"
            }
        );
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ProductId = 1,
                LocationId = 1,
                Name = "Chicken",
                UnitPrice = 7m,
                Currency = Currency.Eur,
                VatPercent = 21m
            },
            new Product
            {
                ProductId = 2,
                LocationId = 1,
                Name = "Fries",
                UnitPrice = 1m,
                Currency = Currency.Eur,
                VatPercent = 21m
            },
            new Product
            {
                ProductId = 3,
                LocationId = 1,
                Name = "Beef",
                UnitPrice = 5m,
                Currency = Currency.Eur,
                VatPercent = 21m
            },
            new Product
            {
                ProductId = 4,
                LocationId = 2,
                Name = "Haircut",
                UnitPrice = 20m,
                Currency = Currency.Eur,
                VatPercent = 21m
            },
            new Product
            {
                ProductId = 5,
                LocationId = 2,
                Name = "Hair dye",
                UnitPrice = 40m,
                Currency = Currency.Eur,
                VatPercent = 21m
            }
        );

        modelBuilder.Entity<Employee>().HasData(
        new Employee
        {
            EmployeeId = 1,
            FirstName = "John",
            LastName = "Pork",
            LocationId = 2,
            PhoneNumber = "+53247865",
            Email = "john@gmail.com",
            Status = 0,
            Salt = "PcXPMJdOXdI8jaO51iElcQ==",
            PasswordHash = "JcDy7FOzBeRm3MvQpgahatB2YWAGMorZoh/U+s5rWu8=" //string
        },
        new Employee
        {
            EmployeeId = 2,
            FirstName = "Mr",
            LastName = "Bean",
            LocationId = 2,
            PhoneNumber = "+532324565",
            Email = "bean@gmail.com",
            Status = 0,
            Salt = "ts6OSzF2JBMKDIFQo4KiCQ==",
            PasswordHash = "tPUom03r7Y6JKDBnlcqwacCXvKK1GHIWFwkf1A96wtg=" //string
        },
        new Employee
        {
            EmployeeId = 3,
            FirstName = "Jazz",
            LastName = "Singsanong",
            LocationId = 1,
            PhoneNumber = "+43278963",
            Email = "jazz@gmail.com",
            Status = 0,
            Salt = "BpclPWmfJoKhQ0n57VcWNA==",
            PasswordHash = "yotfvuH/qPE5/d8RknKRJM1Km086Pe7IgT2Z0MUqXoE=" //string
        }
    );

    modelBuilder.Entity<Service>().HasData(
            new Service
            {
                ServiceId = 1,
                ProductId = 4,
                LocationId = 2,
                Status = ServiceStatus.available,
                DurationMinutes = 60
            },
            new Service
            {
                ServiceId = 2,
                ProductId = 5,
                LocationId = 2,
                Status = ServiceStatus.available,
                DurationMinutes = 60
            }
        );

        modelBuilder.Entity<EmployeeServiceQualification>().HasData(
            new EmployeeServiceQualification
            {
                EmployeeId = 1,
                ServiceId = 1
            },

            new EmployeeServiceQualification
            {
                EmployeeId = 1,
                ServiceId = 2
            }
        );

        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                RoleId = 1,
                Name = "Employee"
            },

            new Role
            {
                RoleId = 2,
                Name = "Manager"
            }
        );

        modelBuilder.Entity<EmployeeRole>().HasData(
            new EmployeeRole
            {
                EmployeeId = 1,
                RoleId = 1
            },

            new EmployeeRole
            {
                EmployeeId = 2,
                RoleId = 2
            },

            new EmployeeRole
            {
                EmployeeId = 3,
                RoleId = 1
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
    public DbSet<ItemVariationSelection> ItemVariationSelections { get; set; }
    public DbSet<Variation> Variations { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<EmployeeRole> EmployeeRoles { get; set; }
    public DbSet<Stock> StockList { get; set; }
    public DbSet<ProductStockRequirement> ProductStockRequirements { get; set; }
}
