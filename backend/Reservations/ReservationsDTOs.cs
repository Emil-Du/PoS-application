namespace backend.Reservations;

// To avoid reusing database entity
public class ReservationDTO
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public int LocationId { get; set; }
    public int ProviderId { get; set; }
    public int CustomerId { get; set; }
    public long ReservationTime { get; set; }
    public long AppointmentTime { get; set; }
    public ReservationStatus Status { get; set; }
}


public class ReservationCreationDTO
{
    public int ServiceId { get; set; }
    public int LocationId { get; set; }
    public int ProviderId { get; set; }
    public int CustomerId { get; set; }
    public long AppointmentTime { get; set; }

}

public class ReservationCreationResponseDTO
{
    public ReservationDTO Reservation { get; set; } = new();
}

public class GetReservationsDTO
{
    public int? CustomerId { get; set; }
    public int? ServiceId { get; set; }
    public int? ProviderId { get; set; }
    public long? From { get; set; }
    public long? To { get; set; }

}

public class GetReservationsResponseDTO
{
    public List<ReservationDTO> Reservations { get; set; } = new();
}

public class GetReservationResponseDTO
{
    public ReservationDTO Reservation { get; set; } = new();

}

public class EditReservationDTO
{
    public long AppointmentTime { get; set; }
    public int ProviderId { get; set; }
    public ReservationStatus Status { get; set; }

}

public class EditReservationResponseDTO
{
    public ReservationDTO Reservation { get; set; } = new();

}