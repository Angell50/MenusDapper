using Dapper;
using Menus.UI.Data;
using Menus.UI.Data.Entities;
using Menus.UI.IServices;
using Menus.UI.Models;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace Menus.UI.Services
{
    public class MenuServices : IMenuService
    {
        private readonly IOptions<ConnectionStrings> _conexionBD;
        public MenuServices(IOptions<ConnectionStrings> conexionBD)
        {
            _conexionBD = conexionBD;
        }
        public async Task<string> Delete(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Pid_menu", id);
            using (IDbConnection oConex = new SqlConnection(_conexionBD.Value.Menu))
            {
                oConex.Open();
                var rpt = await oConex.ExecuteAsync("dbo.ELMININAR_MENU_01", parameters, commandType: CommandType.StoredProcedure);
                oConex.Close();
            }
            return "Deleted";
        }

        public async Task<List<MenusEnty>> GetAllMenus()
        {
           List<MenusEnty> lstMenus = new List<MenusEnty>();
            using (IDbConnection oConex = new SqlConnection(_conexionBD.Value.Menu))
            {
                oConex.Open();
                lstMenus = (List<MenusEnty>) await oConex.QueryAsync<MenusEnty>("dbo.GETALL_MENUS_01", commandType: CommandType.StoredProcedure);
                oConex.Close();
            }
            return lstMenus;
        }

		public List<VMMenus> GetAllVMMenus()
		{
			List<VMMenus> lstVMenus = new List<VMMenus>();
			using (IDbConnection oConex = new SqlConnection(_conexionBD.Value.Menu))
			{
				oConex.Open();
				lstVMenus = (List<VMMenus>) oConex.Query<VMMenus>("dbo.GETALL_MENUS_02", commandType: CommandType.StoredProcedure);
                oConex.Close();
			}
			return lstVMenus;
		}

		public void Save(MenusEnty oMenu)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Pmenu_alumno", oMenu.menu_alumno);
            parameters.Add("@Pmenu_fecha", oMenu.menu_fecha);
            parameters.Add("@Pmenu_ejer", oMenu.menu_ejer);
            parameters.Add("@Pmenu_coach", oMenu.menu_coach);

            using (IDbConnection oConex = new SqlConnection(_conexionBD.Value.Menu))
            {
                oConex.Open();
                var rpt = oConex.ExecuteAsync("dbo.INSERT_MENUS_01", parameters, commandType: CommandType.StoredProcedure);
                oConex.Close();
            }
        }

        public void Update(MenusEnty oMenu)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Pid_menu", oMenu.id_menu);
			parameters.Add("@Pmenu_alumno", oMenu.menu_alumno);
			parameters.Add("@Pmenu_fecha", oMenu.menu_fecha);
			parameters.Add("@Pmenu_ejer", oMenu.menu_ejer);
			parameters.Add("@Pmenu_coach", oMenu.menu_coach);

			using (IDbConnection oConex = new SqlConnection(_conexionBD.Value.Menu))
            {
                oConex.Open();
                var rpt = oConex.ExecuteAsync("dbo.UPDATE_MENU_01", parameters, commandType: CommandType.StoredProcedure);
                oConex.Close();
            }
        }
    }
}
