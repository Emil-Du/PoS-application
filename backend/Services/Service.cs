namespace backend.Services;

using backend.Mappings;

public class Service
{
    public int ServiceId { get; set; }
    public int ProductId { get; set; }
    public int LocationId { get; set; }
    public ServiceStatus Status { get; set; } = ServiceStatus.available;

    public ICollection<EmployeeServiceQualification> Qualifications { get; set; } // navigation, to use methods like .Include()
            = new List<EmployeeServiceQualification>();

}