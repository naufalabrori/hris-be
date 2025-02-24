
using System.ComponentModel.DataAnnotations;

namespace HRIS.Core.Entity
{
    public class Menu : BaseEntity
    {
        public Menu() { }

        public Menu(MenuDto menu)
        {
            MenuName = menu.menuName.ToUpper();
        }

        public Guid Id { get; set; } = default!;
        [StringLength(50)]
        public string MenuName { get; set; } = default!;

        public void UpdateMenu(MenuDto menu)
        {
            MenuName = menu.menuName.ToUpper() ?? MenuName;
        }
    }
}
