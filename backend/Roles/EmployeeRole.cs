namespace backend.Roles;

public class EmployeeRole
{
    public int EmployeeId { get; set; }
    public int RoleId { get; set; }

    public Role Role { get; set; } = null!;
}
