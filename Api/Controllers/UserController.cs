using HRIS.Core.Dto;
using HRIS.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Super Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] NewUserDto user, CancellationToken cancellationToken)
        {
            var response = await _userService.CreateUserAsync(user, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync([FromQuery] UserQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _userService.ReadUsersAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsersByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _userService.ReadUserByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id, [FromBody] UserDto updateUser, CancellationToken cancellationToken)
        {
            var response = await _userService.UpdateUserAsync(id, updateUser, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _userService.DeleteUserAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> UserLoginAsync([FromBody] UserLoginDto login, CancellationToken cancellationToken)
        {
            var response = await _userService.LoginUserAsync(login, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
