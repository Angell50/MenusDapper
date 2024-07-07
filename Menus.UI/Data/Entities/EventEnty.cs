using System.ComponentModel.DataAnnotations;

namespace Menus.UI.Data.Entities
{
	public class EventEnty
	{
		public int id { get; set; }
		public int id_menu { get; set; }
		public string title { get; set; }
		public string Description { get; set; }
        public DateTime StarDate { get; set; }        
        public DateTime EndDate { get; set; }
		public string Location { get; set; }
	}
}
