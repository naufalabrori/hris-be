using System;

namespace HRIS.Core.Dto
{
    public record PayrollDto(string employeeId, DateTime payPeriodStartDate, DateTime payPeriodEndDate, decimal grossSalary, decimal deductions, decimal netSalary, DateTime paymentDate);

}