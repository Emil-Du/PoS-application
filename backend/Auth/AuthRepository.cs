using backend.Database;
using backend.Auth;
using backend.Customers;
using backend.Employees;
using backend.Auth.Utils;
using Microsoft.EntityFrameworkCore;

public class AuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
        {
            _context = context;
        }
    public async Task<bool> EmployeeEmailExists(string email)
    {
        return await _context.Employees.AnyAsync(c => c.Email == email);
    }

    public async Task<Employee> RegisterEmployeeAsync(EmployeeRegistrationDTO employeeRegistrationDTO)
    {
        var salt = PasswordUtils.GenerateSalt();

        var newEmployee = new Employee
        {
            FirstName = employeeRegistrationDTO.FirstName,
            LastName = employeeRegistrationDTO.LastName,
            LocationId = employeeRegistrationDTO.LocationId,
            Email = employeeRegistrationDTO.Email,
            PhoneNumber = employeeRegistrationDTO.PhoneNumber,
            Salt = salt,
            PasswordHash = PasswordUtils.HashPassword(employeeRegistrationDTO.Password, salt) 
        };

        _context.Employees.Add(newEmployee);

        await _context.SaveChangesAsync();

        return newEmployee;
    }

    public async Task<EmployeeLoginResponseDTO> LoginEmployeeAsync(EmployeeLoginDTO employeeLoginDTO)
    {
        var data = await _context.Employees
            .Where(c => c.Email == employeeLoginDTO.Email)
            .Select(c => new
            {
                c.EmployeeId,
                c.FirstName,
                c.LastName,
                c.LocationId,
                c.Salt,
                c.PasswordHash,
                c.Status
            })
            .FirstOrDefaultAsync();

        if (data == null || data.PasswordHash != PasswordUtils.HashPassword(employeeLoginDTO.Password, data.Salt))
        {
            throw new IncorrectLoginDetailsException();
        }

        if (data.Status == EmployeeStatus.Inactive)
        {
            throw new InactiveStatusException();
        }

        return new EmployeeLoginResponseDTO
        {
            EmployeeId = data.EmployeeId,
            FirstName = data.FirstName,
            LastName = data.LastName,
            LocationId = data.LocationId
        };

    }
}