using System.Collections.Generic;

namespace HRIS.Infrastructure.Utils.Interfaces
{
    public interface ITokenGenerator
    {
        public string GenerateToken(string userId, string username, string fullname, List<string> roles);
    }
}
