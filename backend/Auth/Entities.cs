// Reikia suvienodint su backend.Customers ir backend.Employees

namespace backend.Auth;

public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public string PasswordHash {get; set; } = string.Empty;

}

public enum EmployeeStatus
{
    Active,
    Inactive
}

public class Employee
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;
    public string Salt { get; set; } = string.Empty;
    public string PasswordHash {get; set; } = string.Empty;
}