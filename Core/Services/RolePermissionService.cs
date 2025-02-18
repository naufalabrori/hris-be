namespace HRIS.Core.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IHrisRepository _hrisRepository;

        public RolePermissionService(IRolePermissionRepository rolePermissionRepository, IHrisRepository hrisRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _hrisRepository = hrisRepository;
        }

        public async Task<ApiResponseDto<RolePermission?>> CreateRolePermissionAsync(RolePermissionDto rolePermission, CancellationToken cancellationToken)
        {
            try
            {
                var newRolePermission = new RolePermission(rolePermission);

                await _rolePermissionRepository.AddAsync(newRolePermission, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<RolePermission?>
                {
                    Success = true,
                    Message = "Create RolePermission successfully",
                    Data = newRolePermission
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<RolePermission?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<RolePermissionsResponseDto>> ReadRolePermissionsAsync(RolePermissionQueryDto rolePermissionQueryDto, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _rolePermissionRepository.GetAllAsync(rolePermissionQueryDto, cancellationToken);

                return new ApiResponseDto<RolePermissionsResponseDto>
                {
                    Success = true,
                    Message = "Get all RolePermission successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<RolePermissionsResponseDto>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<RolePermission?>> ReadRolePermissionByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<RolePermission?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var rolePermission = await _rolePermissionRepository.GetByIdAsync(id, cancellationToken);
                if (rolePermission == null)
                {
                    return new ApiResponseDto<RolePermission?>
                    {
                        Success = false,
                        Message = "RolePermission not found"
                    };
                }

                return new ApiResponseDto<RolePermission?>
                {
                    Success = true,
                    Message = "Get RolePermission successfully",
                    Data = rolePermission
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<RolePermission?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<RolePermission?>> UpdateRolePermissionAsync(string id, RolePermissionDto updateRolePermission, CancellationToken cancellationToken)
        {
            try
            {
                if (!StringExtensions.IsValidGuid(id))
                {
                    return new ApiResponseDto<RolePermission?>
                    {
                        Success = false,
                        Message = "Invalid Guid format"
                    };
                }

                var rolePermission = await _rolePermissionRepository.GetByIdAsync(id, cancellationToken);
                if (rolePermission == null)
                {
                    return new ApiResponseDto<RolePermission?>
                    {
                        Success = false,
                        Message = "RolePermission not found"
                    };
                }

                rolePermission.UpdateRolePermission(updateRolePermission);

                await _rolePermissionRepository.UpdateAsync(rolePermission, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<RolePermission?>
                {
                    Success = true,
                    Message = "Update RolePermission successfully",
                    Data = rolePermission
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<RolePermission?>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ApiResponseDto<bool>> DeleteRolePermissionAsync(string id, CancellationToken cancellationToken)
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

                var rolePermission = await _rolePermissionRepository.GetByIdAsync(id, cancellationToken);
                if (rolePermission == null)
                {
                    return new ApiResponseDto<bool>
                    {
                        Success = false,
                        Message = "RolePermission not found"
                    };
                }

                await _rolePermissionRepository.DeleteAsync(rolePermission, cancellationToken);
                await _hrisRepository.SaveChangesAsync(cancellationToken);

                return new ApiResponseDto<bool>
                {
                    Success = true,
                    Message = "Delete RolePermission successfully",
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
