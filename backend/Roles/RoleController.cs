namespace backend.Roles;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = Roles.SuperAdmin + "," + Roles.Manager + "," + Roles.Employee)]
public class RoleController : ControllerBase
{
    private readonly IRoleService _service;

    public RoleController(IRoleService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _service.GetRolesAsync();
        return Ok(roles);
    }

    [HttpGet("{roleId}")]
    [Authorize(Roles = "manager,employee")]
    public async Task<IActionResult> GetRoleById(int roleId)
    {
        var role = await _service.GetRoleByIdAsync(roleId);

        if (role == null)
        {
            return NotFound();
        }

        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] RoleCreateRequest roleCreateRequest)
    {

        if (roleCreateRequest == null)
        {
            return BadRequest();
        }

        var newRole = await _service.CreateRoleAsync(roleCreateRequest);
        return CreatedAtAction(nameof(GetRoles), new { roleId = newRole.RoleId }, newRole);
    }

    [HttpPatch("{roleId}")]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> UpdateRole(int roleId, [FromBody] RoleUpdateRequest roleUpdateRequest)
    {
        if (roleUpdateRequest == null)
        {
            return BadRequest();
        }

        var updated = await _service.UpdateRoleByIdAsync(roleId, roleUpdateRequest);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{roleId}")]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> DeleteRole(int roleId)
    {
        var deleted = await _service.DeleteRoleByIdAsync(roleId);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("{roleId}/assign")]
    public async Task<IActionResult> AssignRole([FromRoute] int roleId, [FromBody] RoleAssignmentRequest request)
    {
        var updated = await _service.AssignRoleToEmployeeAsync(roleId, request.EmployeeId);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpGet("RoleIdByEmployeeId")]
    [Authorize(Roles = "manager,employee")]
    public async Task<IActionResult> GetRoleIdByEmployeeId([FromQuery] int employeeId)
    {
        EmployeeRolesDTO? role = await _service.GetRoleIdByEmployeeIdAsync(employeeId);


        if (role == null)
            return NotFound($"No role found for employeeId: {employeeId}");

        return Ok(role.RoleId);
    }

}
