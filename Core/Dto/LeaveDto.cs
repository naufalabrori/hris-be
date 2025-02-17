
namespace HRIS.Core.Dto
{
    public record LeaveDto(string employeeId, string leaveType, DateTime startDate, DateTime endDate, string status);

    public record LeaveQueryDto(string employeeId, string leaveType, DateTime startDate, DateTime endDate, string status, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record LeavesResponseDto(List<Leave> Data, int TotalData);
}
