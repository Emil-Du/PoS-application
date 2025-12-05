namespace backend.Employees;

public class EmployeeRequest
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public int LocationId { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;

}