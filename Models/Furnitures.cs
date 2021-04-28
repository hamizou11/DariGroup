using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariGroupe.Models
{
    public class Furnitures
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string state { get; set; }
        public string address { get; set; }
        public double price { get; set; }
        public string picture { get; set; }
    }
}