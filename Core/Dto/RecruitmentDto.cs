
namespace HRIS.Core.Dto
{
    public record RecruitmentDto(string jobTitleId, string departmentId, DateTime postingDate, DateTime closingDate, string status);

    public record RecruitmentQueryDto(string jobTitleId, string departmentId, DateTime postingDate, DateTime closingDate, string status, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record RecruitmentsResponseDto(List<Recruitment> Data, int TotalData);
}
