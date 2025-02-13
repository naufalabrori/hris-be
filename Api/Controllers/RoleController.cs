using HRIS.Core.Dto;
using HRIS.Core.Interfaces.Services;
using HRIS.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAsync([FromBody] RoleDto role, CancellationToken cancellationToken)
        {
            var response = await _roleService.CreateRoleAsync(role, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetRolesAsync([FromQuery] RoleQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _roleService.ReadRolesAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRolesByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _roleService.ReadRoleByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoleAsync(string id, [FromBody] RoleDto updateRole, CancellationToken cancellationToken)
        {
            var response = await _roleService.UpdateRoleAsync(id, updateRole, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _roleService.DeleteRoleAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
