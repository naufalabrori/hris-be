
using HRIS.Core.Dto;

namespace HRIS.Data.Repositories
{
    public class PerformanceReviewRepository : IPerformanceReviewRepository
    {
        private readonly HrisContext _hrisContext;

        public PerformanceReviewRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(PerformanceReview performanceReview, CancellationToken cancellationToken)
        {
            _hrisContext.PerformanceReviews.Add(performanceReview);
        }

        public async Task<PerformanceReview?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid performanceReviewId = Guid.Parse(id);
            var performanceReview = await _hrisContext.PerformanceReviews.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == performanceReviewId, cancellationToken);
            return performanceReview;
        }

        public async Task<PerformanceReviewsResponseDto> GetAllAsync(PerformanceReviewQueryDto performanceReviewQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.PerformanceReviews.IsActiveRows().AsNoTracking().Select(x => x);

            if (!string.IsNullOrWhiteSpace(performanceReviewQueryDto.employeeId))
            {
                query = query.Where(x => x.EmployeeId.ToString() == performanceReviewQueryDto.employeeId);
            }
            if (!string.IsNullOrWhiteSpace(performanceReviewQueryDto.reviewerId))
            {
                query = query.Where(x => x.ReviewerId.ToString() == performanceReviewQueryDto.reviewerId);
            }
            if (performanceReviewQueryDto?.reviewDate != null && performanceReviewQueryDto.reviewDate != DateTime.MinValue)
            {
                query = query.Where(x => x.ReviewDate.Date == performanceReviewQueryDto.reviewDate.Date);
            }
            if (performanceReviewQueryDto?.rating != null)
            {
                query = query.Where(x => x.Rating == performanceReviewQueryDto.rating);
            }
            if (!string.IsNullOrWhiteSpace(performanceReviewQueryDto?.comments))
            {
                query = query.Where(x => x.Comments.Contains(performanceReviewQueryDto.comments));
            }
            if (!string.IsNullOrWhiteSpace(performanceReviewQueryDto?.sortBy) && performanceReviewQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{performanceReviewQueryDto.sortBy} {(performanceReviewQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(performanceReviewQueryDto.offset)
                .Take(performanceReviewQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new PerformanceReviewsResponseDto(page, totalData);
        }

        public async Task UpdateAsync(PerformanceReview performanceReview, CancellationToken cancellationToken)
        {
            _hrisContext.PerformanceReviews.Update(performanceReview);
        }

        public async Task DeleteAsync(PerformanceReview performanceReview, CancellationToken cancellationToken)
        {
            _hrisContext.PerformanceReviews.Remove(performanceReview);
        }
    }
}
