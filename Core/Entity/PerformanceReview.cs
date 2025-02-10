using HRIS.Core.Dto;
using System;

namespace HRIS.Core.Entity
{
    public class PerformanceReview : BaseEntity
    {
        public PerformanceReview() { }

        public PerformanceReview(PerformanceReviewDto performanceReview)
        {
            EmployeeId = Guid.Parse(performanceReview.employeeId);
            ReviewDate = performanceReview.reviewDate;
            ReviewerId = Guid.Parse(performanceReview.reviewerId);
            Rating = performanceReview.rating;
            Comments = performanceReview.comments;
        }

        public Guid Id { get; set; } = default!;
        public Guid EmployeeId { get; set; } = default!;
        public DateTime ReviewDate { get; set; }
        public Guid ReviewerId { get; set; } = default!;
        public decimal Rating { get; set; }
        public string Comments { get; set; } = default!;

        public void UpdatePerformanceReview(PerformanceReviewDto performanceReview)
        {
            EmployeeId = Guid.TryParse(performanceReview.employeeId, out var employeeId) ? employeeId : EmployeeId;
            ReviewDate = performanceReview?.reviewDate ?? ReviewDate;
            ReviewerId = Guid.TryParse(performanceReview.reviewerId, out var reviewerId) ? reviewerId : ReviewerId;
            Rating = performanceReview?.rating ?? Rating;
            Comments = performanceReview?.comments ?? Comments;
        }
    }
}
