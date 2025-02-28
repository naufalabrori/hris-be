
namespace HRIS.Core.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IHrisRepository _hrisRepository;

        public DepartmentService(IDepartmentRepository departmentRepository, IHrisRepository hrisRepository)
        {
            _departmentRepository = departmentRepository;
            _hrisRepository = hrisRepository;
        }

        public async Task<ApiResponseDto<Department?>> CreateDepartmentAsync(DepartmentDto department, CancellationToken cancellationToken)
        {
            var existingDepartment = await _departmentRepository.GetByDepartmentNameAsync(department.departmentName, cancellationToken);
            if (existingDepartment != null)
            {
                return new ApiResponseDto<Department?>
                {
                    Success = false,
                    Message = "Department already exist"
                };
            }

            var newDepartment = new Department(department);

            await _departmentRepository.AddAsync(newDepartment, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Department?>
            {
                Success = true,
                Message = "Create department successfully",
                Data = newDepartment
            };
        }

        public async Task<ApiResponseDto<DepartmentsResponseDto>> ReadDepartmentsAsync(DepartmentQueryDto departmentQueryDto, CancellationToken cancellationToken)
        {
            var data = await _departmentRepository.GetAllAsync(departmentQueryDto, cancellationToken);

            return new ApiResponseDto<DepartmentsResponseDto>
            {
                Success = true,
                Message = "Get all department successfully",
                Data = data
            };
        }

        public async Task<ApiResponseDto<Department?>> ReadDepartmentByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<Department?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);
            if (department == null)
            {
                return new ApiResponseDto<Department?>
                {
                    Success = false,
                    Message = "Department not found",
                };
            }

            return new ApiResponseDto<Department?>
            {
                Success = true,
                Message = "Get department successfully",
                Data = department
            };
        }

        public async Task<ApiResponseDto<Department?>> UpdateDepartmentAsync(string id, DepartmentUpdateDto updateDepartment, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<Department?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);
            if (department == null)
            {
                return new ApiResponseDto<Department?>
                {
                    Success = false,
                    Message = "Department not found",
                };
            }

            department.UpdateDepartment(updateDepartment);

            await _departmentRepository.UpdateAsync(department, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Department?>
            {
                Success = true,
                Message = "Update department successfully",
                Data = department
            };
        }

        public async Task<ApiResponseDto<bool>> DeleteDepartmentAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);
            if (department == null)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Department not found",
                };
            }

            await _departmentRepository.DeleteAsync(department, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<bool>
            {
                Success = true,
                Message = "Delete department successfully",
                Data = true
            };
        }
    }
}
