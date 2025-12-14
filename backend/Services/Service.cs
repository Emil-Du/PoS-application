namespace backend.Services;

using backend.Mappings;
using backend.Products;
using backend.Locations;
using backend.Reservations;

public class Service
{
    public int ServiceId { get; set; }
    public int ProductId { get; set; }
    public int LocationId { get; set; }
    public ServiceStatus Status { get; set; } = ServiceStatus.available;
    public int DurationMinutes { get; set; }
    public Product Product { get; set; } = null!;
    public Location Location { get; set; } = null!;
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<EmployeeServiceQualification> QualifiedEmployees { get; set; }
            = new List<EmployeeServiceQualification>();

}