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
    public class JobTitleController : ControllerBase
    {
        private readonly IJobTitleService _jobTitleService;

        public JobTitleController(IJobTitleService jobTitleService)
        {
            _jobTitleService = jobTitleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] JobTitleDto jobTitle, CancellationToken cancellationToken)
        {
            var response = await _jobTitleService.CreateJobTitleAsync(jobTitle, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetJobTitlesAsync([FromQuery] JobTitleQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _jobTitleService.ReadJobTitlesAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobTitlesByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _jobTitleService.ReadJobTitleByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobTitleAsync(string id, [FromBody] JobTitleUpdateDto updateJobTitle, CancellationToken cancellationToken)
        {
            var response = await _jobTitleService.UpdateJobTitleAsync(id, updateJobTitle, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobTitleAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _jobTitleService.DeleteJobTitleAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
