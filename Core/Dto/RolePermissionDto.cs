
namespace HRIS.Core.Dto
{
    public record RolePermissionDto(string roleId, string permissionId);

    public record RolePermissionQueryDto(string roleId, string permissionId, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record RolePermissionsResponseDto(List<RolePermission> Data, int TotalData);
}
