using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DariGroupe.Models
{
    public class Commande
    {
        [ForeignKey("Delevry")]
        public int id { get; set; }


        public float total { get; set; }

        public virtual Delevrys delevry { get; set; }
    }
}
