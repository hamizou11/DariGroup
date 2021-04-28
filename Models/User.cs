using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariGroupe.Models
{
    public class User
    {
		public long id { get; set; } // Clé primaire
		public String firstName { get; set; }
		public String lastName { get; set; }
		public String password { get; set; }
		public Double idC { get; set; }
		public Double number { get; set; }
		public String email { get; set; }
	}
}
