
namespace HRIS.Core.Dto
{
    public record JobTitleDto(string title, string description, decimal minSalary, decimal maxSalary);

    public record JobTitleUpdateDto(string title, string description, decimal minSalary, decimal maxSalary, bool isActive);

    public record JobTitleQueryDto(string title, string description, decimal? minSalary, decimal? maxSalary, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record JobTitlesResponseDto(List<JobTitle> Data, int TotalData);
}
