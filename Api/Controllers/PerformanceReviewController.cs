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
    public class PerformanceReviewController : ControllerBase
    {
        private readonly IPerformanceReviewService _performanceReviewService;

        public PerformanceReviewController(IPerformanceReviewService performanceReviewService)
        {
            _performanceReviewService = performanceReviewService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] PerformanceReviewDto performanceReview, CancellationToken cancellationToken)
        {
            var response = await _performanceReviewService.CreatePerformanceReviewAsync(performanceReview, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetPerformanceReviewsAsync([FromQuery] PerformanceReviewQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _performanceReviewService.ReadPerformanceReviewsAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerformanceReviewsByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _performanceReviewService.ReadPerformanceReviewByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerformanceReviewAsync(string id, [FromBody] PerformanceReviewDto updatePerformanceReview, CancellationToken cancellationToken)
        {
            var response = await _performanceReviewService.UpdatePerformanceReviewAsync(id, updatePerformanceReview, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformanceReviewAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _performanceReviewService.DeletePerformanceReviewAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
