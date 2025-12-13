namespace backend.Roles;

public interface IRoleRepository
{
    Task<List<Role>> GetRolesAsync();
    Task<Role?> GetRoleByIdAsync(int roleId);
    Task<Role> CreateRoleAsync(RoleCreateRequest roleCreateRequest);
    Task<Role?> UpdateRoleByIdAsync(int roleId, RoleUpdateRequest roleUpdateRequest);
    Task<bool> DeleteRoleByIdAsync(int roleId);

    Task<bool> AssignRoleToEmployeeAsync(int roleId, int employeeId);
    Task<bool> RemoveRoleFromEmployeeAsync(int roleId, int employeeId);
}
