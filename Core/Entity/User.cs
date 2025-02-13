using HRIS.Core.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Core.Entity
{
    public class User : BaseEntity
    {
        public User() { }

        public User(NewUserDto user)
        {
            EmployeeId = Guid.Parse(user.employeeId);
            Username = user.username;
            Password = user.password;
            Email = user.email;
        }

        public Guid Id { get; set; } = default!;
        public Guid EmployeeId { get; set; } = default;
        [StringLength(50)]
        public string Username { get; set; } = default!;
        [StringLength(255)]
        public string Password { get; set; } = default!;
        [StringLength(100)]
        public string Email { get; set; } = default!;
        public DateTime? LastLogin {  get; set; }

        public void UpdateUser(UserDto userDto)
        {
            EmployeeId = Guid.TryParse(userDto.employeeId, out var parsedGuid) ? parsedGuid : EmployeeId;
            Username = userDto.username ?? Username;
            Password = userDto.password ?? Password;
            Email = userDto.email ?? Email;
            LastLogin = userDto.lastLogin ?? LastLogin;
        }

    }
}
