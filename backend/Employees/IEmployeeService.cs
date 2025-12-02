using backend.Common;

namespace backend.Employees;

public interface IEmployeeService
{
    Task<PaginatedResponse<EmployeeResponse>> GetEmployeesAsync(EmployeeQuery query);
    Task<EmployeeResponse?> GetEmployeeByIdAsync(int employeeId);
    Task<EmployeeResponse> CreateEmployeeAsync(EmployeeRequest request);
    Task<bool> UpdateEmployeeByIdAsync(int employeeId, EmployeeRequest request);
}