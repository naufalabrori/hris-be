
namespace HRIS.Core.Interfaces.Services
{
    public interface IDepartmentService
    {
        public Task<ApiResponseDto<Department?>> CreateDepartmentAsync(DepartmentDto department, CancellationToken cancellationToken);
        public Task<ApiResponseDto<DepartmentsResponseDto>> ReadDepartmentsAsync(DepartmentQueryDto departmentQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Department?>> ReadDepartmentByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Department?>> UpdateDepartmentAsync(string id, DepartmentDto updateDepartment, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeleteDepartmentAsync(string id, CancellationToken cancellationToken);
    }
}
