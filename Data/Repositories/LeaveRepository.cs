
namespace HRIS.Data.Repositories
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly HrisContext _hrisContext;

        public LeaveRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(Leave leave, CancellationToken cancellationToken)
        {
            _hrisContext.Leaves.Add(leave);
        }

        public async Task<Leave?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid leaveId = Guid.Parse(id);
            var leave = await _hrisContext.Leaves.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == leaveId, cancellationToken);
            return leave;
        }

        public async Task<List<Leave>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken)
        {
            var leaves = await _hrisContext.Leaves.IsActiveRows().AsNoTracking().Where(x => x.EmployeeId == Guid.Parse(employeeId)).ToListAsync(cancellationToken);
            return leaves;
        }

        public async Task<LeavesResponseDto> GetAllAsync(LeaveQueryDto leaveQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.Leaves.Select(x => x);

            if (!string.IsNullOrWhiteSpace(leaveQueryDto?.sortBy) && leaveQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{leaveQueryDto.sortBy} {(leaveQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(leaveQueryDto.offset)
                .Take(leaveQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new LeavesResponseDto(page, totalData);
        }

        public async Task UpdateAsync(Leave leave, CancellationToken cancellationToken)
        {
            _hrisContext.Leaves.Update(leave);
        }

        public async Task DeleteAsync(Leave leave, CancellationToken cancellationToken)
        {
            _hrisContext.Leaves.Remove(leave);
        }
    }
}
