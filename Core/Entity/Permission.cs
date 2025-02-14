using HRIS.Core.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Core.Entity
{
    public class Permission : BaseEntity
    {
        public Permission() { }
        public Permission(PermissionDto permissionDto)
        {
            PermissionName = permissionDto.permissionName;
        }

        public Guid Id { get; set; } = default!;
        [StringLength(100)]
        public string PermissionName { get; set; } = default!;

        public void UpdatePermission(PermissionDto permissionDto)
        {
            PermissionName = permissionDto.permissionName ?? PermissionName;
        }
    }
}
