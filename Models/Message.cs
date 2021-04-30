using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariGroupe.Models
{
    public class Message
    {
        public int id { get; set; }
        public bool seen { get; set; }
        public DateTime receivedDate { get; set; }
       
        public string contents { get; set; }
        
    }
}