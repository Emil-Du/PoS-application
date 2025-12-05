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
    public int LocationId { get; set; }
    public int ProviderId { get; set; }
    public int CustomerId { get; set; }
    public long ReservationTime {get; set; }
    public long AppointmentTime {get; set; }
    public ReservationStatus Status { get; set; } = ReservationStatus.Active;
}