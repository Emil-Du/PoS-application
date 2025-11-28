using backend.Auth;

public interface IAuthService
{
    Task<Customer> Register(RegistrationDTO registrationDTO);

    Task<LoginResponseDTO> Login(LoginDTO loginDTO);
}
