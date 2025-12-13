using backend.Reservations;

namespace backend.Customers;

public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public ICollection<Reservation> Reservations { get; set; } = [];

}
