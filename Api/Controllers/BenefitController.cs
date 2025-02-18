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
    public class BenefitController : ControllerBase
    {
        private readonly IBenefitService _benefitService;

        public BenefitController(IBenefitService benefitService)
        {
            _benefitService = benefitService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] BenefitDto benefit, CancellationToken cancellationToken)
        {
            var response = await _benefitService.CreateBenefitAsync(benefit, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetBenefitsAsync([FromQuery] BenefitQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _benefitService.ReadBenefitsAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBenefitsByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _benefitService.ReadBenefitByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBenefitAsync(string id, [FromBody] BenefitDto updateBenefit, CancellationToken cancellationToken)
        {
            var response = await _benefitService.UpdateBenefitAsync(id, updateBenefit, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBenefitAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _benefitService.DeleteBenefitAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
