
namespace HRIS.Core.Interfaces.Services
{
    public interface IPerformanceReviewService
    {
        public Task<ApiResponseDto<PerformanceReview?>> CreatePerformanceReviewAsync(PerformanceReviewDto performanceReview, CancellationToken cancellationToken);
        public Task<ApiResponseDto<PerformanceReviewsResponseDto>> ReadPerformanceReviewsAsync(PerformanceReviewQueryDto performanceReviewQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<PerformanceReview?>> ReadPerformanceReviewByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<PerformanceReview?>> UpdatePerformanceReviewAsync(string id, PerformanceReviewDto updatePerformanceReview, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeletePerformanceReviewAsync(string id, CancellationToken cancellationToken);
    }
}
