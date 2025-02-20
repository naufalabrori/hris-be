
namespace HRIS.Core.Services
{
    public class EmployeeTrainingService : IEmployeeTrainingService
    {
        private readonly IEmployeeTrainingRepository _employeeTrainingRepository;
        private readonly IHrisRepository _hrisRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITrainingRepository _trainingRepository;

        public EmployeeTrainingService(IEmployeeTrainingRepository employeeTrainingRepository, IHrisRepository hrisRepository, IEmployeeRepository employeeRepository, ITrainingRepository trainingRepository)
        {
            _employeeTrainingRepository = employeeTrainingRepository;
            _hrisRepository = hrisRepository;
            _employeeRepository = employeeRepository;
            _trainingRepository = trainingRepository;
        }

        public async Task<ApiResponseDto<EmployeeTraining?>> CreateEmployeeTrainingAsync(EmployeeTrainingDto employeeTraining, CancellationToken cancellationToken)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(employeeTraining.employeeId, cancellationToken);
            if (existingEmployee == null)
            {
                return new ApiResponseDto<EmployeeTraining?>
                {
                    Success = false,
                    Message = "Employee not found"
                };
            }

            var existingTraining = await _trainingRepository.GetByIdAsync(employeeTraining.trainingId, cancellationToken);
            if (existingTraining == null)
            {
                return new ApiResponseDto<EmployeeTraining?>
                {
                    Success = false,
                    Message = "Training not found"
                };
            }

            var newEmpTraining = new EmployeeTraining(employeeTraining);

            await _employeeTrainingRepository.AddAsync(newEmpTraining, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<EmployeeTraining?>
            {
                Success = true,
                Message = "Create employee training successfully",
                Data = newEmpTraining
            };
        }

        public async Task<ApiResponseDto<EmployeeTrainingsResponseDto>> ReadEmployeeTrainingsAsync(EmployeeTrainingQueryDto employeeTrainingQueryDto, CancellationToken cancellationToken)
        {
            var data = await _employeeTrainingRepository.GetAllAsync(employeeTrainingQueryDto, cancellationToken);

            return new ApiResponseDto<EmployeeTrainingsResponseDto>
            {
                Success = true,
                Message = "Get all employee training successfully"
            };
        }

        public async Task<ApiResponseDto<EmployeeTraining?>> ReadEmployeeTrainingByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<EmployeeTraining?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var empTraining = await _employeeTrainingRepository.GetByIdAsync(id, cancellationToken);
            if (empTraining == null)
            {
                return new ApiResponseDto<EmployeeTraining?>
                {
                    Success = false,
                    Message = "Employee training not found"
                };
            }

            return new ApiResponseDto<EmployeeTraining?>
            {
                Success = true,
                Message = "Get employee training successfully"
            };
        }

        public async Task<ApiResponseDto<EmployeeTraining?>> UpdateEmployeeTrainingAsync(string id, EmployeeTrainingDto updateEmployeeTraining, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<EmployeeTraining?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var empTraining = await _employeeTrainingRepository.GetByIdAsync(id, cancellationToken);
            if (empTraining == null)
            {
                return new ApiResponseDto<EmployeeTraining?>
                {
                    Success = false,
                    Message = "Employee training not found"
                };
            }

            empTraining.UpdateEmployeeTraining(updateEmployeeTraining);

            await _employeeTrainingRepository.UpdateAsync(empTraining, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<EmployeeTraining?>
            {
                Success = true,
                Message = "Update employee training successfully",
                Data = empTraining
            };
        }

        public async Task<ApiResponseDto<bool>> DeleteEmployeeTrainingAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var empTraining = await _employeeTrainingRepository.GetByIdAsync(id, cancellationToken);
            if (empTraining == null)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Employee training not found"
                };
            }

            await _employeeTrainingRepository.DeleteAsync(empTraining, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<bool>
            {
                Success = true,
                Message = "Delete employee training successfully",
                Data = true
            };
        }
    }
}
