using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariGroupe.Models
{
    public class Class1
    {
        public int id { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public string content { get; set; }
        public int likes { get; set; }
        public int dislikes { get; set; }

        public ArrayList databaseFile = new ArrayList();
        public ArrayList comments = new ArrayList();

      
    }
}
