
namespace HRIS.Core.Interfaces.Repositories
{
    public interface IUserRoleRepository
    {
        public Task AddAsync(UserRole userRole, CancellationToken cancellationToken);
        public Task<UserRole?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<UserRolesResponseDto> GetAllAsync(UserRoleQueryDto userRoleQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(UserRole userRole, CancellationToken cancellationToken);
        public Task DeleteAsync(UserRole userRole, CancellationToken cancellationToken);
        public Task<UserRole?> GetByUserIdAndRoleIdAsync(string userId, string roleId, CancellationToken cancellationToken);
    }
}
