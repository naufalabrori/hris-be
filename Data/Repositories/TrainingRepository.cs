
namespace HRIS.Data.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly HrisContext _hrisContext;

        public TrainingRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(Training training, CancellationToken cancellationToken)
        {
            _hrisContext.Trainings.Add(training);
        }

        public async Task<Training?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var trainingId = Guid.Parse(id);
            var training = await _hrisContext.Trainings.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == trainingId, cancellationToken);
            return training;
        }

        public async Task<TrainingsResponseDto> GetAllAsync(TrainingQueryDto trainingQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.Trainings.IsActiveRows().AsNoTracking().Select(x => x);

            if (!string.IsNullOrWhiteSpace(trainingQueryDto.trainingName))
            {
                query = query.Where(x => x.TrainingName.Contains(trainingQueryDto.trainingName));
            }
            if (!string.IsNullOrWhiteSpace(trainingQueryDto.description))
            {
                query = query.Where(x => x.Description.Contains(trainingQueryDto.description));
            }
            if (trainingQueryDto?.startDate != null && trainingQueryDto.startDate != DateTime.MinValue)
            {
                query = query.Where(x => x.StartDate.Date >= trainingQueryDto.startDate.Date);
            }
            if (trainingQueryDto?.endDate != null && trainingQueryDto.endDate != DateTime.MinValue)
            {
                query = query.Where(x => x.EndDate.Date <= trainingQueryDto.endDate.Date);
            }
            if (!string.IsNullOrWhiteSpace(trainingQueryDto.trainer))
            {
                query = query.Where(x => x.Trainer.Contains(trainingQueryDto.trainer));
            }
            if (!string.IsNullOrWhiteSpace(trainingQueryDto?.sortBy) && trainingQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{trainingQueryDto.sortBy} {(trainingQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(trainingQueryDto.offset)
                .Take(trainingQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new TrainingsResponseDto(page, totalData);
        }

        public async Task UpdateAsync(Training training, CancellationToken cancellationToken)
        {
            _hrisContext.Trainings.Update(training);
        }

        public async Task DeleteAsync(Training training, CancellationToken cancellationToken)
        {
            _hrisContext.Trainings.Remove(training);
        }
    }
}
