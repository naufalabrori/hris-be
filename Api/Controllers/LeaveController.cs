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
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveService _leaveService;

        public LeaveController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] LeaveDto leave, CancellationToken cancellationToken)
        {
            var response = await _leaveService.CreateLeaveAsync(leave, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetLeavesAsync([FromQuery] LeaveQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _leaveService.ReadLeavesAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeavesByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _leaveService.ReadLeaveByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeaveAsync(string id, [FromBody] LeaveDto updateLeave, CancellationToken cancellationToken)
        {
            var response = await _leaveService.UpdateLeaveAsync(id, updateLeave, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _leaveService.DeleteLeaveAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("Approve/{id}")]
        public async Task<IActionResult> ApproveLeaveAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _leaveService.ApproveLeaveAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
