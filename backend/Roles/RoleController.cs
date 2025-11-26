namespace backend.Roles;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/v1/[controller]")]

public class RoleController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;
    private readonly ILogger<RoleController> _logger;

    public RoleController(IRoleRepository roleRepository, ILogger<RoleController> logger)
    {
        _roleRepository = roleRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleRepository.GetRolesAsync();
        return Ok(roles);
    }

    [HttpGet("{roleId}")]
    public async Task<IActionResult> GetRoleById(int roleId)
    {
        var role = await _roleRepository.GetRoleByIdAsync(roleId);

        if (role == null)
        {
            return NotFound();
        }

        _logger.LogInformation($"Fetched role with ID {roleId}");
        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] RoleDTO roleDTO)
    {

        if (roleDTO == null)
        {
                _logger.LogWarning("RoleDTO is null.");
                return BadRequest();
        }

        var newRole = await _roleRepository.CreateRoleAsync(roleDTO);
        return CreatedAtAction(nameof(GetRoles), new { roleId = newRole.RoleId }, newRole);
    }

    [HttpPatch("{roleId}")]
    public async Task<IActionResult> UpdateRole(int roleId, [FromBody] RoleDTO roleDTO)
    {
        if (roleDTO == null)
        {
                _logger.LogWarning("RoleDTO is null.");
                return BadRequest();
        }

        var updated = await _roleRepository.UpdateRoleByIdAsync(roleId, roleDTO);

        if (!updated)
        {
            _logger.LogWarning($"Role with ID {roleId} not found.");
            return NotFound();
        }

        _logger.LogInformation($"Updated role with ID {roleId}");
        return NoContent();
    }

    [HttpDelete("{roleId}")]
    public async Task<IActionResult> DeleteRole(int roleId)
    {
        var deleted = await _roleRepository.DeleteRoleByIdAsync(roleId);

        if (!deleted)
        {
            _logger.LogWarning($"Role with ID {roleId} not found.");
            return NotFound();
        }

        _logger.LogInformation($"Deleted role with ID {roleId}");
        return NoContent();
    }

    [HttpPost("{roleId}/assign")]
    public async Task<IActionResult> AssignRole([FromRoute] int roleId, [FromBody] RoleAssignmentRequest request)
    {
        await _roleRepository.AssignRoleToEmployeeAsync(roleId, request.EmployeeId);
        return NoContent();
    }

    [HttpDelete("{roleId}/assign/{employeeId}")]
    public async Task<IActionResult> RemoveRole([FromRoute] int roleId, [FromRoute] int employeeId)
    {
        var removed = await _roleRepository.RemoveRoleFromEmployeeAsync(roleId, employeeId);
        if (!removed) return NotFound();
        return NoContent();
    }


}
