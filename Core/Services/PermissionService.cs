
namespace HRIS.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IHrisRepository _hrisRepository;

        public PermissionService(IPermissionRepository permissionRepository, IHrisRepository hrisRepository)
        {
            _permissionRepository = permissionRepository;
            _hrisRepository = hrisRepository;
        }

        public async Task<ApiResponseDto<Permission?>> CreatePermissionAsync(PermissionDto permission, CancellationToken cancellationToken)
        {
            var existingPermission = await _permissionRepository.GetByPermissionNameAsync(permission.permissionName, cancellationToken);
            if (existingPermission != null)
            {
                return new ApiResponseDto<Permission?>
                {
                    Success = false,
                    Message = "Permission name already exist"
                };
            }

            var newPermission = new Permission(permission);

            await _permissionRepository.AddAsync(newPermission, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Permission?>
            {
                Success = true,
                Message = "Create permission successfully",
                Data = newPermission
            };
        }

        public async Task<ApiResponseDto<PermissionsResponseDto>> ReadPermissionsAsync(PermissionQueryDto permissionQueryDto, CancellationToken cancellationToken)
        {
            var data = await _permissionRepository.GetAllAsync(permissionQueryDto, cancellationToken);

            return new ApiResponseDto<PermissionsResponseDto>
            {
                Success = true,
                Message = "Get all permission successfully",
                Data = data
            };
        }

        public async Task<ApiResponseDto<Permission?>> ReadPermissionByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<Permission?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var permission = await _permissionRepository.GetByIdAsync(id, cancellationToken);
            if (permission == null)
            {
                return new ApiResponseDto<Permission?>
                {
                    Success = false,
                    Message = "Permission not found"
                };
            }

            return new ApiResponseDto<Permission?>
            {
                Success = true,
                Message = "Get permission successfully",
                Data = permission
            };
        }

        public async Task<ApiResponseDto<Permission?>> UpdatePermissionAsync(string id, PermissionDto updatePermission, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<Permission?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var permission = await _permissionRepository.GetByIdAsync(id, cancellationToken);
            if (permission == null)
            {
                return new ApiResponseDto<Permission?>
                {
                    Success = false,
                    Message = "Permission not found"
                };
            }

            permission.UpdatePermission(updatePermission);
            await _permissionRepository.UpdateAsync(permission, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Permission?>
            {
                Success = true,
                Message = "Update permission successfully",
                Data = permission
            };
        }

        public async Task<ApiResponseDto<bool>> DeletePermissionAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var permission = await _permissionRepository.GetByIdAsync(id, cancellationToken);
            if (permission == null)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Permission not found"
                };
            }

            await _permissionRepository.DeleteAsync(permission, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<bool>
            {
                Success = true,
                Message = "Delete permission successfully",
                Data = true
            };
        }
    }
}
