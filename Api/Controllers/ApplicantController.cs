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
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;

        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ApplicantDto applicant, CancellationToken cancellationToken)
        {
            var response = await _applicantService.CreateApplicantAsync(applicant, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetApplicantsAsync([FromQuery] ApplicantQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _applicantService.ReadApplicantsAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicantsByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _applicantService.ReadApplicantByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplicantAsync(string id, [FromBody] ApplicantDto updateApplicant, CancellationToken cancellationToken)
        {
            var response = await _applicantService.UpdateApplicantAsync(id, updateApplicant, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicantAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _applicantService.DeleteApplicantAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
