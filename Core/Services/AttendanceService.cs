
namespace HRIS.Core.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IHrisRepository _hrisRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository, IHrisRepository hrisRepository)
        {
            _attendanceRepository = attendanceRepository;
            _hrisRepository = hrisRepository;
        }

        public async Task<ApiResponseDto<Attendance?>> CreateAttendanceAsync(AttendanceDto attendance, CancellationToken cancellationToken)
        {
            var newAttendance = new Attendance(attendance);

            await _attendanceRepository.AddAsync(newAttendance, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Attendance?>
            {
                Success = true,
                Message = "Create attendance successfully",
                Data = newAttendance
            };
        }

        public async Task<ApiResponseDto<AttendancesResponseDto>> ReadAttendancesAsync(AttendanceQueryDto attendanceQueryDto, CancellationToken cancellationToken)
        {
            var data = await _attendanceRepository.GetAllAsync(attendanceQueryDto, cancellationToken);

            return new ApiResponseDto<AttendancesResponseDto>
            {
                Success = true,
                Message = "Get all atteandance successfully",
                Data = data
            };
        }

        public async Task<ApiResponseDto<Attendance?>> ReadAttendanceByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<Attendance?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var atteandance = await _attendanceRepository.GetByIdAsync(id, cancellationToken);
            if (atteandance == null)
            {
                return new ApiResponseDto<Attendance?>
                {
                    Success = false,
                    Message = "Attendance not found"
                };
            }

            return new ApiResponseDto<Attendance?>
            {
                Success = true,
                Message = "Get attendance successfully",
                Data = atteandance
            };
        }

        public async Task<ApiResponseDto<Attendance?>> UpdateAttendanceAsync(string id, AttendanceDto updateAttendance, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<Attendance?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var atteandance = await _attendanceRepository.GetByIdAsync(id, cancellationToken);
            if (atteandance == null)
            {
                return new ApiResponseDto<Attendance?>
                {
                    Success = false,
                    Message = "Attendance not found"
                };
            }

            atteandance.UpdateAttendance(updateAttendance);

            await _attendanceRepository.UpdateAsync(atteandance, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Attendance?>
            {
                Success = true,
                Message = "Update attendance successfully",
                Data = atteandance
            };
        }

        public async Task<ApiResponseDto<bool>> DeleteAttendanceAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var atteandance = await _attendanceRepository.GetByIdAsync(id, cancellationToken);
            if (atteandance == null)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Attendance not found"
                };
            }

            await _attendanceRepository.DeleteAsync(atteandance, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<bool>
            {
                Success = true,
                Message = "Delete atteandane successfully",
                Data = true
            };
        }
    }
}
