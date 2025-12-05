using backend.Reservations;

public class ReservationsService : IReservationsService
{
    private readonly ReservationsRepository _reservationsRepository;

    public ReservationsService(ReservationsRepository reservationsRepository)
    {
        _reservationsRepository = reservationsRepository;
    }

    public async Task<ReservationCreationResponseDTO> Create(ReservationCreationDTO reservationCreationDTO)
    {
        var reservation = new Reservation
            {
                ServiceId = reservationCreationDTO.ServiceId,
                LocationId = reservationCreationDTO.LocationId,
                ProviderId = reservationCreationDTO.ProviderId,
                CustomerId = reservationCreationDTO.CustomerId,
                ReservationTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                AppointmentTime = reservationCreationDTO.AppointmentTime
            };

        var completedReservation =  await _reservationsRepository.CreateReservationAsync(reservation);

        return new ReservationCreationResponseDTO
        {
            Reservation = 
            {
                Id = completedReservation.Id,
                CustomerId = completedReservation.CustomerId,
                ServiceId = completedReservation.ServiceId,
                ProviderId = completedReservation.ProviderId,
                LocationId = completedReservation.LocationId,
                ReservationTime = completedReservation.ReservationTime,
                AppointmentTime = completedReservation.AppointmentTime,
                Status = completedReservation.Status
            }
        };
    }

    public async Task<GetReservationsResponseDTO> Get(GetReservationsDTO getReservationsDTO)
    {
        return await _reservationsRepository.GetReservationsAsync(getReservationsDTO);
    }

    public async Task<GetReservationResponseDTO> GetSingle(int reservationId)
    {
        return await _reservationsRepository.GetReservationAsync(reservationId);
    }

    public async Task<EditReservationResponseDTO> Edit(int reservationId, EditReservationDTO editReservationDTO)
    {
        return await _reservationsRepository.EditReservationAsync(reservationId, editReservationDTO);
    }
}