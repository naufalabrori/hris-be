
namespace HRIS.Core.Dto
{
    public record PerformanceReviewDto(string employeeId, DateTime reviewDate, string reviewerId, decimal rating, string comments);

    public record PerformanceReviewQueryDto(string employeeId, DateTime reviewDate, string reviewerId, decimal rating, string comments, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record PerformanceReviewsResponseDto(List<PerformanceReview> Data, int TotalData);
}
