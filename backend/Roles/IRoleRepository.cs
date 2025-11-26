namespace backend.Roles;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetRolesAsync();
    Task<Role> CreateRoleAsync(RoleDTO roleDTO);
    Task<Role?> GetRoleByIdAsync(int roleId);
    Task<bool> UpdateRoleByIdAsync(int roleId, RoleDTO roleDTO);
    Task<bool> DeleteRoleByIdAsync(int roleId);

    Task<bool> AssignRoleToEmployeeAsync(int roleId, int employeeId);
    Task<bool> RemoveRoleFromEmployeeAsync(int roleId, int employeeId);
}
