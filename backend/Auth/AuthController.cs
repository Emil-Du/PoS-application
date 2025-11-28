using backend.Auth;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegistrationDTO registrationDTO)
    {
        try
        {
            var response_details = await _authService.Register(registrationDTO);

            var responseDTO = new RegistrationResponseDTO
            {
                CustomerId = response_details.CustomerId,
                Name = response_details.Name,
                Email = response_details.Email,
                PhoneNumber = response_details.PhoneNumber
            };  
            
            return Ok(responseDTO);
        }
        catch (EmailAlreadyExistsException e)
        {   
            return StatusCode(409, e.Message);
        }

    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        try
        {
            var responseDTO = await _authService.Login(loginDTO);
            
            return Ok(responseDTO);
        }
        catch (IncorrectLoginDetailsException e)
        {   
            return StatusCode(401, e.Message);
        }

    }
}