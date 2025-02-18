
namespace HRIS.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHrisRepository _hrisRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IHrisRepository hrisRepository)
        {
            _employeeRepository = employeeRepository;
            _hrisRepository = hrisRepository;
        }

        public async Task<ApiResponseDto<Employee?>> CreateEmployeeAsync(EmployeeDto employee, CancellationToken cancellationToken)
        {
            try
            {
                var newEmployee = new Employee(employee);

                await _employeeRepository.AddAsync(newEmployee, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<Employee?>
                {
                    Success = true,
                    Message = "Create employee successfully",
                    Data = newEmployee
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<Employee?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<EmployeesResponseDto>> ReadEmployeesAsync(EmployeeQueryDto employeeQueryDto, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _employeeRepository.GetAllAsync(employeeQueryDto, cancellationToken);

                return new ApiResponseDto<EmployeesResponseDto>
                {
                    Success = true,
                    Message = "Get all employee successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<EmployeesResponseDto>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<Employee?>> ReadEmployeeByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<Employee?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);
                if (employee == null)
                {
                    return new ApiResponseDto<Employee?>
                    {
                        Success = false,
                        Message = "Employee not found",
                    };
                }

                return new ApiResponseDto<Employee?>
                {
                    Success = true,
                    Message = "Get employee successfully",
                    Data = employee
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<Employee?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<Employee?>> UpdateEmployeeAsync(string id, EmployeeDto updateEmployee, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<Employee?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);
                if (employee == null)
                {
                    return new ApiResponseDto<Employee?>
                    {
                        Success = false,
                        Message = "Employee not found",
                    };
                }

                employee.UpdateEmployee(updateEmployee);

                await _employeeRepository.UpdateAsync(employee, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<Employee?>
                {
                    Success = true,
                    Message = "Update employee successfully",
                    Data = employee
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<Employee?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<bool>> DeleteEmployeeAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<bool>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);
                if (employee == null)
                {
                    return new ApiResponseDto<bool>
                    {
                        Success = false,
                        Message = "Employee not found",
                    };
                }

                await _employeeRepository.DeleteAsync(employee, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Message = "Delete employee successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
