namespace backend.Roles;

public class RoleUpdateRequest
{
    public string? Name { get; set; }
    public List<string>? Flags { get; set; }
}
