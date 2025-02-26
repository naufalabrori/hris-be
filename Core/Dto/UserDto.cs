
namespace HRIS.Core.Dto
{
    public record NewUserDto(string employeeId, string email, string username, string password);

    public record UserDto(string employeeId, string email, string username, string password, DateTime? lastLogin);

    public record UserQueryDto(string employeeId, string email, string username, DateTime lastLogin, string sortBy, bool? isDesc, int limit = 15, int offset = 0);

    public class UserResponseDto : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime? LastLogin { get; set; }
    }

    public record UsersResponseDto(List<UserResponseDto> Data, int TotalData);

    public record UserLoginDto(string email, string password);

    public record UserLoginResponseDto(string token, UserResponseDto user);
}
