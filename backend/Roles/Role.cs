namespace backend.Roles;

public class Role
{
    public int RoleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<PermissionFlag> Flags { get; set; } = new();
}
