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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] EmployeeDto employee, CancellationToken cancellationToken)
        {
            var response = await _employeeService.CreateEmployeeAsync(employee, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesAsync([FromQuery] EmployeeQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _employeeService.ReadEmployeesAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeesByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _employeeService.ReadEmployeeByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync(string id, [FromBody] EmployeeDto updateEmployee, CancellationToken cancellationToken)
        {
            var response = await _employeeService.UpdateEmployeeAsync(id, updateEmployee, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _employeeService.DeleteEmployeeAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
