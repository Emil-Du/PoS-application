using backend.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetEmployeesAsync(EmployeeQuery query)
        {
            var dbQuery = _context.Employees.AsQueryable();

            if (query.LocationId.HasValue)
            {
                dbQuery = dbQuery.Where(emp => emp.LocationId == query.LocationId.Value);
            }

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                dbQuery = dbQuery.Where(emp =>
                    emp.FirstName.Contains(query.Search) ||
                    emp.LastName.Contains(query.Search) ||
                    emp.Email.Contains(query.Search) ||
                    emp.PhoneNumber.Contains(query.Search));
            }

            return await dbQuery
                .OrderBy(emp => emp.EmployeeId)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(EmployeeQuery query)
        {
            var dbQuery = _context.Employees.AsQueryable();

            if (query.LocationId.HasValue)
            {
                dbQuery = dbQuery.Where(emp => emp.LocationId == query.LocationId.Value);
            }

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                dbQuery = dbQuery.Where(emp =>
                    emp.FirstName.Contains(query.Search) ||
                    emp.LastName.Contains(query.Search) ||
                    emp.Email.Contains(query.Search) ||
                    emp.PhoneNumber.Contains(query.Search));
            }

            return await dbQuery.CountAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
        {
            return await _context.Employees.FindAsync(employeeId);
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> UpdateEmployeeByIdAsync(Employee employee)
        {
            var existing = await _context.Employees.FindAsync(employee.EmployeeId);
            if (existing == null) return false;

            existing.FirstName = employee.FirstName;
            existing.LastName = employee.LastName;
            existing.LocationId = employee.LocationId;
            existing.Email = employee.Email;
            existing.PhoneNumber = employee.PhoneNumber;
            existing.Status = employee.Status;

            await _context.SaveChangesAsync();
            return true;
        }
    }

}