using System.ComponentModel.DataAnnotations;

namespace HRIS.Core.Entity
{
    public class Permission : BaseEntity
    {
        public Permission() { }
        public Permission(PermissionDto permissionDto)
        {
            PermissionName = permissionDto.permissionName;
            Action = permissionDto.action;
            Resource = permissionDto.resource;
        }

        public Guid Id { get; set; } = default!;
        [StringLength(100)]
        public string PermissionName { get; set; } = default!;
        [StringLength(10)]
        public string? Action { get; set; } = string.Empty;
        [StringLength(50)]
        public string? Resource { get; set; } = string.Empty;

        public void UpdatePermission(PermissionDto permissionDto)
        {
            PermissionName = permissionDto.permissionName ?? PermissionName;
            Action = permissionDto.action ?? Action;
            Resource = permissionDto.resource ?? Resource;
        }
    }
}
