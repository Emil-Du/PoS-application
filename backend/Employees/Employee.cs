using backend.Mappings;
namespace backend.Employees;

public class Employee
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public int LocationId { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public EmployeeStatus Status { get; set; } = EmployeeStatus.active;

    public ICollection<EmployeeServiceQualification> Qualifications { get; set; } // navigation, to use methods like .Include()
            = new List<EmployeeServiceQualification>();

}