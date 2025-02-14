
namespace HRIS.Core.Interfaces.Services
{
    public interface IPermissionService
    {
        public Task<ApiResponseDto<Permission?>> CreatePermissionAsync(PermissionDto permission, CancellationToken cancellationToken);
        public Task<ApiResponseDto<PermissionsResponseDto>> ReadPermissionsAsync(PermissionQueryDto permissionQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Permission?>> ReadPermissionByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Permission?>> UpdatePermissionAsync(string id, PermissionDto updatePermission, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeletePermissionAsync(string id, CancellationToken cancellationToken);
    }
}
