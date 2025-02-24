
namespace HRIS.Core.Services
{
    public class MenuService : IMenuService
    {
        private readonly IHrisRepository _hrisRepository;
        private readonly IMenuRepository _menuRepository;

        public MenuService(IHrisRepository hrisRepository, IMenuRepository menuRepository)
        {
            _hrisRepository = hrisRepository;
            _menuRepository = menuRepository;
        }

        public async Task<ApiResponseDto<Menu?>> CreateMenuAsync(MenuDto menu, CancellationToken cancellationToken)
        {
            var existingMenu = await _menuRepository.GetByMenuNameAsync(menu.menuName.ToUpper(), cancellationToken);
            if (existingMenu != null)
            {
                return new ApiResponseDto<Menu?>
                {
                    Success = false,
                    Message = "Menu already exist"
                };
            }

            var newMenu = new Menu(menu);

            await _menuRepository.AddAsync(newMenu, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Menu?>
            {
                Success = true,
                Message = "Create menu successfully",
                Data = newMenu
            };
        }

        public async Task<ApiResponseDto<MenusResponseDto>> ReadMenusAsync(MenuQueryDto menuQueryDto, CancellationToken cancellationToken)
        {
            var data = await _menuRepository.GetAllAsync(menuQueryDto, cancellationToken);

            return new ApiResponseDto<MenusResponseDto>
            {
                Success = true,
                Message = "Get all menu successfully",
                Data = data
            };
        }

        public async Task<ApiResponseDto<Menu?>> ReadMenuByIdAsync(string id, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.GetByIdAsync(id, cancellationToken);
            if (menu == null)
            {
                return new ApiResponseDto<Menu?>
                {
                    Success = false,
                    Message = "Menu not found"
                };
            }

            return new ApiResponseDto<Menu?>
            {
                Success = true,
                Message = "Get menu successfully",
                Data = menu
            };
        }

        public async Task<ApiResponseDto<Menu?>> UpdateMenuAsync(string id, MenuDto updateMenu, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.GetByIdAsync(id, cancellationToken);
            if (menu == null)
            {
                return new ApiResponseDto<Menu?>
                {
                    Success = false,
                    Message = "Menu not found"
                };
            }

            menu.UpdateMenu(updateMenu);

            await _menuRepository.UpdateAsync(menu, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<Menu?>
            {
                Success = true,
                Message = "Update menu successfully",
                Data = menu
            };
        }

        public async Task<ApiResponseDto<bool>> DeleteMenuAsync(string id, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.GetByIdAsync(id, cancellationToken);
            if (menu == null)
            {
                return new ApiResponseDto<bool>
                {
                    Success = false,
                    Message = "Menu not found"
                };
            }

            await _menuRepository.DeleteAsync(menu, cancellationToken);
            await _hrisRepository.SaveChangesAsync(cancellationToken);

            return new ApiResponseDto<bool>
            {
                Success = true,
                Message = "Delete menu successfully",
                Data = true
            };
        }
    }
}
