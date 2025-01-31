using System;

namespace HRIS.Core.Dto
{
    public record LeaveDto(string employeeId, string leaveType, DateTime startDate, DateTime endDate, string status);
}
