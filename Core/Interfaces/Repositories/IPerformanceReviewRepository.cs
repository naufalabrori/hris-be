
namespace HRIS.Core.Interfaces.Repositories
{
    public interface IPerformanceReviewRepository
    {
        public Task AddAsync(PerformanceReview performanceReview, CancellationToken cancellationToken);
        public Task<PerformanceReview?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<PerformanceReviewsResponseDto> GetAllAsync(PerformanceReviewQueryDto performanceReviewQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(PerformanceReview performanceReview, CancellationToken cancellationToken);
        public Task DeleteAsync(PerformanceReview performanceReview, CancellationToken cancellationToken);
    }
}
