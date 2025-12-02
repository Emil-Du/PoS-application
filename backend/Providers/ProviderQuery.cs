namespace backend.Providers;

public class ProviderQuery
{
    public int? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public int? ServiceId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public string? Search { get; set; }
}