
namespace HRIS.Core.Interfaces.Repositories
{
    public interface IPermissionRepository
    {
        public Task AddAsync(Permission permission, CancellationToken cancellationToken);
        public Task<Permission?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<Permission?> GetByPermissionNameAsync(string permissionName, CancellationToken cancellationToken);
        public Task<PermissionsResponseDto> GetAllAsync(PermissionQueryDto permissionQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(Permission permission, CancellationToken cancellationToken);
        public Task DeleteAsync(Permission permission, CancellationToken cancellationToken);
    }
}
