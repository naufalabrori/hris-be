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
    public class RecruitmentController : ControllerBase
    {
        private readonly IRecruitmentService _recruitmentService;

        public RecruitmentController(IRecruitmentService recruitmentService)
        {
            _recruitmentService = recruitmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RecruitmentDto recruitment, CancellationToken cancellationToken)
        {
            var response = await _recruitmentService.CreateRecruitmentAsync(recruitment, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecruitmentsAsync([FromQuery] RecruitmentQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _recruitmentService.ReadRecruitmentsAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecruitmentsByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _recruitmentService.ReadRecruitmentByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecruitmentAsync(string id, [FromBody] RecruitmentDto updateRecruitment, CancellationToken cancellationToken)
        {
            var response = await _recruitmentService.UpdateRecruitmentAsync(id, updateRecruitment, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecruitmentAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _recruitmentService.DeleteRecruitmentAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
