namespace backend.Services;

using backend.Mappings;
using backend.Products;
using backend.Locations;

public class Service
{
    public int ServiceId { get; set; }
    public int ProductId { get; set; }
    public int LocationId { get; set; }
    public ServiceStatus Status { get; set; } = ServiceStatus.available;
    public int DurationMinutes { get; set; } // added the field, missing in data model, necessary

    // Removed description(comes from api) as it is not in the data model

    public Product Product { get; set; } = null!;
    public Location Location { get; set; } = null!;
    public ICollection<EmployeeServiceQualification> QualifiedEmployees { get; set; } // navigation, to use methods like .Include()
            = new List<EmployeeServiceQualification>();

}