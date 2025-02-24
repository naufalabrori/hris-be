
namespace HRIS.Core.Dto
{
    public record RolePermissionDto(string roleId, string permissionId);

    public record RolePermissionQueryDto(string roleId, string permissionId, string permissionName, string action, string resource, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public class RolePermissionExtDto : RolePermission
    {
        public string PermissionName { get; set; } = string.Empty;
        public string Action {  get; set; } = string.Empty;
        public string Resource { get; set; } = string.Empty;
    }

    public record RolePermissionsResponseDto(List<RolePermissionExtDto> Data, int TotalData);
}
