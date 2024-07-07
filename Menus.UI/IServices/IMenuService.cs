using Menus.UI.Data.Entities;
using Menus.UI.Models;

namespace Menus.UI.IServices
{
    public interface IMenuService
    {
        Task<List<MenusEnty>> GetAllMenus();
		List<VMMenus> GetAllVMMenus();
		void Save(MenusEnty oMenu);
        void Update(MenusEnty oMenu);
        Task<string> Delete(int id);
    }
}
