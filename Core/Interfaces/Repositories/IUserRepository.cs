using HRIS.Core.Dto;
using HRIS.Core.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace HRIS.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task AddAsync(User user, CancellationToken cancellationToken);
        public Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<User?> GetByEmployeeId(string employeeId, CancellationToken cancellationToken);
        public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
        public Task<UsersResponseDto> GetAllAsync(UserQueryDto userQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(User user, CancellationToken cancellationToken);
        public Task DeleteAsync(User user, CancellationToken cancellationToken);
    }
}
