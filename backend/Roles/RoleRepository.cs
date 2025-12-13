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
        return await _context.Roles.FindAsync(roleId);
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
        var employee = await _context.EmployeeRoles.FindAsync(employeeId);
        if (role == null || employee == null)
        {
            return false;
        }

         _context.EmployeeRoles.Add(new EmployeeRole
         {
            RoleId = roleId,
            EmployeeId = employeeId
         });

        await _context.SaveChangesAsync();
        return true;
    }
}