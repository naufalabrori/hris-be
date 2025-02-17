
namespace HRIS.Core.Interfaces.Repositories
{
    public interface IEmployeeTrainingRepository
    {
        public Task AddAsync(EmployeeTraining employeeTraining, CancellationToken cancellationToken);
        public Task<EmployeeTraining?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<EmployeeTrainingsResponseDto> GetAllAsync(EmployeeTrainingQueryDto employeeTrainingQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(EmployeeTraining employeeTraining, CancellationToken cancellationToken);
        public Task DeleteAsync(EmployeeTraining employeeTraining, CancellationToken cancellationToken);
    }
}
