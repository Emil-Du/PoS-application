namespace backend.Services;

public class CreateServiceRequest
{
    public int ProductId { get; set; }
    public int LocationId { get; set; }
    public ServiceStatus Status { get; set; } = ServiceStatus.available;
    public int DurationMinutes { get; set; }

}