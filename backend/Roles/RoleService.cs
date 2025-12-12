namespace backend.Roles;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;

    public RoleService(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Role>> GetRolesAsync()
    {
        return await _repository.GetRolesAsync();
    }

    public async Task<Role?> GetRoleByIdAsync(int roleId)
    {
        return await _repository.GetRoleByIdAsync(roleId);
    }

    public async Task<Role> CreateRoleAsync(RoleCreateRequest roleCreateRequest)
    {
        return await _repository.CreateRoleAsync(roleCreateRequest);
    }

    public async Task<Role?> UpdateRoleByIdAsync(int roleId, RoleUpdateRequest roleUpdateRequest)
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

    public async Task<bool> RemoveRoleFromEmployeeAsync(int roleId, int employeeId)
    {
        return await _repository.RemoveRoleFromEmployeeAsync(roleId, employeeId);
    }
}
