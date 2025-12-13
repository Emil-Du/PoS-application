namespace backend.Roles;

using backend.Mappings;

public class Role
{
    public int RoleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

}
