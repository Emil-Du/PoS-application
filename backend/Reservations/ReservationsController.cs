using backend.Reservations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class ReservationsController : ControllerBase
{
    private readonly IReservationsService _reservationsService;

    public ReservationsController(IReservationsService reservationsService)
    {
        _reservationsService = reservationsService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] ReservationCreationDTO reservationCreationDTO)
    {
        return Ok(await _reservationsService.Create(reservationCreationDTO));
    }

    [HttpGet]
    public async Task<IActionResult> GetReservations([FromQuery] GetReservationsDTO getReservationsDTO)
    {
        return Ok(await _reservationsService.Get(getReservationsDTO));
    }

    [HttpGet("{reservationId}")]
    public async Task<IActionResult> GetReservation([FromRoute] int reservationId)
    {
        try
        {
            return Ok(await _reservationsService.GetSingle(reservationId));
        }
        catch (ReservationNotFoundException e)
        {
            return StatusCode(404, e.Message);
        }

    }

    [HttpPatch("{reservationId}")]
    public async Task<IActionResult> EditReservation([FromRoute] int reservationId, [FromBody] EditReservationDTO editReservationDTO)
    {
        try
        {
            return Ok(await _reservationsService.Edit(reservationId, editReservationDTO));
        }
        catch (ReservationNotFoundException e)
        {
            return StatusCode(404, e.Message);
        }
    }
}