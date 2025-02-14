
namespace HRIS.Data.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly HrisContext _hrisContext;

        public UserRoleRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(UserRole userRole, CancellationToken cancellationToken)
        {
            _hrisContext.UserRoles.Add(userRole);
        }

        public async Task<UserRole?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid userRoleId = Guid.Parse(id);
            var userRole = await _hrisContext.UserRoles.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == userRoleId, cancellationToken);
            return userRole;
        }

        public async Task<UserRolesResponseDto> GetAllAsync(UserRoleQueryDto userRoleQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.UserRoles.IsActiveRows().Select(x => x);

            if (!string.IsNullOrWhiteSpace(userRoleQueryDto.userId))
            {
                query = query.Where(x => x.UserId.ToString() == userRoleQueryDto.userId);
            }
            if (!string.IsNullOrWhiteSpace(userRoleQueryDto.roleId))
            {
                query = query.Where(x => x.RoleId.ToString() == userRoleQueryDto.roleId);
            }
            if (!string.IsNullOrWhiteSpace(userRoleQueryDto?.sortBy) && userRoleQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{userRoleQueryDto.sortBy} {(userRoleQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(userRoleQueryDto.offset)
                .Take(userRoleQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new UserRolesResponseDto(page, totalData);
        }

        public async Task UpdateAsync(UserRole userRole, CancellationToken cancellationToken)
        {
            _hrisContext.UserRoles.Update(userRole);
        }

        public async Task DeleteAsync(UserRole userRole, CancellationToken cancellationToken)
        {
            _hrisContext.UserRoles.Remove(userRole);
        }
    }
}
