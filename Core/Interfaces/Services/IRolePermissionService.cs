namespace HRIS.Core.Interfaces.Services
{
    public interface IRolePermissionService
    {
        public Task<ApiResponseDto<RolePermission?>> CreateRolePermissionAsync(RolePermissionDto rolePermission, CancellationToken cancellationToken);
        public Task<ApiResponseDto<RolePermissionsResponseDto>> ReadRolePermissionsAsync(RolePermissionQueryDto rolePermissionQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<RolePermission?>> ReadRolePermissionByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<RolePermission?>> UpdateRolePermissionAsync(string id, RolePermissionDto updateRolePermission, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeleteRolePermissionAsync(string id, CancellationToken cancellationToken);
    }
}
