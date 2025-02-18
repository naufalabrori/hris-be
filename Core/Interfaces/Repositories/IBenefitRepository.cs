
namespace HRIS.Core.Interfaces.Repositories
{
    public interface IBenefitRepository
    {
        public Task AddAsync(Benefits benefit, CancellationToken cancellationToken);
        public Task<Benefits?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<List<Benefits>> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken);
        public Task<BenefitsResponseDto> GetAllAsync(BenefitQueryDto benefitQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(Benefits benefit, CancellationToken cancellationToken);
        public Task DeleteAsync(Benefits benefit, CancellationToken cancellationToken);
    }
}
