using HRIS.Core.Dto;
using HRIS.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RolePermissionDto rolePermission, CancellationToken cancellationToken)
        {
            var response = await _rolePermissionService.CreateRolePermissionAsync(rolePermission, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetRolePermissionsAsync([FromQuery] RolePermissionQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _rolePermissionService.ReadRolePermissionsAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRolePermissionsByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _rolePermissionService.ReadRolePermissionByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRolePermissionAsync(string id, [FromBody] RolePermissionDto updateRolePermission, CancellationToken cancellationToken)
        {
            var response = await _rolePermissionService.UpdateRolePermissionAsync(id, updateRolePermission, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolePermissionAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _rolePermissionService.DeleteRolePermissionAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
