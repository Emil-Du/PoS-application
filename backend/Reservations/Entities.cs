using backend.Customers;
using backend.Employees;
using backend.Locations;
using backend.Services;

namespace backend.Reservations;

public enum ReservationStatus
{
    Active,
    Cancelled,
    Completed
}

public class Reservation
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    public int LocationId { get; set; }
    public Location Location { get; set; } = null!;
    public int ProviderId { get; set; }
    public Employee Provider { get; set; } = null!;
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public long ReservationTime { get; set; }
    public long AppointmentTime { get; set; }
    public ReservationStatus Status { get; set; } = ReservationStatus.Active;
}