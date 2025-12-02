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

    public async Task<bool> CustomerEmailExists(string email)
    {
        return await _context.Customers.AnyAsync(c => c.Email == email);
    }

    public async Task<bool> EmployeeEmailExists(string email)
    {
        return await _context.Employees.AnyAsync(c => c.Email == email);
    }

    public async Task<Customer> RegisterCustomerAsync(CustomerRegistrationDTO customerRegistrationDTO)
    {
        var salt = PasswordUtils.GenerateSalt();

        var newCustomer = new Customer
        {
            Name = customerRegistrationDTO.Name,
            Email = customerRegistrationDTO.Email,
            PhoneNumber = customerRegistrationDTO.PhoneNumber,
            Salt = salt,
            PasswordHash = PasswordUtils.HashPassword(customerRegistrationDTO.Password, salt) 
        };

        _context.Customers.Add(newCustomer);

        await _context.SaveChangesAsync();

        return newCustomer;
    }

    public async Task<int> LoginCustomerAsync(CustomerLoginDTO customerLoginDTO)
    {
        var data = await _context.Customers
            .Where(c => c.Email == customerLoginDTO.Email)
            .Select(c => new
            {
                Id = c.CustomerId,
                Salt = c.Salt,
                PasswordHash = c.PasswordHash
            })
            .FirstOrDefaultAsync();

        if (data == null || data.PasswordHash != PasswordUtils.HashPassword(customerLoginDTO.Password, data.Salt))
        {
            throw new IncorrectLoginDetailsException();
        }

        return data.Id;

    }

    public async Task<Employee> RegisterEmployeeAsync(EmployeeRegistrationDTO employeeRegistrationDTO)
    {
        var salt = PasswordUtils.GenerateSalt();

        var newEmployee = new Employee
        {
            FirstName = employeeRegistrationDTO.FirstName,
            LastName = employeeRegistrationDTO.LastName,
            LocationId = employeeRegistrationDTO.EmploymentLocationId,
            Email = employeeRegistrationDTO.Email,
            PhoneNumber = employeeRegistrationDTO.PhoneNumber,
            Salt = salt,
            PasswordHash = PasswordUtils.HashPassword(employeeRegistrationDTO.Password, salt) 
        };

        _context.Employees.Add(newEmployee);

        await _context.SaveChangesAsync();

        return newEmployee;
    }

    public async Task<int> LoginEmployeeAsync(EmployeeLoginDTO employeeLoginDTO)
    {
        var data = await _context.Employees
            .Where(c => c.Email == employeeLoginDTO.Email)
            .Select(c => new
            {
                Id = c.EmployeeId,
                Salt = c.Salt,
                PasswordHash = c.PasswordHash,
                Status = c.Status
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

        return data.Id;

    }
}