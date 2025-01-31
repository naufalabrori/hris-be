using System;

namespace HRIS.Core.Dto
{
    public record AttendanceDto(string employeeId, DateTime date, TimeOnly timeIn, TimeOnly timeOut, decimal hoursWorked);
}
