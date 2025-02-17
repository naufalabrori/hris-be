
namespace HRIS.Core.Dto
{
    public record AttendanceDto(string employeeId, DateTime date, TimeOnly timeIn, TimeOnly timeOut, decimal hoursWorked);

    public record AttendanceQueryDto(string employeeId, DateTime date, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record AttendancesResponseDto(List<Attendance> Data, int TotalData);
}
