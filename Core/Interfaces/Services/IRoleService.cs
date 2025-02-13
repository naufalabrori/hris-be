
namespace HRIS.Core.Interfaces.Services
{
    public interface IRoleService
    {
        public Task<ApiResponseDto<Role?>> CreateRoleAsync(RoleDto role, CancellationToken cancellationToken);
        public Task<ApiResponseDto<RolesResponseDto>> ReadRolesAsync(RoleQueryDto roleQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Role?>> ReadRoleByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Role?>> UpdateRoleAsync(string id, RoleDto updateRole, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeleteRoleAsync(string id, CancellationToken cancellationToken);
    }
}
