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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] DepartmentDto department, CancellationToken cancellationToken)
        {
            var response = await _departmentService.CreateDepartmentAsync(department, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartmentsAsync([FromQuery] DepartmentQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _departmentService.ReadDepartmentsAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentsByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _departmentService.ReadDepartmentByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartmentAsync(string id, [FromBody] DepartmentDto updateDepartment, CancellationToken cancellationToken)
        {
            var response = await _departmentService.UpdateDepartmentAsync(id, updateDepartment, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _departmentService.DeleteDepartmentAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
