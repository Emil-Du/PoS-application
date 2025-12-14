namespace backend.Roles;

public class RoleCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public List<PermissionFlag> Flags { get; set; } = new();
}
