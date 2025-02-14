
namespace HRIS.Core.Interfaces.Repositories
{
    public interface IRolePermissionRepository
    {
        public Task AddAsync(RolePermission rolePermission, CancellationToken cancellationToken);
        public Task<RolePermission?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<RolePermissionsResponseDto> GetAllAsync(RolePermissionQueryDto rolePermissionQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(RolePermission rolePermission, CancellationToken cancellationToken);
        public Task DeleteAsync(RolePermission rolePermission, CancellationToken cancellationToken);
    }
}
