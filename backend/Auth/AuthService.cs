using backend.Auth;
using backend.Customers;
using backend.Employees;
using backend.Roles;

public class AuthService : IAuthService
{
    private readonly AuthRepository _authRepository;
    private readonly IConfiguration _configuration;
    private readonly IRoleService _roleService;

    public AuthService(AuthRepository authRepository, IConfiguration configuration, IRoleService roleService)
    {
        _authRepository = authRepository;
        _configuration = configuration;
        _roleService = roleService;
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

        int? roleId = (await _roleService.GetRoleIdByEmployeeIdAsync(employee.EmployeeId))?.RoleId;

        if (roleId == null)
        {
            throw new Exception("RoleId not found by employeeId");
        }

        var roleInfo = await _roleService.GetRoleByIdAsync(roleId.Value);

        if (roleInfo == null)
        {
            throw new Exception("Role name wasnt found via provided roleId.");
        }

        return new EmployeeLoginServiceResponse
        {
            Employee = employee,
            AccessToken = JwtUtils.GenerateToken(roleInfo.Name, _configuration),
            ExpiresIn = expiresIn
        };
    }
}