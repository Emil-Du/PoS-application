namespace backend.Roles;

public class RoleCreateRequest
{
    public string Name { get; set; } = string.Empty;
    public List<string> Flags { get; set; } = new();
}
