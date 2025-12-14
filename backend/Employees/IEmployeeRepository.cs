using System.Collections;

namespace backend.Employees;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetEmployeesAsync(EmployeeQuery query);
    Task<int> GetTotalCountAsync(EmployeeQuery query);
    Task<Employee?> GetEmployeeByIdAsync(int employeeId);
    Task<Employee> CreateEmployeeAsync(Employee employee);
    Task<bool> UpdateEmployeeByIdAsync(Employee employee);
}