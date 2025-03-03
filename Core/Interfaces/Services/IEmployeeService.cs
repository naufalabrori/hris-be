
namespace HRIS.Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        public Task<ApiResponseDto<Employee?>> CreateEmployeeAsync(EmployeeDto employee, CancellationToken cancellationToken);
        public Task<ApiResponseDto<EmployeesResponseDto>> ReadEmployeesAsync(EmployeeQueryDto employeeQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<EmployeeExtDto?>> ReadEmployeeByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Employee?>> UpdateEmployeeAsync(string id, EmployeeDto updateEmployee, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeleteEmployeeAsync(string id, CancellationToken cancellationToken);
    }
}
