
namespace HRIS.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly HrisContext _hrisContext;

        public RoleRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(Role role, CancellationToken cancellationToken)
        {
            _hrisContext.Roles.Add(role);
        }

        public async Task<Role?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid roleId = Guid.Parse(id);
            var role = await _hrisContext.Roles.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken);
            return role;
        }

        public async Task<Role?> GetByRolenameAsync(string rolename, CancellationToken cancellationToken)
        {
            var role = await _hrisContext.Roles.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.RoleName == rolename, cancellationToken);
            return role;
        }

        public async Task<RolesResponseDto> GetAllAsync(RoleQueryDto roleQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.Roles.IsActiveRows().Select(x => x);

            if (!string.IsNullOrWhiteSpace(roleQueryDto.roleName))
            {
                query = query.Where(x => x.RoleName.Contains(roleQueryDto.roleName));
            }
            if (!string.IsNullOrWhiteSpace(roleQueryDto?.sortBy) && roleQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{roleQueryDto.sortBy} {(roleQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(roleQueryDto.offset)
                .Take(roleQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new RolesResponseDto(page, totalData);
        }

        public async Task UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            _hrisContext.Roles.Update(role);
        }

        public async Task DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            _hrisContext.Roles.Remove(role);
        }
    }
}
