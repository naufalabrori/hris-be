
namespace HRIS.Core.Interfaces.Repositories
{
    public interface IAttendanceRepository
    {
        public Task AddAsync(Attendance attendance, CancellationToken cancellationToken);
        public Task<Attendance?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<List<Attendance>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken);
        public Task<AttendancesResponseDto> GetAllAsync(AttendanceQueryDto attendanceQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(Attendance attendance, CancellationToken cancellationToken);
        public Task DeleteAsync(Attendance attendance, CancellationToken cancellationToken);
    }
}
