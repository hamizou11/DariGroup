using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariGroupe.Models
{
    public class Customer
    {
        public long id { get; set; }
        public String picture { get; set; }
        public String descriptionBlock { get; set; }
        private String Address { get; set; }
        public List<Announcement> announcements { get; set; }
    }
}