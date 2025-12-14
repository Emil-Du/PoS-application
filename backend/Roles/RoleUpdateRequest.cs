namespace backend.Roles;

public class RoleUpdateRequest
{
    public string? Name { get; set; }
    public List<PermissionFlag>? Flags { get; set; }
}
