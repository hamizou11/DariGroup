using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DariGroupe.Models
{
    public enum TypeRec
    {
        [Display(Name = "AggressiveCustomer")]

        AggressiveCustomer, 
        TimesComplaint,
        Scam,
        Other
    }
}
