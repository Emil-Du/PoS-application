namespace backend.Auth;

public class CustomerRegistrationDTO
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password {get; set; } = string.Empty;
}

public class CustomerRegistrationResponseDTO
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}

public class CustomerLoginDTO
{
    public string Email { get; set; } = string.Empty;
    public string Password {get; set; } = string.Empty;
}

public class CustomerLoginResponseDTO
{
    public string AccessToken { get; set; } = string.Empty;
    public string TokenType { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
}

public class EmployeeRegistrationDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int EmploymentLocationId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password {get; set; } = string.Empty;
}

public class EmployeeRegistrationResponseDTO
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int EmploymentLocationId { get; set; }
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
    public string AccessToken { get; set; } = string.Empty;
    public string TokenType { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
}