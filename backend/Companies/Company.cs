namespace backend.Companies;

public class Company
{
    public int CompanyId { get; set; }
    public string Name { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
}