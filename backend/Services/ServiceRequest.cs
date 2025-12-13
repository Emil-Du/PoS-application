namespace backend.Services;

public class ServiceRequest
{
    public int ProductId { get; set; }
    public int CompanyId { get; set; }
    public string Title { get; set; } = default!;
    public BasePrice BasePrice { get; set; } = default!;
    public int DurationMinutes { get; set; }
    public ServiceStatus Status { get; set; }

}