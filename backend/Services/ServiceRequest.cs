namespace backend.Services;

public class ServiceRequest
{
    public string Title { get; set; } = default!;
    public BasePrice BasePrice { get; set; } = default!;
    public int DurationMinutes { get; set; }
    public ServiceStatus Status { get; set; }

}