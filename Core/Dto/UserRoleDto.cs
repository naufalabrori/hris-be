
namespace HRIS.Core.Dto
{
    public record UserRoleDto(string userId, string roleId);

    public record UserRoleQueryDto(string userId, string roleId, string roleName, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public class UserRoleExtDto : UserRole
    {
        public string RoleName { get; set; } = string.Empty;
    }

    public record UserRolesResponseDto(List<UserRoleExtDto> Data, int TotalData);
}
