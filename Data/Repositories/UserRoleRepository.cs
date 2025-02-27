
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
            var query = (from ur in _hrisContext.UserRoles
                            join r in _hrisContext.Roles on ur.RoleId equals r.Id
                            select new UserRoleExtDto
                            {
                                Id = ur.Id,
                                RoleId = ur.RoleId,
                                UserId = ur.UserId,
                                RoleName = r.RoleName,
                                IsActive = ur.IsActive,
                                CreatedBy = ur.CreatedBy,
                                CreatedDate = ur.CreatedDate,
                                CreatedByName = ur.CreatedByName,
                                ModifiedBy = ur.ModifiedBy,
                                ModifiedByName = ur.ModifiedByName,
                                ModifiedDate = ur.ModifiedDate,
                            });

            if (!string.IsNullOrWhiteSpace(userRoleQueryDto.userId))
            {
                query = query.Where(x => x.UserId.ToString() == userRoleQueryDto.userId);
            }
            if (!string.IsNullOrWhiteSpace(userRoleQueryDto.roleId))
            {
                query = query.Where(x => x.RoleId.ToString() == userRoleQueryDto.roleId);
            }
            if (!string.IsNullOrWhiteSpace(userRoleQueryDto?.roleName))
            {
                query = query.Where(x => x.RoleName.ToLower().Contains(userRoleQueryDto.roleName.ToLower()));
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

        public async Task<UserRole?> GetByUserIdAndRoleIdAsync(string userId, string roleId, CancellationToken cancellationToken)
        {
            var userRole = await _hrisContext.UserRoles.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.UserId.ToString() == userId && x.RoleId.ToString() == roleId, cancellationToken);
            return userRole;
        }

        public async Task<List<UserRoleExtDto>> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            var query = (from ur in _hrisContext.UserRoles
                         join r in _hrisContext.Roles on ur.RoleId equals r.Id
                         where ur.UserId.ToString() == userId
                         select new UserRoleExtDto
                         {
                             Id = ur.Id,
                             RoleId = ur.RoleId,
                             UserId = ur.UserId,
                             RoleName = r.RoleName,
                             IsActive = ur.IsActive,
                             CreatedBy = ur.CreatedBy,
                             CreatedDate = ur.CreatedDate,
                             CreatedByName = ur.CreatedByName,
                             ModifiedBy = ur.ModifiedBy,
                             ModifiedByName = ur.ModifiedByName,
                             ModifiedDate = ur.ModifiedDate,
                         });

            var userRoles = await query.ToListAsync(cancellationToken);
            return userRoles;
        }
    }
}
