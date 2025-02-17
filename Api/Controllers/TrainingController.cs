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
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TrainingDto training, CancellationToken cancellationToken)
        {
            var response = await _trainingService.CreateTrainingAsync(training, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrainingsAsync([FromQuery] TrainingQueryDto query, CancellationToken cancellationToken)
        {
            var response = await _trainingService.ReadTrainingsAsync(query, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingsByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _trainingService.ReadTrainingByIdAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrainingAsync(string id, [FromBody] TrainingDto updateTraining, CancellationToken cancellationToken)
        {
            var response = await _trainingService.UpdateTrainingAsync(id, updateTraining, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _trainingService.DeleteTrainingAsync(id, cancellationToken);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
