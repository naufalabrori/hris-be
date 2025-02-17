
namespace HRIS.Core.Interfaces.Services
{
    public interface ITrainingService
    {
        public Task<ApiResponseDto<Training?>> CreateTrainingAsync(TrainingDto training, CancellationToken cancellationToken);
        public Task<ApiResponseDto<TrainingsResponseDto>> ReadTrainingsAsync(TrainingQueryDto trainingQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Training?>> ReadTrainingByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Training?>> UpdateTrainingAsync(string id, TrainingDto updateTraining, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeleteTrainingAsync(string id, CancellationToken cancellationToken);
    }
}
