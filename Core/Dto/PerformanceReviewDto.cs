using System;

namespace HRIS.Core.Dto
{
    public record PerformanceReviewDto(string employeeId, DateTime reviewDate, string reviewerId, decimal rating, string comments);
}
