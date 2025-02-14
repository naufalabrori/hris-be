
namespace HRIS.Core.Interfaces.Repositories
{
    public interface IJobTitleRepository
    {
        public Task AddAsync(JobTitle jobTitle, CancellationToken cancellationToken);
        public Task<JobTitle?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<JobTitlesResponseDto> GetAllAsync(JobTitleQueryDto jobTitleQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(JobTitle jobTitle, CancellationToken cancellationToken);
        public Task DeleteAsync(JobTitle jobTitle, CancellationToken cancellationToken);
    }
}
