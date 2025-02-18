
namespace HRIS.Core.Interfaces.Services
{
    public interface IRecruitmentService
    {
        public Task<ApiResponseDto<Recruitment?>> CreateRecruitmentAsync(RecruitmentDto recruitment, CancellationToken cancellationToken);
        public Task<ApiResponseDto<RecruitmentsResponseDto>> ReadRecruitmentsAsync(RecruitmentQueryDto recruitmentQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Recruitment?>> ReadRecruitmentByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Recruitment?>> UpdateRecruitmentAsync(string id, RecruitmentDto updateRecruitment, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeleteRecruitmentAsync(string id, CancellationToken cancellationToken);
    }
}
