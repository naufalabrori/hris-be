
using HRIS.Core.Dto;

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
            var query = (from rp in _hrisContext.RolePermissions
                            join p in _hrisContext.Permissions on rp.PermissionId equals p.Id
                            join r in _hrisContext.Roles on rp.RoleId equals r.Id
                            select new RolePermissionExtDto
                            {
                                Id = rp.Id,
                                RoleId = rp.RoleId,
                                PermissionId = rp.PermissionId,
                                PermissionName = p.PermissionName,
                                Action = p.Action ?? string.Empty,
                                Resource = p.Resource ?? string.Empty,
                                IsActive = rp.IsActive,
                                CreatedBy = rp.CreatedBy,
                                CreatedByName = rp.CreatedByName,
                                CreatedDate = rp.CreatedDate,
                                ModifiedBy = rp.ModifiedBy,
                                ModifiedByName = rp.ModifiedByName,
                                ModifiedDate = rp.ModifiedDate,
                            });

            if (!string.IsNullOrWhiteSpace(rolePermissionQueryDto.roleId))
            {
                query = query.Where(x => x.RoleId.ToString() == rolePermissionQueryDto.roleId);
            }
            if (!string.IsNullOrWhiteSpace(rolePermissionQueryDto.permissionId))
            {
                query = query.Where(x => x.PermissionId.ToString() == rolePermissionQueryDto.permissionId);
            }
            if (!string.IsNullOrWhiteSpace(rolePermissionQueryDto?.permissionName))
            {
                query = query.Where(x => x.PermissionName.ToUpper().Contains(rolePermissionQueryDto.permissionName.ToUpper()));
            }
            if (!string.IsNullOrWhiteSpace(rolePermissionQueryDto?.action))
            {
                query = query.Where(x => x.Action.ToUpper().Contains(rolePermissionQueryDto.action.ToUpper()));
            }
            if (!string.IsNullOrWhiteSpace(rolePermissionQueryDto?.resource))
            {
                query = query.Where(x => x.Resource.ToUpper().Contains(rolePermissionQueryDto.resource.ToUpper()));
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

        public async Task<RolePermission?> GetByRoleIdAndPermissionIdAsync(string roleId, string permissionId, CancellationToken cancellationToken)
        {
            var rolePermission = await _hrisContext.RolePermissions.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.RoleId.ToString() == roleId && x.PermissionId.ToString() == permissionId, cancellationToken);
            return rolePermission;
        }
    }
}
