using System;

namespace HRIS.Core.Dto
{
    public record UserDto(string id, string employeeId, string email, string username, string password, DateTime? lastLogin);
}
