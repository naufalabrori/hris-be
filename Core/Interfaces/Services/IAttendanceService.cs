
namespace HRIS.Core.Interfaces.Services
{
    public interface IAttendanceService
    {
        public Task<ApiResponseDto<Attendance?>> CreateAttendanceAsync(AttendanceDto attendance, CancellationToken cancellationToken);
        public Task<ApiResponseDto<AttendancesResponseDto>> ReadAttendancesAsync(AttendanceQueryDto attendanceQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Attendance?>> ReadAttendanceByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Attendance?>> UpdateAttendanceAsync(string id, AttendanceDto updateAttendance, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeleteAttendanceAsync(string id, CancellationToken cancellationToken);
    }
}
