namespace HRIS.Infrastructure.Utils.Interfaces
{
    public interface ICurrentUserAccessor
    {
        string? GetCurrentUsername();
        string? GetCurrentFullname();
        string? GetCurrentUserId();
    }
}
