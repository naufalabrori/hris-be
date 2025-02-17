
namespace HRIS.Core.Interfaces.Services
{
    public interface IEmployeeTrainingService
    {
        public Task<ApiResponseDto<EmployeeTraining?>> CreateEmployeeTrainingAsync(EmployeeTrainingDto employeeTraining, CancellationToken cancellationToken);
        public Task<ApiResponseDto<EmployeeTrainingsResponseDto>> ReadEmployeeTrainingsAsync(EmployeeTrainingQueryDto employeeTrainingQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<EmployeeTraining?>> ReadEmployeeTrainingByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<EmployeeTraining?>> UpdateEmployeeTrainingAsync(string id, EmployeeTrainingDto updateEmployeeTraining, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeleteEmployeeTrainingAsync(string id, CancellationToken cancellationToken);
    }
}
