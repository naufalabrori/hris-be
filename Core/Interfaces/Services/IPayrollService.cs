
namespace HRIS.Core.Interfaces.Services
{
    public interface IPayrollService
    {
        public Task<ApiResponseDto<Payroll?>> CreatePayrollAsync(PayrollDto payroll, CancellationToken cancellationToken);
        public Task<ApiResponseDto<PayrollsResponseDto>> ReadPayrollsAsync(PayrollQueryDto payrollQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Payroll?>> ReadPayrollByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Payroll?>> UpdatePayrollAsync(string id, PayrollDto updatePayroll, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeletePayrollAsync(string id, CancellationToken cancellationToken);
    }
}
