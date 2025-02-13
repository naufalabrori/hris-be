using HRIS.Core.Interfaces.Repositories;
using HRIS.Core.Interfaces.Services;
using HRIS.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IHrisRepository _hrisRepository;
        private readonly IRoleRepository _roleRepository;

        public RoleService(IHrisRepository hrisRepository, IRoleRepository roleRepository)
        {
            _hrisRepository = hrisRepository;
            _roleRepository = roleRepository;
        }

        public async Task<ApiResponseDto<Role?>> CreateRoleAsync(RoleDto role, CancellationToken cancellationToken)
        {
            var existingRole = await _roleRepository.GetByRolenameAsync(role.roleName, cancellationToken);
            if (existingRole != null)
            {
                return new ApiResponseDto<Role?>
                {
                    Success = false,
                    Message = "Rolename already exist"
                };
            }

            var newRole = new Role(role);

            await _roleRepository.AddAsync(newRole, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Role?>
            {
                Success = true,
                Message = "Role create successfully",
                Data = newRole
            };
        }

        public async Task<ApiResponseDto<RolesResponseDto>> ReadRolesAsync(RoleQueryDto roleQueryDto, CancellationToken cancellationToken)
        {
            var data = await _roleRepository.GetAllAsync(roleQueryDto, cancellationToken);

            return new ApiResponseDto<RolesResponseDto>
            {
                Success = true,
                Message = "Get all role successfully",
                Data = data
            };
        }

        public async Task<ApiResponseDto<Role?>> ReadRoleByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<Role?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var role = await _roleRepository.GetByIdAsync(id, cancellationToken);
            if (role == null)
            {
                return new ApiResponseDto<Role?>
                {
                    Success = false,
                    Message = "Role not found"
                };
            }

            return new ApiResponseDto<Role?>
            {
                Success = true,
                Message = "Get role successfully",
                Data = role
            };
        }

        public async Task<ApiResponseDto<Role?>> UpdateRoleAsync(string id, RoleDto updateRole, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<Role?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var role = await _roleRepository.GetByIdAsync(id, cancellationToken);
            if (role == null)
            {
                return new ApiResponseDto<Role?>
                {
                    Success = false,
                    Message = "Role not found"
                };
            }

            role.UpdateRole(updateRole);

            await _roleRepository.UpdateAsync(role, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Role?>
            {
                Success = true,
                Message = "Update role successfully",
                Data = role
            };
        }

        public async Task<ApiResponseDto<bool>> DeleteRoleAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var role = await _roleRepository.GetByIdAsync(id, cancellationToken);
            if (role == null)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Role not found"
                };
            }

            await _roleRepository.DeleteAsync(role, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<bool>
            {
                Success = true,
                Message = "Delete role successfully"
            };
        }
    }
}
