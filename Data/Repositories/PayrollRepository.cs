
namespace HRIS.Data.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly HrisContext _hrisContext;

        public PayrollRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(Payroll payroll, CancellationToken cancellationToken)
        {
            _hrisContext.Payrolls.Add(payroll);
        }

        public async Task<Payroll?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid payrollId = Guid.Parse(id);
            var payroll = await _hrisContext.Payrolls.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == payrollId, cancellationToken);
            return payroll;
        }

        public async Task<List<Payroll>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken)
        {
            var payrolls = await _hrisContext.Payrolls.IsActiveRows().AsNoTracking().Where(x => x.EmployeeId == Guid.Parse(employeeId)).ToListAsync(cancellationToken);
            return payrolls;
        }

        public async Task<PayrollsResponseDto> GetAllAsync(PayrollQueryDto payrollQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.Payrolls.IsActiveRows().Select(x => x);

            if (!string.IsNullOrWhiteSpace(payrollQueryDto.employeeId))
            {
                query = query.Where(x => x.EmployeeId.ToString() == payrollQueryDto.employeeId);
            }
            if (payrollQueryDto?.grossSalary != null)
            {
                query = query.Where(x => x.GrossSalary == payrollQueryDto.grossSalary);
            }
            if (payrollQueryDto?.deductions != null)
            {
                query = query.Where(x => x.Deductions == payrollQueryDto.deductions);
            }
            if (payrollQueryDto?.netSalary != null)
            {
                query = query.Where(x => x.NetSalary == payrollQueryDto.netSalary);
            }
            if (payrollQueryDto?.paymentDate != null && payrollQueryDto.paymentDate != DateTime.MinValue)
            {
                query = query.Where(x => x.PaymentDate.Date == payrollQueryDto.paymentDate.Date);
            }
            if (!string.IsNullOrWhiteSpace(payrollQueryDto?.sortBy) && payrollQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{payrollQueryDto.sortBy} {(payrollQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(payrollQueryDto.offset)
                .Take(payrollQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new PayrollsResponseDto(page, totalData);
        }

        public async Task UpdateAsync(Payroll payroll, CancellationToken cancellationToken)
        {
            _hrisContext.Payrolls.Update(payroll);
        }

        public async Task DeleteAsync(Payroll payroll, CancellationToken cancellationToken)
        {
            _hrisContext.Payrolls.Remove(payroll);
        }
    }
}
