using backend.Auth;
using backend.Customers;
using backend.Employees;

public class AuthService : IAuthService
{
    private readonly AuthRepository _authRepository;
    private readonly IConfiguration _configuration;

    public AuthService(AuthRepository authRepository, IConfiguration configuration)
    {
        _authRepository = authRepository;
        _configuration = configuration;
    }

    public async Task<Customer> RegisterCustomer(CustomerRegistrationDTO customerRegistrationDTO)
    {
        if (await _authRepository.CustomerEmailExists(customerRegistrationDTO.Email))
        {
            throw new EmailAlreadyExistsException();
        }

        var newCustomer = await _authRepository.RegisterCustomerAsync(customerRegistrationDTO);

        return newCustomer;
    }

    public async Task<CustomerLoginResponseDTO> LoginCustomer(CustomerLoginDTO customerLoginDTO)
    {
        int loggedInCustomerId = await _authRepository.LoginCustomerAsync(customerLoginDTO);

        int expiresIn = int.Parse(_configuration["Jwt:ExpiresIn"] ?? throw new Exception("JWT 'ExpiresIn' is missing in configuration."));

        return new CustomerLoginResponseDTO
            {
                AccessToken = JwtUtils.GenerateToken(loggedInCustomerId, _configuration),
                TokenType = "Bearer",
                ExpiresIn = expiresIn
            };
    }

    public async Task<Employee> RegisterEmployee(EmployeeRegistrationDTO employeeRegistrationDTO)
    {
        if (await _authRepository.EmployeeEmailExists(employeeRegistrationDTO.Email))
        {
            throw new EmailAlreadyExistsException();
        }

        return await _authRepository.RegisterEmployeeAsync(employeeRegistrationDTO);
    }

    public async Task<EmployeeLoginServiceResponse> LoginEmployee(EmployeeLoginDTO employeeLoginDTO)
    {
        var employee = await _authRepository.LoginEmployeeAsync(employeeLoginDTO);

        int expiresIn = int.Parse(_configuration["Jwt:ExpiresIn"] ?? throw new Exception("JWT 'ExpiresIn' is missing in configuration."));

        return new EmployeeLoginServiceResponse
        {
            Employee = employee,
            AccessToken = JwtUtils.GenerateToken(employee.EmployeeId, _configuration),
            ExpiresIn = expiresIn
        };
    }
}