using backend.Auth;

public interface IAuthService
{
    Task<Customer> RegisterCustomer(CustomerRegistrationDTO customerRegistrationDTO);

    Task<CustomerLoginResponseDTO> LoginCustomer(CustomerLoginDTO customerLoginDTO);
    Task<Employee> RegisterEmployee(EmployeeRegistrationDTO employeeRegistrationDTO);
    Task<EmployeeLoginResponseDTO> LoginEmployee(EmployeeLoginDTO employeeLoginDTO);
}
