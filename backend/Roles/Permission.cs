namespace backend.Roles;

using backend.Mappings;

public class Permission
{
    public int PermissionId { get; set; }
    public PermissionFlag Name { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
