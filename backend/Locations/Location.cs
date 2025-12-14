using backend.Companies;
using backend.Employees;
using backend.Reservations;
using backend.Services;
namespace backend.Locations;

public class Location
{
    public int LocationId { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public ICollection<Service> Services { get; set; } = new List<Service>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}