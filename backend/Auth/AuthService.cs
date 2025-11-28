using backend.Auth;

public class AuthService : IAuthService
{
    private readonly AuthRepository _authRepository;
    private readonly IConfiguration _configuration;

    public AuthService(AuthRepository authRepository, IConfiguration configuration)
    {
        _authRepository = authRepository;
        _configuration = configuration;
    }

    public async Task<Customer> Register(RegistrationDTO registrationDTO)
    {
        if (await _authRepository.EmailExists(registrationDTO.Email))
        {
            throw new EmailAlreadyExistsException();
        }

        var newCustomer = await _authRepository.RegisterCustomerAsync(registrationDTO);

        return newCustomer;
    }

    public async Task<LoginResponseDTO> Login(LoginDTO loginDTO)
    {
        int loggedInCustomerId = await _authRepository.LoginCustomerAsync(loginDTO);

        int expiresIn = int.Parse(_configuration["Jwt:ExpiresIn"] ?? throw new Exception("JWT 'ExpiresIn' is missing in configuration."));

        return new LoginResponseDTO
            {
                AccessToken = JwtUtils.GenerateToken(loggedInCustomerId, _configuration),
                TokenType = "Bearer",
                ExpiresIn = expiresIn
            };
    }
}