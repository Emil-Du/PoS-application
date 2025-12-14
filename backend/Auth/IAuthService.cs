using backend.Auth;
using backend.Customers;
using backend.Employees;

public interface IAuthService
{
    Task<Customer> RegisterCustomer(CustomerRegistrationDTO customerRegistrationDTO);

    Task<CustomerLoginResponseDTO> LoginCustomer(CustomerLoginDTO customerLoginDTO);
    Task<Employee> RegisterEmployee(EmployeeRegistrationDTO employeeRegistrationDTO);
    Task<EmployeeLoginServiceResponse> LoginEmployee(EmployeeLoginDTO employeeLoginDTO);
}
