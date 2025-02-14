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
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserRoleDto userRole, CancellationToken cancellationToken)
        {
            var response = await _userRoleService.CreateUserRoleAsync(userRole, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRolesAsync([FromQuery] UserRoleQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _userRoleService.ReadUserRolesAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserRolesByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _userRoleService.ReadUserRoleByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserRoleAsync(string id, [FromBody] UserRoleDto updateUserRole, CancellationToken cancellationToken)
        {
            var response = await _userRoleService.UpdateUserRoleAsync(id, updateUserRole, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRoleAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _userRoleService.DeleteUserRoleAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
