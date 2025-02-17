using HRIS.Core.Dto;
using HRIS.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRIS.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeTrainingController : ControllerBase
    {
        private readonly IEmployeeTrainingService _employeeTrainingService;

        public EmployeeTrainingController(IEmployeeTrainingService employeeTrainingService)
        {
            _employeeTrainingService = employeeTrainingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] EmployeeTrainingDto employeeTraining, CancellationToken cancellationToken)
        {
            var response = await _employeeTrainingService.CreateEmployeeTrainingAsync(employeeTraining, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeTrainingsAsync([FromQuery] EmployeeTrainingQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _employeeTrainingService.ReadEmployeeTrainingsAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeTrainingsByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _employeeTrainingService.ReadEmployeeTrainingByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeTrainingAsync(string id, [FromBody] EmployeeTrainingDto updateEmployeeTraining, CancellationToken cancellationToken)
        {
            var response = await _employeeTrainingService.UpdateEmployeeTrainingAsync(id, updateEmployeeTraining, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeTrainingAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _employeeTrainingService.DeleteEmployeeTrainingAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
