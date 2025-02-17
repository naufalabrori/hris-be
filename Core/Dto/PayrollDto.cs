
namespace HRIS.Core.Dto
{
    public record PayrollDto(string employeeId, DateTime payPeriodStartDate, DateTime payPeriodEndDate, decimal grossSalary, decimal deductions, decimal netSalary, DateTime paymentDate);

    public record PayrollQueryDto(string employeeId, decimal grossSalary, decimal deductions, decimal netSalary, DateTime paymentDate, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record PayrollsResponseDto(List<Payroll> Data, int TotalData);

}