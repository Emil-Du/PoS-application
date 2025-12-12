namespace backend.Roles;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class RoleController : ControllerBase
{
    private readonly IRoleRepository _repository;

    public RoleController(IRoleRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _repository.GetRolesAsync();
        return Ok(roles);
    }

    [HttpGet("{roleId}")]
    public async Task<IActionResult> GetRoleById(int roleId)
    {
        var role = await _repository.GetRoleByIdAsync(roleId);

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

        var newRole = await _repository.CreateRoleAsync(roleCreateRequest);
        return CreatedAtAction(nameof(GetRoles), new { roleId = newRole.RoleId }, newRole);
    }

    [HttpPatch("{roleId}")]
    public async Task<IActionResult> UpdateRole(int roleId, [FromBody] RoleUpdateRequest roleUpdateRequest)
    {
        if (roleUpdateRequest == null)
        {
            return BadRequest();
        }

        var updated = await _repository.UpdateRoleByIdAsync(roleId, roleUpdateRequest);

        if (updated == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{roleId}")]
    public async Task<IActionResult> DeleteRole(int roleId)
    {
        var deleted = await _repository.DeleteRoleByIdAsync(roleId);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("{roleId}/assign")]
    public async Task<IActionResult> AssignRole([FromRoute] int roleId, [FromBody] RoleAssignmentRequest request)
    {
        await _repository.AssignRoleToEmployeeAsync(roleId, request.EmployeeId);
        return NoContent();
    }

    [HttpDelete("{roleId}/assign/{employeeId}")]
    public async Task<IActionResult> RemoveRole([FromRoute] int roleId, [FromRoute] int employeeId)
    {
        var removed = await _repository.RemoveRoleFromEmployeeAsync(roleId, employeeId);
        if (!removed) return NotFound();
        return NoContent();
    }


}
