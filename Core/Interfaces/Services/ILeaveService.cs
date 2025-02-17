
namespace HRIS.Core.Interfaces.Services
{
    public interface ILeaveService
    {
        public Task<ApiResponseDto<Leave?>> CreateLeaveAsync(LeaveDto leave, CancellationToken cancellationToken);
        public Task<ApiResponseDto<LeavesResponseDto>> ReadLeavesAsync(LeaveQueryDto leaveQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Leave?>> ReadLeaveByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Leave?>> UpdateLeaveAsync(string id, LeaveDto updateLeave, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeleteLeaveAsync(string id, CancellationToken cancellationToken);
    }
}
