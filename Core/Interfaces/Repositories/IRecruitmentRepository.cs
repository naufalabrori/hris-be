
namespace HRIS.Core.Interfaces.Repositories
{
    public interface IRecruitmentRepository
    {
        public Task AddAsync(Recruitment recruitment, CancellationToken cancellationToken);
        public Task<Recruitment?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<RecruitmentsResponseDto> GetAllAsync(RecruitmentQueryDto recruitmentQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(Recruitment recruitment, CancellationToken cancellationToken);
        public Task DeleteAsync(Recruitment recruitment, CancellationToken cancellationToken);
    }
}
