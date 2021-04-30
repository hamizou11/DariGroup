using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DariGroupe.Models
{
    public class Authentification
    {
        public string username { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
        public Boolean remmember_me { get; set; }

    }
}