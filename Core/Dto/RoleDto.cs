using HRIS.Core.Entity;
using System.Collections.Generic;

namespace HRIS.Core.Dto
{
    public record RoleDto(string roleName);

    public record RoleQueryDto(string roleName, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public record RolesResponseDto(List<Role> Data, int TotalData);
}
