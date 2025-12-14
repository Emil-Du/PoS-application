namespace backend.Employees
{
    public class EmployeeQuery
    {
        public int? EmployeeId { get; set; }
        public EmployeeStatus? Status { get; set; }
        public int? LocationId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public string? Search { get; set; }
    }
}