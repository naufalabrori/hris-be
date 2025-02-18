
namespace HRIS.Data.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly HrisContext _hrisContext;

        public PermissionRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(Permission permission, CancellationToken cancellationToken)
        {
            _hrisContext.Permissions.Add(permission);
        }

        public async Task<Permission?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid permissionId = Guid.Parse(id);
            var permission = await _hrisContext.Permissions.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == permissionId, cancellationToken);
            return permission;
        }

        public async Task<Permission?> GetByPermissionNameAsync(string permissionName, CancellationToken cancellationToken)
        {
            var permission = await _hrisContext.Permissions.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.PermissionName == permissionName, cancellationToken);
            return permission;
        }

        public async Task<PermissionsResponseDto> GetAllAsync(PermissionQueryDto permissionQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.Permissions.IsActiveRows().Select(x => x);

            if (!string.IsNullOrWhiteSpace(permissionQueryDto.permissionName))
            {
                query = query.Where(x => x.PermissionName.Contains(permissionQueryDto.permissionName));
            }
            if (!string.IsNullOrWhiteSpace(permissionQueryDto.action))
            {
                query = query.Where(x => x.Action.Contains(permissionQueryDto.action));
            }
            if (!string.IsNullOrWhiteSpace(permissionQueryDto.resource))
            {
                query = query.Where(x => x.Resource.Contains(permissionQueryDto.resource));
            }
            if (!string.IsNullOrWhiteSpace(permissionQueryDto?.sortBy) && permissionQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{permissionQueryDto.sortBy} {(permissionQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(permissionQueryDto.offset)
                .Take(permissionQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new PermissionsResponseDto(page, totalData);
        }

        public async Task UpdateAsync(Permission permission, CancellationToken cancellationToken)
        {
            _hrisContext.Permissions.Update(permission);
        }

        public async Task DeleteAsync(Permission permission, CancellationToken cancellationToken)
        {
            _hrisContext.Permissions.Remove(permission);
        }
    }
}
