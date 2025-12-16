using backend.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using backend.Roles;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("RegisterEmployee")]
    [Authorize(Roles = Roles.SuperAdmin + "," + Roles.Manager)]
    public async Task<IActionResult> RegisterEmployee([FromBody] EmployeeRegistrationDTO employeeRegistrationDTO)
    {
        try
        {
            var response_details = await _authService.RegisterEmployee(employeeRegistrationDTO);

            var responseDTO = new EmployeeRegistrationResponseDTO
            {
                EmployeeId = response_details.EmployeeId,
                FirstName = response_details.FirstName,
                LastName = response_details.LastName,
                LocationId = response_details.LocationId,
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

    [HttpPost("LoginEmployee")]
    public async Task<IActionResult> LoginEmployee([FromBody] EmployeeLoginDTO employeeLoginDTO)
    {
        try
        {
            var serviceResponse = await _authService.LoginEmployee(employeeLoginDTO);

            Response.Cookies.Append(
                "accessToken",
                serviceResponse.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,     // true: HTTPS; false: HTTP, LOCALHOST
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddSeconds(serviceResponse.ExpiresIn),
                    Path = "/"
                }
            );

            return Ok(serviceResponse.Employee);
        }
        catch (IncorrectLoginDetailsException e)
        {   
            return StatusCode(401, e.Message);
        }
        catch (InactiveStatusException e)
        {   
            return StatusCode(403, e.Message);
        }

    }

    [HttpPost("LogoutEmployee")]
    [Authorize(Roles = Roles.SuperAdmin + "," + Roles.Manager + "," + Roles.Employee)]
    public IActionResult LogoutEmployee()
    {
        Response.Cookies.Append(
            "accessToken",
            "",
            new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(-1),
                Path = "/"
            }
        );

        return Ok();
    }

}