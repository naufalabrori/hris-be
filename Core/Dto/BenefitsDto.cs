
namespace HRIS.Core.Dto
{
    public record BenefitDto(string employeeId, string benefitType, DateTime startDate, DateTime endDate, string details);

    public record BenefitQueryDto(string employeeId, string benefitType, DateTime startDate, DateTime endDate, string details, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record BenefitsResponseDto(List<Benefits> Data, int TotalData);
}
