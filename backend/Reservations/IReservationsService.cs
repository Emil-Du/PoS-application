using backend.Reservations;

public interface IReservationsService
{
    Task<ReservationCreationResponseDTO> Create(ReservationCreationDTO reservationCreationDTO);
    Task<GetReservationsResponseDTO> Get(GetReservationsDTO getReservationsDTO);
    Task<GetReservationResponseDTO> GetSingle(int reservationId);
    Task<EditReservationResponseDTO> Edit(int reservationId, EditReservationDTO editReservationDTO);
}
