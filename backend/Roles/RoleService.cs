namespace backend.Roles;
using backend.Mappings;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;

    public RoleService(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<RoleDTO>> GetRolesAsync()
    {
        var roles = await _repository.GetRolesAsync();
        
        return roles.Select(MapToDto).ToList();
    }

    public async Task<RoleDTO?> GetRoleByIdAsync(int roleId)
    {
        var role = await _repository.GetRoleByIdAsync(roleId);
        return role == null ? null : MapToDto(role);
    }

    public async Task<RoleDTO> CreateRoleAsync(RoleCreateRequest roleCreateRequest)
    {
        var role = await _repository.CreateRoleAsync(roleCreateRequest);
        return MapToDto(role);
    }

    public async Task<bool> UpdateRoleByIdAsync(int roleId, RoleUpdateRequest roleUpdateRequest)
    {
        return await _repository.UpdateRoleByIdAsync(roleId, roleUpdateRequest);
    }

    public async Task<bool> DeleteRoleByIdAsync(int roleId)
    {
        return await _repository.DeleteRoleByIdAsync(roleId);
    }

    public async Task<bool> AssignRoleToEmployeeAsync(int roleId, int employeeId)
    {
        return await _repository.AssignRoleToEmployeeAsync(roleId, employeeId);
    }

    private static RoleDTO MapToDto(Role role)
    {
        return new RoleDTO
        {
            RoleId = role.RoleId,
            Name = role.Name,
            Flags = role.RolePermissions
                .Select(rp => rp.Permission.Name)
                .ToList()
        };
    }

}
