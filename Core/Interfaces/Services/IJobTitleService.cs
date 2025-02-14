
namespace HRIS.Core.Interfaces.Services
{
    public interface IJobTitleService
    {
        public Task<ApiResponseDto<JobTitle?>> CreateJobTitleAsync(JobTitleDto jobTitle, CancellationToken cancellationToken);
        public Task<ApiResponseDto<JobTitlesResponseDto>> ReadJobTitlesAsync(JobTitleQueryDto jobTitleQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<JobTitle?>> ReadJobTitleByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<JobTitle?>> UpdateJobTitleAsync(string id, JobTitleDto updateJobTitle, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeleteJobTitleAsync(string id, CancellationToken cancellationToken);
    }
}
