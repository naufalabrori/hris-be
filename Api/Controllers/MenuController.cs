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
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MenuDto menu, CancellationToken cancellationToken)
        {
            var response = await _menuService.CreateMenuAsync(menu, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetMenusAsync([FromQuery] MenuQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _menuService.ReadMenusAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenusByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _menuService.ReadMenuByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuAsync(string id, [FromBody] MenuDto updateMenu, CancellationToken cancellationToken)
        {
            var response = await _menuService.UpdateMenuAsync(id, updateMenu, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _menuService.DeleteMenuAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
