namespace backend.Reservations;

public class ReservationNotFoundException : Exception
{
    private const string ExceptionMessage = "Requested reservation details were not found or do not exist.";

    public ReservationNotFoundException() : base(ExceptionMessage) {}
}
