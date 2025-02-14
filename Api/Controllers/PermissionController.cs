using HRIS.Core.Dto;
using HRIS.Core.Interfaces.Services;
using HRIS.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PermissionDto permission, CancellationToken cancellationToken)
        {
            var response = await _permissionService.CreatePermissionAsync(permission, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetPermissionsAsync([FromQuery] PermissionQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _permissionService.ReadPermissionsAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermissionsByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _permissionService.ReadPermissionByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePermissionAsync(string id, [FromBody] PermissionDto updatePermission, CancellationToken cancellationToken)
        {
            var response = await _permissionService.UpdatePermissionAsync(id, updatePermission, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermissionAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _permissionService.DeletePermissionAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
