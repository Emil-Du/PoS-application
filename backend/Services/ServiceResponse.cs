namespace backend.Services;

public class ServiceResponse
{
    public int ServiceId { get; set; }
    public int ProductId { get; set; }
    public int CompanyId { get; set; }
    public string Title { get; set; } = default!; // name of the product/service
    public BasePrice BasePrice { get; set; } = default!;
    public int DurationMinutes { get; set; }
    public ServiceStatus Status { get; set; }

}