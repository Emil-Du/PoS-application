using backend.Locations;
using backend.Mappings;
using backend.Orders;
using backend.Reservations;
using backend.Roles;
namespace backend.Employees;

public class Employee
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;
    public string Salt { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public ICollection<Reservation> Reservations { get; set; } = [];
    public ICollection<Order> Orders { get; set; } = [];
    public ICollection<EmployeeServiceQualification> Qualifications { get; set; } = [];

}