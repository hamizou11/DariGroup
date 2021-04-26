using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariGroupe.Models
{
    public class Announcement
    {

		public long id { get; set; }
		public String title { get; set; }
		public String description { get; set; }
		public Boolean available { get; set; }
		public String type { get; set; }
		public DateTime startDate { get; set; }
		public DateTime endDate { get; set; }
		public long duration { get; set; }
	}
}