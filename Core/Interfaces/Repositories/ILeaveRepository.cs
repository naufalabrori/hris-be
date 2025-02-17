
namespace HRIS.Core.Interfaces.Repositories
{
    public interface ILeaveRepository
    {
        public Task AddAsync(Leave leave, CancellationToken cancellationToken);
        public Task<Leave?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<List<Leave>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken);
        public Task<LeavesResponseDto> GetAllAsync(LeaveQueryDto leaveQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(Leave leave, CancellationToken cancellationToken);
        public Task DeleteAsync(Leave leave, CancellationToken cancellationToken);
    }
}
