
namespace HRIS.Core.Interfaces.Services
{
    public interface IMenuService
    {
        public Task<ApiResponseDto<Menu?>> CreateMenuAsync(MenuDto menu, CancellationToken cancellationToken);
        public Task<ApiResponseDto<MenusResponseDto>> ReadMenusAsync(MenuQueryDto menuQueryDto, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Menu?>> ReadMenuByIdAsync(string id, CancellationToken cancellationToken);
        public Task<ApiResponseDto<Menu?>> UpdateMenuAsync(string id, MenuDto updateMenu, CancellationToken cancellationToken);
        public Task<ApiResponseDto<bool>> DeleteMenuAsync(string id, CancellationToken cancellationToken);
    }
}
