using backend.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.Roles;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Role>> GetRolesAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<Role?> GetRoleByIdAsync(int roleId)
    {
        return await _context.Roles.FindAsync(roleId);
    }

    public async Task<Role> CreateRoleAsync(RoleCreateRequest roleCreateRequest)
    {
        var role = new Role
        {
            Name = roleCreateRequest.Name,
            Flags = roleCreateRequest.Flags
        };

        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<Role?> UpdateRoleByIdAsync(int roleId, RoleUpdateRequest roleUpdateRequest)
    {
        var role = await _context.Roles.FindAsync(roleId);
        if (role == null)
        {
            return null;
        }

        if (roleUpdateRequest.Name != null)
        {
            role.Name = roleUpdateRequest.Name;
        }
        if (roleUpdateRequest.Flags != null)
        {
            role.Flags = roleUpdateRequest.Flags;
        }

        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<bool> DeleteRoleByIdAsync(int roleId)
    {
        var role = await _context.Roles.FindAsync(roleId);
        if (role == null)
        {
            return false;
        }

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AssignRoleToEmployeeAsync(int roleId, int employeeId)
    {
        var role = await _context.Roles.FindAsync(roleId);
        var employee = await _context.EmployeeRoles.FindAsync(employeeId);
        if (role == null || employee == null)
        {
            return false;
        }

        employee.RoleId = roleId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveRoleFromEmployeeAsync(int roleId, int employeeId)
    {
        var role = await _context.Roles.FindAsync(roleId);
        var employee = await _context.EmployeeRoles.FindAsync(employeeId);
        if (role == null || employee == null)
        {
            return false;
        }

        if (employee.RoleId != roleId)
        {
            return false;
        }

        employee.RoleId = 0;
        await _context.SaveChangesAsync();
        return true;
    }
}