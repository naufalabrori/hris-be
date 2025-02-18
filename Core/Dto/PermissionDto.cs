
namespace HRIS.Core.Dto
{
    public record PermissionDto(string permissionName, string action, string resource);

    public record PermissionQueryDto(string permissionName, string action, string resource, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record PermissionsResponseDto(List<Permission> Data, int TotalData);
}
