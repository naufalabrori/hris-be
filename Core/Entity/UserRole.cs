using HRIS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Core.Entity
{
    public class UserRole : BaseEntity
    {
        public UserRole() { }

        public UserRole(UserRoleDto userRoleDto)
        {
            UserId = Guid.Parse(userRoleDto.userId);
            RoleId = Guid.Parse(userRoleDto.roleId);
        }

        public Guid Id { get; set; } = default!;
        public Guid UserId { get; set; } = default!;
        public Guid RoleId { get; set; } = default!;

        public void UpdateUserRole(UserRoleDto userRoleDto)
        {
            UserId = Guid.TryParse(userRoleDto.userId, out var userIdGuid) ? userIdGuid : UserId;
            RoleId = Guid.TryParse(userRoleDto.roleId, out var roleIdGuid) ? roleIdGuid : RoleId;
        }
    }
}
