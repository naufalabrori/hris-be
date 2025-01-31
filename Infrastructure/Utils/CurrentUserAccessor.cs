using HRIS.Infrastructure.Utils.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HRIS.Infrastructure.Utils
{
    public class CurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public string? GetCurrentFullname()
        {
            return _httpContextAccessor?.HttpContext?.User?.FindFirst(x => x.Type == "fullname")?.Value;
        }

        public string? GetCurrentUserId()
        {
            return _httpContextAccessor?.HttpContext.User?.FindFirst(x => x.Type == "uid")?.Value;
        }
    }
}
