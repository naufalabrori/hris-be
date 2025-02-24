
namespace HRIS.Core.Interfaces.Repositories
{
    public interface IMenuRepository
    {
        public Task AddAsync(Menu menu, CancellationToken cancellationToken);
        public Task<Menu?> GetByIdAsync(string id, CancellationToken cancellationToken);
        public Task<Menu?> GetByMenuNameAsync(string menuName, CancellationToken cancellationToken);
        public Task<MenusResponseDto> GetAllAsync(MenuQueryDto menuQueryDto, CancellationToken cancellationToken);
        public Task UpdateAsync(Menu menu, CancellationToken cancellationToken);
        public Task DeleteAsync(Menu menu, CancellationToken cancellationToken);
    }
}
