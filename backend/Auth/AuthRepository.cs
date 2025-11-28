using backend.Database;
using backend.Auth;
using backend.Auth.Utils;
using Microsoft.EntityFrameworkCore;
public class AuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

    public async Task<bool> EmailExists(string email)
    {
        return await _context.Customers.AnyAsync(c => c.Email == email);
    }

    public async Task<Customer> RegisterCustomerAsync(RegistrationDTO registrationDTO)
    {
        var salt = PasswordUtils.GenerateSalt();

        var newCustomer = new Customer
        {
            Name = registrationDTO.Name,
            Email = registrationDTO.Email,
            PhoneNumber = registrationDTO.PhoneNumber,
            Salt = salt,
            PasswordHash = PasswordUtils.HashPassword(registrationDTO.Password, salt) 
        };

        _context.Customers.Add(newCustomer);

        await _context.SaveChangesAsync();

        return newCustomer;
    }

    public async Task<int> LoginCustomerAsync(LoginDTO loginDTO)
    {
        var data = await _context.Customers
            .Where(c => c.Email == loginDTO.Email)
            .Select(c => new
            {
                Id = c.CustomerId,
                Salt = c.Salt,
                PasswordHash = c.PasswordHash
            })
            .FirstOrDefaultAsync();

        if (data == null || data.PasswordHash != PasswordUtils.HashPassword(loginDTO.Password, data.Salt))
        {
            throw new IncorrectLoginDetailsException();
        }

        return data.Id;

    }
}