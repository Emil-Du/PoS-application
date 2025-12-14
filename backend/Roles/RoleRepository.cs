using backend.Database;
using Microsoft.EntityFrameworkCore;
using backend.Mappings;

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
        return await _context.Roles
        .Include(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
        .ToListAsync();
    }

    public async Task<Role?> GetRoleByIdAsync(int roleId)
    {
        return await _context.Roles
        .Include(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
        .FirstOrDefaultAsync(r => r.RoleId == roleId);
    }

    public async Task<Role> CreateRoleAsync(RoleCreateRequest roleCreateRequest)
    {
        var role = new Role
        {
                Name = roleCreateRequest.Name,
        };

        foreach (var flag in roleCreateRequest.Flags)
    {
        var permission = await _context.Permissions
            .SingleAsync(p => p.Name == flag);

        role.RolePermissions.Add(new RolePermission
        {
            Permission = permission
        });
    }

        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<bool> UpdateRoleByIdAsync(int roleId, RoleUpdateRequest roleUpdateRequest)
    {
        var role = await _context.Roles
        .Include(r => r.RolePermissions)
        .FirstOrDefaultAsync(r => r.RoleId == roleId);

        if (role == null)
        {
            return false;
        }

        if(roleUpdateRequest.Name != null)
        {
            role.Name = roleUpdateRequest.Name;
        }
        if(roleUpdateRequest.Flags != null)
        {
            role.RolePermissions.Clear();

        foreach (var flag in roleUpdateRequest.Flags)
        {
            var permission = await _context.Permissions
                .SingleAsync(p => p.Name == flag);

            role.RolePermissions.Add(new RolePermission
            {
                Permission = permission
            });
        }
        }

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
        var employee = await _context.Employees.FindAsync(employeeId);
        
        if (role == null || employee == null)
        {
            return false;
        }

        var employeeRole = await _context.EmployeeRoles.FindAsync(employeeId, roleId);

         if (employeeRole == null)
        {
            employeeRole = new EmployeeRole
            {
                EmployeeId = employeeId,
                RoleId = roleId
            };
            _context.EmployeeRoles.Add(employeeRole);
            await _context.SaveChangesAsync();
        }

        await _context.SaveChangesAsync();
        return true;
    }
}