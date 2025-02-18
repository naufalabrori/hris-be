
namespace HRIS.Core.Interfaces.Services
{
    public interface IApplicantService
    {
        public Task<ApiResponseDto<Applicant?>> CreateApplicantAsync(ApplicantDto applicant, CancellationToken cancellationToken);
        public Task<ApiResponseDto<ApplicantsResponseDto>> ReadApplicantsAsync(ApplicantQueryDto applicantQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Applicant?>> ReadApplicantByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Applicant?>> UpdateApplicantAsync(string id, ApplicantDto updateApplicant, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeleteApplicantAsync(string id, CancellationToken cancellationToken);
    }
}
