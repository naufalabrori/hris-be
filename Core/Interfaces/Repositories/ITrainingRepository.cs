
namespace HRIS.Core.Interfaces.Repositories
{
    public interface ITrainingRepository
    {
        public Task AddAsync(Training training, CancellationToken cancellationToken);
        public Task<Training?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<TrainingsResponseDto> GetAllAsync(TrainingQueryDto trainingQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(Training training, CancellationToken cancellationToken);
        public Task DeleteAsync(Training training, CancellationToken cancellationToken);
    }
}
