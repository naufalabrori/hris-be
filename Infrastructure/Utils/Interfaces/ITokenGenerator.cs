namespace HRIS.Infrastructure.Utils.Interfaces
{
    public interface ITokenGenerator
    {
        public string GenerateToken(string userId, string email, string fullname);
    }
}
