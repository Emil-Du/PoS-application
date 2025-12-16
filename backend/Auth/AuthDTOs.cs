namespace backend.Auth;
using backend.Employees;

public class EmployeeRegistrationDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password {get; set; } = string.Empty;
}

public class EmployeeRegistrationResponseDTO
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;
}

public class EmployeeLoginDTO
{
    public string Email { get; set; } = string.Empty;
    public string Password {get; set; } = string.Empty;
}

public class EmployeeLoginResponseDTO
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public int LocationId { get; set; }

    //public string EmployeeRole { get; set; } = default!;
}

public class EmployeeLoginServiceResponse
{
    public EmployeeLoginResponseDTO Employee { get; set; } = default!;
    public string AccessToken { get; set; } = default!;
    public int ExpiresIn { get; set; }
}
