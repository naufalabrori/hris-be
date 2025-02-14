
namespace HRIS.Core.Interfaces.Services
{
    public interface IUserRoleService
    {
        public Task<ApiResponseDto<UserRole?>> CreateUserRoleAsync(UserRoleDto userRole, CancellationToken cancellationToken);
        public Task<ApiResponseDto<UserRolesResponseDto>> ReadUserRolesAsync(UserRoleQueryDto userRoleQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<UserRole?>> ReadUserRoleByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<UserRole?>> UpdateUserRoleAsync(string id, UserRoleDto updateUserRole, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeleteUserRoleAsync(string id, CancellationToken cancellationToken);
    }
}
