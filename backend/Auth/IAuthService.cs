using backend.Auth;
using backend.Customers;
using backend.Employees;

public interface IAuthService
{
    Task<Employee> RegisterEmployee(EmployeeRegistrationDTO employeeRegistrationDTO);
    Task<EmployeeLoginServiceResponse> LoginEmployee(EmployeeLoginDTO employeeLoginDTO);
}
