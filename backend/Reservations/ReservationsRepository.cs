using backend.Database;
using backend.Reservations;
using Microsoft.EntityFrameworkCore;
public class ReservationsRepository
{
    private readonly AppDbContext _context;

    public ReservationsRepository(AppDbContext context)
        {
            _context = context;
        }

    public async Task<Reservation> CreateReservationAsync(Reservation reservation)
    {
        _context.Reservations.Add(reservation);

        await _context.SaveChangesAsync();

        return reservation;
    }

    public async Task<GetReservationsResponseDTO> GetReservationsAsync(GetReservationsDTO getReservationsDTO)
    {
        var matchingReservations =  await _context.Reservations
            .Where(r =>
                (r.CustomerId == getReservationsDTO.CustomerId) &&
                (r.ServiceId == getReservationsDTO.ServiceId) &&
                (r.ProviderId == getReservationsDTO.ProviderId) &&
                r.AppointmentTime >= getReservationsDTO.From &&
                r.AppointmentTime <= getReservationsDTO.To)
            .ToListAsync();

        return new GetReservationsResponseDTO
        {
            Reservations = matchingReservations.Select(r => new ReservationDTO
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                ServiceId = r.ServiceId,
                ProviderId = r.ProviderId,
                LocationId = r.LocationId,
                ReservationTime = r.ReservationTime,
                AppointmentTime = r.AppointmentTime,
                Status = r.Status
            }).ToList()
        };
    }

    public async Task<GetReservationResponseDTO> GetReservationAsync(int reservationId)
    {
        var reservationDetails = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);

        if (reservationDetails == null)
        {
            throw new ReservationNotFoundException();
        }
        
        return new GetReservationResponseDTO
        {
            Reservation = 
            {
                Id = reservationDetails.Id,
                CustomerId = reservationDetails.CustomerId,
                ServiceId = reservationDetails.ServiceId,
                ProviderId = reservationDetails.ProviderId,
                LocationId = reservationDetails.LocationId,
                ReservationTime = reservationDetails.ReservationTime,
                AppointmentTime = reservationDetails.AppointmentTime,
                Status = reservationDetails.Status
            }
        };
    }

    public async Task<EditReservationResponseDTO> EditReservationAsync(int reservationId, EditReservationDTO editReservationDTO)
    {
        var reservationDetails = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);

        if (reservationDetails == null)
        {
            throw new ReservationNotFoundException();
        }

        reservationDetails.AppointmentTime = editReservationDTO.AppointmentTime;
        reservationDetails.ProviderId = editReservationDTO.ProviderId;
        reservationDetails.Status = editReservationDTO.Status;

        await _context.SaveChangesAsync();
        
        return new EditReservationResponseDTO
        {
            Reservation = 
            {
                Id = reservationDetails.Id,
                CustomerId = reservationDetails.CustomerId,
                ServiceId = reservationDetails.ServiceId,
                ProviderId = reservationDetails.ProviderId,
                LocationId = reservationDetails.LocationId,
                ReservationTime = reservationDetails.ReservationTime,
                AppointmentTime = reservationDetails.AppointmentTime,
                Status = reservationDetails.Status
            }
        };
    }
}