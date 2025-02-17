
namespace HRIS.Data.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly HrisContext _hrisContext;

        public AttendanceRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(Attendance attendance, CancellationToken cancellationToken)
        {
            _hrisContext.Attendances.Add(attendance);
        }

        public async Task<Attendance?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid attendanceId = Guid.Parse(id);
            var attendance = await _hrisContext.Attendances.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == attendanceId, cancellationToken);
            return attendance;
        }

        public async Task<List<Attendance>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken)
        {
            var attendances = await _hrisContext.Attendances.IsActiveRows().AsNoTracking().Where(x => x.EmployeeId == Guid.Parse(employeeId)).ToListAsync(cancellationToken);
            return attendances;
        }

        public async Task<AttendancesResponseDto> GetAllAsync(AttendanceQueryDto attendanceQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.Attendances.IsActiveRows().Select(x => x);

            if (!string.IsNullOrWhiteSpace(attendanceQueryDto.employeeId))
            {
                query = query.Where(x => x.EmployeeId.ToString() ==  attendanceQueryDto.employeeId);
            }
            if(attendanceQueryDto?.date != null && attendanceQueryDto.date != DateTime.MinValue)
            {
                query = query.Where(x => x.Date.Date ==  attendanceQueryDto.date.Date);
            }
            if (!string.IsNullOrWhiteSpace(attendanceQueryDto?.sortBy) && attendanceQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{attendanceQueryDto.sortBy} {(attendanceQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(attendanceQueryDto.offset)
                .Take(attendanceQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new AttendancesResponseDto(page, totalData);
        }

        public async Task UpdateAsync(Attendance attendance, CancellationToken cancellationToken)
        {
            _hrisContext.Attendances.Update(attendance);
        }

        public async Task DeleteAsync(Attendance attendance, CancellationToken cancellationToken)
        {
            _hrisContext.Attendances.Remove(attendance);
        }
    }
}
