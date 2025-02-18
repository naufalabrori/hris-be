
namespace HRIS.Core.Interfaces.Repositories
{
    public interface IApplicantRepository
    {
        public Task AddAsync(Applicant applicant, CancellationToken cancellationToken);
        public Task<Applicant?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApplicantsResponseDto> GetAllAsync(ApplicantQueryDto applicantQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(Applicant applicant, CancellationToken cancellationToken);
        public Task DeleteAsync(Applicant applicant, CancellationToken cancellationToken);
    }
}
