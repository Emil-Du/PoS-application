namespace backend.Roles;

public interface IRoleService
{
    Task<List<RoleDTO>> GetRolesAsync();
    Task<RoleDTO?> GetRoleByIdAsync(int roleId);
    Task<RoleDTO> CreateRoleAsync(RoleCreateRequest roleCreateRequest);
    Task<bool> UpdateRoleByIdAsync(int roleId, RoleUpdateRequest roleUpdateRequest);
    Task<bool> DeleteRoleByIdAsync(int roleId);

    Task<bool> AssignRoleToEmployeeAsync(int roleId, int employeeId);
}
