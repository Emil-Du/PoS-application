using backend.Auth;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost("RegisterCustomer")]
    public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRegistrationDTO customerRegistrationDTO)
    {
        try
        {
            var response_details = await _authService.RegisterCustomer(customerRegistrationDTO);

            var responseDTO = new CustomerRegistrationResponseDTO
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

    [HttpPost("LoginCustomer")]
    public async Task<IActionResult> LoginCustomer([FromBody] CustomerLoginDTO customerLoginDTO)
    {
        try
        {
            var responseDTO = await _authService.LoginCustomer(customerLoginDTO);
            
            return Ok(responseDTO);
        }
        catch (IncorrectLoginDetailsException e)
        {   
            return StatusCode(401, e.Message);
        }

    }

    [HttpPost("RegisterEmployee")]
    //[Authorize]  // Only need authorization if doing from frontend, if doing from swagger than this needs to be disabled
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
}