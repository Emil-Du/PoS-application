using backend.Employees;
using backend.Mappings;
using backend.Services;
using Microsoft.EntityFrameworkCore;

namespace backend.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeServiceQualification> ProviderServiceQualifications { get; set; }
    public DbSet<Service> Services { get; set; }

}
