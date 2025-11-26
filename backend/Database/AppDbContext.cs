using Microsoft.EntityFrameworkCore;
using backend.Roles;
using backend.Payments;

namespace backend.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {}
    
    public DbSet<Role> Roles { get; set; }
    public DbSet<Payment> Payments { get; set; }

    // will be EmployeeRoles table 
    //public DbSet<EmployeeRole> EmployeeRoles { get; set; } new class?
}
