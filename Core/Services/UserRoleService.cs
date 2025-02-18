
namespace HRIS.Core.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IHrisRepository _hrisRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository, IHrisRepository hrisRepository)
        {
            _userRoleRepository = userRoleRepository;
            _hrisRepository = hrisRepository;
        }

        public async Task<ApiResponseDto<UserRole?>> CreateUserRoleAsync(UserRoleDto userRole, CancellationToken cancellationToken)
        {
            try
            {
                var newUserRole = new UserRole(userRole);

                await _userRoleRepository.AddAsync(newUserRole, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<UserRole?>
                {
                    Success = true,
                    Message = "Create UserRole successfully",
                    Data = newUserRole
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<UserRole?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<UserRolesResponseDto>> ReadUserRolesAsync(UserRoleQueryDto userRoleQueryDto, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _userRoleRepository.GetAllAsync(userRoleQueryDto, cancellationToken);

                return new ApiResponseDto<UserRolesResponseDto>
                {
                    Success = true,
                    Message = "Get all UserRole successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<UserRolesResponseDto>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<UserRole?>> ReadUserRoleByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<UserRole?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var userRole = await _userRoleRepository.GetByIdAsync(id, cancellationToken);
                if (userRole == null)
                {
                    return new ApiResponseDto<UserRole?>
                    {
                        Success = false,
                        Message = "UserRole not found"
                    };
                }

                return new ApiResponseDto<UserRole?>
                {
                    Success = true,
                    Message = "Get UserRole successfully",
                    Data = userRole
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<UserRole?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<UserRole?>> UpdateUserRoleAsync(string id, UserRoleDto updateUserRole, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<UserRole?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var userRole = await _userRoleRepository.GetByIdAsync(id, cancellationToken);
                if (userRole == null)
                {
                    return new ApiResponseDto<UserRole?>
                    {
                        Success = false,
                        Message = "UserRole not found"
                    };
                }

                userRole.UpdateUserRole(updateUserRole);

                await _userRoleRepository.UpdateAsync(userRole, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<UserRole?>
                {
                    Success = true,
                    Message = "Update UserRole successfully",
                    Data = userRole
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<UserRole?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<bool>> DeleteUserRoleAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<bool>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var userRole = await _userRoleRepository.GetByIdAsync(id, cancellationToken);
                if (userRole == null)
                {
                    return new ApiResponseDto<bool>
                    {
                        Success = false,
                        Message = "UserRole not found"
                    };
                }

                await _userRoleRepository.DeleteAsync(userRole, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Message = "Delete UserRole successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
