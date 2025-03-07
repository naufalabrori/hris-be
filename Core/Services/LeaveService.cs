
namespace HRIS.Core.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly IHrisRepository _hrisRepository;

        public LeaveService(ILeaveRepository leaveRepository, IHrisRepository hrisRepository)
        {
            _leaveRepository = leaveRepository;
            _hrisRepository = hrisRepository;
        }

        public async Task<ApiResponseDto<Leave?>> CreateLeaveAsync(LeaveDto leave, CancellationToken cancellationToken)
        {
            var existingLeave = await _leaveRepository.GetByEmployeeIdAsync(leave.employeeId, cancellationToken);
            if (existingLeave.Count > 0)
            {
                existingLeave = existingLeave.Where(x => x.LeaveType == leave.leaveType && x.StartDate.Date == leave.startDate.Date && x.EndDate.Date == leave.endDate.Date).ToList();

                if (existingLeave.Count > 0)
                {
                    return new ApiResponseDto<Leave?>
                    {
                        Success = false,
                        Message = $"Leave already exist for this leave type ({leave.leaveType}) in {leave.startDate.Date} into {leave.endDate.Date}"
                    };
                }
            }

            var newLeave = new Leave(leave);

            await _leaveRepository.AddAsync(newLeave, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Leave?>
            {
                Success = true,
                Message = "Create leave successfully",
                Data = newLeave
            };
        }

        public async Task<ApiResponseDto<LeavesResponseDto>> ReadLeavesAsync(LeaveQueryDto leaveQueryDto, CancellationToken cancellationToken)
        {
            var data = await _leaveRepository.GetAllAsync(leaveQueryDto, cancellationToken);

            return new ApiResponseDto<LeavesResponseDto>
            {
                Success = true,
                Message = "Get all leave successfully",
                Data = data
            };
        }

        public async Task<ApiResponseDto<Leave?>> ReadLeaveByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<Leave?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var leave = await _leaveRepository.GetByIdAsync(id, cancellationToken);
            if (leave == null)
            {
                return new ApiResponseDto<Leave?>
                {
                    Success = false,
                    Message = "Leave not found"
                };
            }

            return new ApiResponseDto<Leave?>
            {
                Success = true,
                Message = "Get leave successfully",
                Data = leave
            };
        }

        public async Task<ApiResponseDto<Leave?>> UpdateLeaveAsync(string id, LeaveDto updateLeave, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<Leave?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var leave = await _leaveRepository.GetByIdAsync(id, cancellationToken);
            if (leave == null)
            {
                return new ApiResponseDto<Leave?>
                {
                    Success = false,
                    Message = "Leave not found"
                };
            }

            leave.UpdateLeave(updateLeave);

            await _leaveRepository.UpdateAsync(leave, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Leave?>
            {
                Success = true,
                Message = "Update leave successfully",
                Data = leave
            };
        }

        public async Task<ApiResponseDto<bool>> DeleteLeaveAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var leave = await _leaveRepository.GetByIdAsync(id, cancellationToken);
            if (leave == null)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Leave not found"
                };
            }

            await _leaveRepository.DeleteAsync(leave, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<bool>
            {
                Success = true,
                Message = "Delete leave successfully",
                Data = true,
            };
        }

        public async Task<ApiResponseDto<bool>> ApproveLeaveAsync(string id, CancellationToken cancellationToken)
        {
            var leave = await _leaveRepository.GetByIdAsync(id, cancellationToken);
            if (leave == null)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Leave not found"
                };
            }

            leave.Status = "Approve";

            await _leaveRepository.UpdateAsync(leave, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<bool>
            {
                Success = true,
                Message = "Approve leave successfully",
                Data = true,
            };
        }
    }
}
