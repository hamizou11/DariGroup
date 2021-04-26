using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariGroupe.Models
{
    public class Favorites
    {
        public long id { get; set; }
        public String name { get; set; }
        public String type { get; set; }
        public IList<Announcement> announcements { get; set; }
    }
}