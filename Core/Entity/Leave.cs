using HRIS.Core.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Core.Entity
{
    public class Leave : BaseEntity
    {
        public Leave() { }

        public Leave(LeaveDto leave)
        {
            EmployeeId = Guid.Parse(leave.employeeId);
            LeaveType = leave.leaveType;
            StartDate = leave.startDate;
            EndDate = leave.endDate;
            Status = leave.status;
        }

        public Guid Id { get; set; } = default!;
        public Guid EmployeeId { get; set; } = default!;
        [StringLength(50)]
        public string LeaveType { get; set; } = default!;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.MaxValue;
        [StringLength(50)]
        public string Status { get; set; } = default!;

        public void UpdateLeave(LeaveDto leave)
        {
            EmployeeId = Guid.TryParse(leave.employeeId, out var employeeId) ? employeeId : EmployeeId;
            LeaveType = leave.leaveType ?? LeaveType;
            StartDate = leave?.startDate ?? StartDate;
            EndDate = leave?.endDate ?? EndDate;
            Status = leave?.status ?? Status;
        }

    }
}
