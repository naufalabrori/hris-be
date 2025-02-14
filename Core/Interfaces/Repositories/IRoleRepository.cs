
namespace HRIS.Core.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        public Task AddAsync(Role role, CancellationToken cancellationToken);
        public Task<Role?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<Role?> GetByRolenameAsync(string rolename, CancellationToken cancellationToken);
        public Task<RolesResponseDto> GetAllAsync(RoleQueryDto roleQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(Role role, CancellationToken cancellationToken);
        public Task DeleteAsync(Role role, CancellationToken cancellationToken);
    }
}
