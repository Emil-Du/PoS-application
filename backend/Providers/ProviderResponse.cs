namespace backend.Providers;

public class ProviderResponse
{
    public int ProviderId { get; set; }
    public int EmployeeId { get; set; }
    public string Name { get; set; } = default!;
    public List<int> QualifiedServiceIds { get; set; } = new();
}
