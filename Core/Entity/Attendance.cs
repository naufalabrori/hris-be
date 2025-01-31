using HRIS.Core.Dto;
using System;

namespace HRIS.Core.Entity
{
    public class Attendance : BaseEntity
    {
        public Attendance() { }

        public Attendance(AttendanceDto attendance)
        {
            EmployeeId = Guid.Parse(attendance.employeeId);
            Date = attendance.date;
            TimeIn = attendance.timeIn;
            TimeOut = attendance.timeOut;
            HoursWorked = attendance.hoursWorked;
        }

        public Guid Id { get; set; } = default!;
        public Guid EmployeeId { get; set; } = default!;
        public DateTime Date { get; set; } = DateTime.Now;
        public TimeOnly TimeIn { get; set; }
        public TimeOnly TimeOut { get; set; }
        public decimal HoursWorked { get; set; }

        public void UpdateAttendance(AttendanceDto attendance)
        {
            EmployeeId = Guid.TryParse(attendance.employeeId, out var employeeId) ? employeeId : EmployeeId;
            Date = attendance?.date ?? Date;
            TimeIn = attendance?.timeIn ?? TimeIn;
            TimeOut = attendance?.timeOut ?? TimeOut;
            HoursWorked = attendance?.hoursWorked ?? HoursWorked;
        }
    }
}
