
namespace HRIS.Data.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly HrisContext _hrisContext;

        public RolePermissionRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(RolePermission rolePermission, CancellationToken cancellationToken)
        {
            _hrisContext.RolePermissions.Add(rolePermission);
        }

        public async Task<RolePermission?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid rolePermissionId = Guid.Parse(id);
            var rolePermission = await _hrisContext.RolePermissions.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == rolePermissionId, cancellationToken);
            return rolePermission;
        }

        public async Task<RolePermissionsResponseDto> GetAllAsync(RolePermissionQueryDto rolePermissionQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.RolePermissions.IsActiveRows().Select(x => x);

            if (!string.IsNullOrWhiteSpace(rolePermissionQueryDto.roleId))
            {
                query = query.Where(x => x.RoleId.ToString() == rolePermissionQueryDto.roleId);
            }
            if (!string.IsNullOrWhiteSpace(rolePermissionQueryDto.permissionId))
            {
                query = query.Where(x => x.PermissionId.ToString() == rolePermissionQueryDto.permissionId);
            }
            if (!string.IsNullOrWhiteSpace(rolePermissionQueryDto?.sortBy) && rolePermissionQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{rolePermissionQueryDto.sortBy} {(rolePermissionQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
            .Skip(rolePermissionQueryDto.offset)
            .Take(rolePermissionQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new RolePermissionsResponseDto(page, totalData);
        }

        public async Task UpdateAsync(RolePermission rolePermission, CancellationToken cancellationToken)
        {
            _hrisContext.RolePermissions.Update(rolePermission);
        }

        public async Task DeleteAsync(RolePermission rolePermission, CancellationToken cancellationToken)
        {
            _hrisContext.RolePermissions.Remove(rolePermission);
        }
    }
}
