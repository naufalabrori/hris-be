
namespace HRIS.Data.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly HrisContext _hrisContext;

        public MenuRepository(HrisContext hrisContext)
        {
            _hrisContext = hrisContext;
        }

        public async Task AddAsync(Menu menu, CancellationToken cancellationToken)
        {
            _hrisContext.Menus.Add(menu);
        }

        public async Task<Menu?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            Guid menuId = Guid.Parse(id);
            var menu = await _hrisContext.Menus.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.Id == menuId, cancellationToken);
            return menu;
        }

        public async Task<Menu?> GetByMenuNameAsync(string menuName, CancellationToken cancellationToken)
        {
            var menu = await _hrisContext.Menus.IsActiveRows().AsNoTracking().FirstOrDefaultAsync(x => x.MenuName == menuName, cancellationToken);
            return menu;
        }

        public async Task<MenusResponseDto> GetAllAsync(MenuQueryDto menuQueryDto, CancellationToken cancellationToken)
        {
            var query = _hrisContext.Menus.IsActiveRows().AsNoTracking().Select(x => x);

            if (!string.IsNullOrWhiteSpace(menuQueryDto.menuName))
            {
                query = query.Where(x => x.MenuName.Contains(menuQueryDto.menuName.ToUpper()));
            }
            if (!string.IsNullOrWhiteSpace(menuQueryDto?.sortBy) && menuQueryDto.isDesc.HasValue)
            {
                query = query.OrderBy($"{menuQueryDto.sortBy} {(menuQueryDto.isDesc.Value ? "DESC" : "ASC")}");
            }

            var totalData = await query.CountAsync(cancellationToken);
            var pageQuery = query
                .Skip(menuQueryDto.offset)
                .Take(menuQueryDto.limit)
                .AsNoTracking();
            var page = await pageQuery.ToListAsync(cancellationToken);

            return new MenusResponseDto(page, totalData);
        }

        public async Task UpdateAsync(Menu menu, CancellationToken cancellationToken)
        {
            _hrisContext.Menus.Update(menu);
        }

        public async Task DeleteAsync(Menu menu, CancellationToken cancellationToken)
        {
            _hrisContext.Menus.Remove(menu);
        }
    }
}
