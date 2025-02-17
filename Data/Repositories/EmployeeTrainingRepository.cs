
namespace HRIS.Data.Repositories
{
    public class EmployeeTrainingRepository : IEmployeeTrainingRepository
    {
        private readonly HrisContext _hrisContext;

        public EmployeeTrainingRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(EmployeeTraining employeeTraining, CancellationToken cancellationToken)
        {
            _hrisContext.EmployeeTrainings.Add(employeeTraining);
        }

        public async Task<EmployeeTraining?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid empTrainingId = Guid.Parse(id);
            var empTraining = await _hrisContext.EmployeeTrainings.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == empTrainingId, cancellationToken);
            return empTraining;
        }

        public async Task<EmployeeTrainingsResponseDto> GetAllAsync(EmployeeTrainingQueryDto employeeTrainingQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.EmployeeTrainings.IsActiveRows().AsNoTracking().Select(x => x);

            if (!string.IsNullOrWhiteSpace(employeeTrainingQueryDto.employeeId))
            {
                query = query.Where(x => x.EmployeeId.ToString() == employeeTrainingQueryDto.employeeId);
            }
            if (!string.IsNullOrWhiteSpace(employeeTrainingQueryDto.trainingId))
            {
                query = query.Where(x => x.TrainingId.ToString() == employeeTrainingQueryDto.trainingId);
            }
            if (!string.IsNullOrWhiteSpace(employeeTrainingQueryDto?.sortBy) && employeeTrainingQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{employeeTrainingQueryDto.sortBy} {(employeeTrainingQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(employeeTrainingQueryDto.offset)
                .Take(employeeTrainingQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new EmployeeTrainingsResponseDto(page, totalData);
        }

        public async Task UpdateAsync(EmployeeTraining employeeTraining, CancellationToken cancellationToken)
        {
            _hrisContext.EmployeeTrainings.Update(employeeTraining);
        }

        public async Task DeleteAsync(EmployeeTraining employeeTraining, CancellationToken cancellationToken)
        {
            _hrisContext.EmployeeTrainings.Remove(employeeTraining);
        }
    }
}
