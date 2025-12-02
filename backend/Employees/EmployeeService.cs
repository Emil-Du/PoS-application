using backend.Common;
namespace backend.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<EmployeeResponse>> GetEmployeesAsync(EmployeeQuery query)
        {
            var employees = await _repository.GetEmployeesAsync(query);
            var total = await _repository.GetTotalCountAsync(query);

            var data = employees.Select(emp => new EmployeeResponse
            {
                EmployeeId = emp.EmployeeId,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                LocationId = emp.LocationId,
                PhoneNumber = emp.PhoneNumber,
                Email = emp.Email,
                Status = emp.Status
            }).ToList();


            return new PaginatedResponse<EmployeeResponse>
            {
                Page = query.Page,
                PageSize = query.PageSize,
                Total = total,
                Data = data
            };
        }

        public async Task<EmployeeResponse?> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _repository.GetEmployeeByIdAsync(employeeId);
            if (employee == null)
            {
                return null;
            }

            return new EmployeeResponse
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                LocationId = employee.LocationId,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                Status = employee.Status
            };
        }

        public async Task<EmployeeResponse> CreateEmployeeAsync(EmployeeRequest request)
        {
            var employee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                LocationId = request.LocationId,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Status = request.Status
            };

            var created = await _repository.CreateEmployeeAsync(employee);

            return new EmployeeResponse
            {
                EmployeeId = created.EmployeeId,
                FirstName = created.FirstName,
                LastName = created.LastName,
                LocationId = created.LocationId,
                PhoneNumber = created.PhoneNumber,
                Email = created.Email,
                Status = created.Status
            };
        }

        public async Task<bool> UpdateEmployeeByIdAsync(int employeeId, EmployeeRequest request)
        {
            var employee = new Employee
            {
                EmployeeId = employeeId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                LocationId = request.LocationId,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Status = request.Status
            };

            return await _repository.UpdateEmployeeByIdAsync(employee);
        }
    }
}