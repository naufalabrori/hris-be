using HRIS.Core.Dto;
using System;

namespace HRIS.Core.Entity
{
    public class Payroll : BaseEntity
    {
        public Payroll() { }

        public Payroll(PayrollDto payroll)
        {
            EmployeeId = Guid.Parse(payroll.employeeId);
            PayPeriodStartDate = payroll.payPeriodStartDate;
            PayPeriodEndDate = payroll.payPeriodEndDate;
            GrossSalary = payroll.grossSalary;
            Deductions = payroll.deductions;
            NetSalary = payroll.netSalary;
            PaymentDate = payroll.paymentDate;
        }

        public Guid Id { get; set; } = default!;
        public Guid EmployeeId { get; set; } = default!;
        public DateTime PayPeriodStartDate { get; set; } = DateTime.Now;
        public DateTime PayPeriodEndDate { get; set; } = DateTime.MaxValue;
        public decimal GrossSalary { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public void UpdatePayroll(PayrollDto payroll)
        {
            EmployeeId = Guid.TryParse(payroll.employeeId, out var employeeId) ? employeeId : EmployeeId;
            PayPeriodStartDate = payroll?.payPeriodStartDate ?? PayPeriodStartDate;
            PayPeriodEndDate = payroll?.payPeriodEndDate ?? PayPeriodEndDate;
            GrossSalary = payroll?.grossSalary ?? GrossSalary;
            Deductions = payroll?.deductions ?? Deductions;
            NetSalary = payroll?.netSalary ?? NetSalary;
            PaymentDate = payroll?.paymentDate ?? PaymentDate;
        }
    }
}
