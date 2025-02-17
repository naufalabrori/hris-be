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
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;

        public PayrollController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PayrollDto payroll, CancellationToken cancellationToken)
        {
            var response = await _payrollService.CreatePayrollAsync(payroll, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrollsAsync([FromQuery] PayrollQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _payrollService.ReadPayrollsAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayrollsByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _payrollService.ReadPayrollByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayrollAsync(string id, [FromBody] PayrollDto updatePayroll, CancellationToken cancellationToken)
        {
            var response = await _payrollService.UpdatePayrollAsync(id, updatePayroll, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayrollAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _payrollService.DeletePayrollAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
