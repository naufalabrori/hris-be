﻿
namespace HRIS.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IHrisRepository _hrisRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserService(IHrisRepository hrisRepository, IUserRepository userRepository, ITokenGenerator tokenGenerator, 
            IEmployeeRepository employeeRepository, IUserRoleRepository userRoleRepository)
        {
            _hrisRepository = hrisRepository;
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _employeeRepository = employeeRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<ApiResponseDto<UserResponseDto?>> CreateUserAsync(NewUserDto user, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(user.employeeId))
            {
                return new ApiResponseDto<UserResponseDto?>
                {
                    Success = false,
                    Message = "Invalid EmployeeId Guid format"
                };
            }

            var empId = await _userRepository.GetByEmployeeId(user.employeeId, cancellationToken);
            if (empId != null)
            {
                return new ApiResponseDto<UserResponseDto?>
                {
                    Success = false,
                    Message = "User Employee already exist"
                };
            }

            var email = await _userRepository.GetByEmailAsync(user.email, cancellationToken);
            if (email != null)
            {
                return new ApiResponseDto<UserResponseDto?>
                {
                    Success = false,
                    Message = "Email already exist"
                };
            }

            var username = await _userRepository.GetByUsernameAsync(user.username, cancellationToken);
            if (username != null)
            {
                return new ApiResponseDto<UserResponseDto?>
                {
                    Success = false,
                    Message = "Username already exist"
                };
            }

            var newUser = new User(user);
            newUser.Password = PasswordHasher.HashPassword(user.password);

            await _userRepository.AddAsync(newUser, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<UserResponseDto?>
            {
                Success = true,
                Message = "User create successfully",
                Data = newUser.Adapt<UserResponseDto>()
            };
        }

        public async Task<ApiResponseDto<UsersResponseDto>> ReadUsersAsync(UserQueryDto userQueryDto, CancellationToken cancellationToken)
        {
            var response = await _userRepository.GetAllAsync(userQueryDto, cancellationToken);

            return new ApiResponseDto<UsersResponseDto>
            {
                Success = true,
                Message = "Get all user successfully",
                Data = response
            };
        }

        public async Task<ApiResponseDto<UserResponseDto?>> ReadUserByIdAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<UserResponseDto?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
            {
                return new ApiResponseDto<UserResponseDto?>
                {
                    Success = false,
                    Message = "User not found",
                };
            }

            return new ApiResponseDto<UserResponseDto?>
            {
                Success = true,
                Message = "Get user by id successfully",
                Data = user.Adapt<UserResponseDto>()
            };
        }

        public async Task<ApiResponseDto<UserResponseDto?>> UpdateUserAsync(string id, UserDto updateUser, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<UserResponseDto?>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
            {
                return new ApiResponseDto<UserResponseDto?>
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            if (!StringExtensions.IsValidGuid(updateUser.employeeId))
            {
                return new ApiResponseDto<UserResponseDto?>
                {
                    Success = false,
                    Message = "Invalid EmployeeId Guid format"
                };
            }

            var employee = await _userRepository.GetByEmployeeId(updateUser.employeeId, cancellationToken);
            if (employee == null)
            {
                return new ApiResponseDto<UserResponseDto?>
                {
                    Success = false,
                    Message = "Employee not found"
                };
            }

            employee.UpdateUser(updateUser);

            if (!string.IsNullOrWhiteSpace(updateUser.password))
            {
                employee.Password = PasswordHasher.HashPassword(updateUser.password);
            }

            await _userRepository.UpdateAsync(employee, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<UserResponseDto?>
            {
                Success = true,
                Message = "Update user successfully",
                Data = employee.Adapt<UserResponseDto>()
            };
        }

        public async Task<ApiResponseDto<bool>> DeleteUserAsync(string id, CancellationToken cancellationToken)
        {
            if (!StringExtensions.IsValidGuid(id))
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Invalid Guid format"
                };
            }

            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "User not found",
                };
            }

            await _userRepository.DeleteAsync(user, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<bool>
            {
                Success = true,
                Message = "Delete user successfully"
            };
        }

        public async Task<ApiResponseDto<UserLoginResponseDto?>> LoginUserAsync(UserLoginDto userLogin, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(userLogin.email, cancellationToken);
            if (user == null)
            {
                return new ApiResponseDto<UserLoginResponseDto?>
                {
                    Success = false,
                    Message = "User not found",
                };
            }

            var isCorrectPassword = PasswordHasher.VerifyPassword(userLogin.password, user.Password);
            if (!isCorrectPassword)
            {
                return new ApiResponseDto<UserLoginResponseDto?>
                {
                    Success = false,
                    Message = "Incorrect password"
                };
            }

            var employee = await _employeeRepository.GetByIdAsync(user.EmployeeId.ToString(), cancellationToken);
            if (employee == null)
            {
                return new ApiResponseDto<UserLoginResponseDto?>
                {
                    Success = false,
                    Message = "Employee not found"
                };
            }

            string fullname = employee.FirstName + " " + employee.LastName ?? string.Empty;

            user.LastLogin = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            var userRoles = await _userRoleRepository.GetByUserIdAsync(user.Id.ToString(), cancellationToken);
            var roles = new List<string>();

            foreach (var role in userRoles)
            {
                roles.Add(role.RoleName);
            }

            var token = _tokenGenerator.GenerateToken(user.Id.ToString(), user.Username, fullname, roles);

            return new ApiResponseDto<UserLoginResponseDto?>
            {
                Success = true,
                Message = "User login successfully",
                Data = new UserLoginResponseDto(token, user.Adapt<UserResponseDto>())
            };
        }
    }
}
