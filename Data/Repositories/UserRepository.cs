
namespace HRIS.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HrisContext _hrisContext;

        public UserRepository(HrisContext context)
        {
            _hrisContext = context;
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            _hrisContext.Users.Add(user);
        }

        public async Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid uid = Guid.Parse(id);
            var user = await _hrisContext.Users.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == uid, cancellationToken);
            return user;
        }

        public async Task<User?> GetByEmployeeId(string employeeId, CancellationToken cancellationToken)
        {
            Guid empId = Guid.Parse(employeeId);
            var user = await _hrisContext.Users.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.EmployeeId == empId, cancellationToken);
            return user;
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _hrisContext.Users.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Email == email, cancellationToken); 
            return user;
        }

        public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            var user = await _hrisContext.Users.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
            return user;
        }

        public async Task<UsersResponseDto> GetAllAsync(UserQueryDto userQueryDto, CancellationToken cancellationToken)
        {
            var query = from user in _hrisContext.Users
                        select new UserResponseDto
                        {
                            Id = user.Id,
                            EmployeeId = user.EmployeeId,
                            Email = user.Email,
                            Username = user.Username,
                            LastLogin = user.LastLogin,
                            IsActive = user.IsActive,
                            CreatedBy = user.CreatedBy,
                            CreatedByName = user.CreatedByName,
                            CreatedDate = user.CreatedDate,
                            ModifiedBy = user.ModifiedBy,
                            ModifiedByName = user.ModifiedByName,
                            ModifiedDate = user.ModifiedDate,
                        };

            if (!string.IsNullOrWhiteSpace(userQueryDto.employeeId))
            {
                query = query.Where(x => x.EmployeeId == Guid.Parse(userQueryDto.employeeId));
            }
            if (!string.IsNullOrWhiteSpace(userQueryDto.email))
            {
                query = query.Where(x => x.Email.Contains(userQueryDto.email));
            }
            if (!string.IsNullOrWhiteSpace(userQueryDto.username))
            {
                query = query.Where(x => x.Username.Contains(userQueryDto.username));
            }
            if (userQueryDto?.lastLogin != null && userQueryDto.lastLogin != DateTime.MinValue)
            {
                query = query.Where(x => x.LastLogin.Value.Date == userQueryDto.lastLogin.Date);
            }
            if (!string.IsNullOrWhiteSpace(userQueryDto?.sortBy) && userQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{userQueryDto.sortBy} {(userQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(userQueryDto.offset)
                .Take(userQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new UsersResponseDto(page, totalData);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _hrisContext.Users.Update(user);
        }

        public async Task DeleteAsync(User user, CancellationToken cancellationToken)
        {
            _hrisContext.Users.Remove(user);
        }
    }
}
