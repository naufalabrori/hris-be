
namespace HRIS.Core.Dto
{
    public record UserRoleDto(string userId, string roleId);

    public record UserRoleQueryDto(string userId, string roleId, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record UserRolesResponseDto(List<UserRole> Data, int TotalData);
}
