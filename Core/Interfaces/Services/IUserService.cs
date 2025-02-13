
namespace HRIS.Core.Interfaces.Services
{
    public interface IUserService
    {
        public Task<ApiResponseDto<UserResponseDto?>> CreateUserAsync(NewUserDto user, CancellationToken cancellationToken);
        public Task<ApiResponseDto<UsersResponseDto>> ReadUsersAsync(UserQueryDto userQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<UserResponseDto?>> ReadUserByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<UserResponseDto?>> UpdateUserAsync(string id, UserDto updateUser, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeleteUserAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<UserLoginResponseDto?>> LoginUserAsync(UserLoginDto userLogin, CancellationToken cancellationToken);
    }
}
