
namespace HRIS.Core.Dto
{
    public record MenuDto(string menuName);

    public record MenuQueryDto(string menuName, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record MenusResponseDto(List<Menu> Data, int TotalData);
}
