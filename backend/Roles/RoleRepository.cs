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

    public async Task<IEnumerable<Role>> GetRolesAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<Role?> GetRoleByIdAsync(int roleId)
    {
        return await _context.Roles.FindAsync(roleId);
    }

    public async Task<Role> CreateRoleAsync(RoleDTO roleDTO)
    {
        var role = new Role
        {
                Name = roleDTO.Name
        };

        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<bool> UpdateRoleByIdAsync(int roleId, RoleDTO roleDTO)
    {
        var role = await _context.Roles.FindAsync(roleId);
        if (role == null)
        {
            return false;
        }

        role.Name = roleDTO.Name;

        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
        return true;
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

        employee.RoleId = null;
        await _context.SaveChangesAsync();
        return true;
    }
}