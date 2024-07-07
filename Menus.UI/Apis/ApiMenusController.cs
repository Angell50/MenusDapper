using Menus.UI.Data.Entities;
using Menus.UI.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Menus.UI.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiMenusController : ControllerBase
    {
        IMenuService _menuservice;

        public ApiMenusController(IMenuService menuservice)
        {
            _menuservice = menuservice;
        }

        // GET: api/<ApiMenusController>
        [HttpGet]
        public async Task<List<MenusEnty>> GetAllMenus()
        {
            var lstMenu = await _menuservice.GetAllMenus();
            return lstMenu;
        }

        // POST api/<ApiMenusController>
        [HttpPost]
        public void Post([FromForm] MenusEnty oMenu)
        {
            if(oMenu.id_menu == 0)
            {
                _menuservice.Save(oMenu);
            }
            else
            {
                _menuservice.Update(oMenu);
            }
        }

        // DELETE api/<ApiMenusController>/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            return await _menuservice.Delete(id);
        }
    }
}
