
namespace HRIS.Core.Interfaces.Repositories
{
    public interface IPayrollRepository
    {
        public Task AddAsync(Payroll payroll, CancellationToken cancellationToken);
        public Task<Payroll?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<List<Payroll>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken);
        public Task<PayrollsResponseDto> GetAllAsync(PayrollQueryDto payrollQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(Payroll payroll, CancellationToken cancellationToken);
        public Task DeleteAsync(Payroll payroll, CancellationToken cancellationToken);
    }
}
