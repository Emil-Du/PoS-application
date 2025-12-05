using backend.Employees;
using backend.Services;

namespace backend.Mappings
{
    public class EmployeeServiceQualification
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;

        public int ServiceId { get; set; }
        public Service Service { get; set; } = default!;
    }
}

