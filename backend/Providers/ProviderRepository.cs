using backend.Database;
using backend.Employees;
using backend.Mappings;
using Microsoft.EntityFrameworkCore;

namespace backend.Providers
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly AppDbContext _context;

        public ProviderRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Employee?> GetProviderByIdAsync(int providerId)
        {
            return _context.Employees
                .Include(e => e.Qualifications)
                .FirstOrDefaultAsync(e => e.EmployeeId == providerId);
        }

        public async Task<List<Employee>> GetProvidersAsync(ProviderQuery query)
        {
            var dbQuery = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                dbQuery = dbQuery.Where(emp =>
                    emp.FirstName.Contains(query.Search) ||
                    emp.LastName.Contains(query.Search));
            }

            return await dbQuery
                .OrderBy(emp => emp.EmployeeId)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(ProviderQuery query)
        {
            var dbQuery = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                dbQuery = dbQuery.Where(emp =>
                    emp.FirstName.Contains(query.Search) ||
                    emp.LastName.Contains(query.Search));
            }

            return await dbQuery.CountAsync();
        }


        public async Task<bool> UpdateProviderByIdAsync(int providerId, ProviderRequest request)
        {
            var employee = await _context.Employees
                .Include(e => e.Qualifications)
                .FirstOrDefaultAsync(e => e.EmployeeId == providerId);

            if (employee == null)
                return false;

            var toRemove = employee.Qualifications
                .Where(q => request.QualifiedServiceIdsToRemove.Contains(q.ServiceId))
                .ToList();

            foreach (var q in toRemove)
            {
                employee.Qualifications.Remove(q);
            }

            foreach (var id in request.QualifiedServiceIdsToAdd)
            {
                if (!employee.Qualifications.Any(q => q.ServiceId == id))
                {
                    employee.Qualifications.Add(new EmployeeServiceQualification
                    {
                        EmployeeId = providerId,
                        ServiceId = id
                    });
                }
            }


            await _context.SaveChangesAsync();
            return true;
        }
    }

}