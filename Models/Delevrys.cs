using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariGroupe.Models
{
    public class Delevrys
    {
        public long id { get; set; }
        public DateTime delivery_date { get; set; }
        public string cout { get; set; }
        public string etat { get; set; }
        public int? commande_id { get; set; }
        public int? livreur_id { get; set; }

    }
}