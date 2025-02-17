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
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AttendanceDto attendance, CancellationToken cancellationToken)
        {
            var response = await _attendanceService.CreateAttendanceAsync(attendance, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAttendancesAsync([FromQuery] AttendanceQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _attendanceService.ReadAttendancesAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttendancesByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _attendanceService.ReadAttendanceByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttendanceAsync(string id, [FromBody] AttendanceDto updateAttendance, CancellationToken cancellationToken)
        {
            var response = await _attendanceService.UpdateAttendanceAsync(id, updateAttendance, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendanceAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _attendanceService.DeleteAttendanceAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
