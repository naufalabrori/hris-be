using HRIS.Core.Dto;
using System;

namespace HRIS.Core.Entity
{
    public class RolePermission : BaseEntity
    {
        public RolePermission() { }

        public RolePermission(RolePermissionDto rolePermissionDto)
        {
            RoleId = Guid.Parse(rolePermissionDto.roleId);
            PermissionId = Guid.Parse(rolePermissionDto.permissionId);
        }

        public Guid Id { get; set; } = default!;
        public Guid RoleId { get; set; } = default!;
        public Guid PermissionId { get; set; } = default!;

        public void UpdateRolePermission(RolePermissionDto rolePermissionDto)
        {
            RoleId = Guid.TryParse(rolePermissionDto.roleId, out var roleIdGuid) ? roleIdGuid : RoleId;
            PermissionId = Guid.TryParse(rolePermissionDto.permissionId, out var permissionIdGuid) ? permissionIdGuid : PermissionId;
        }
    }
}
