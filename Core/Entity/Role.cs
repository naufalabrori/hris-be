using HRIS.Core.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Core.Entity
{
    public class Role : BaseEntity
    {
        public Role() { }

        public Role(RoleDto roleDto)
        {
            RoleName = roleDto.roleName;
        }

        public Guid Id { get; set; } = default!;
        [StringLength(50)]
        public string RoleName { get; set; } = default!;

        public void UpdateRole(RoleDto roleDto)
        {
            RoleName = roleDto.roleName ?? RoleName;
        }
    }
}
