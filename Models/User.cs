using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DariGroupe.Models
{
    public class User
    {
        public int Id { get; set; }
        public string username { get; set; }
        [DataType(DataType.Date)]
        public DateTime birthDate { get; set; }


        public string firstName { get; set; }
        public string lastName { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }
        public Double idCard { get; set; }

        public Double number { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string email { get; set; }
        public string type { get; set; }

        public string address { get; set; }

        public string descriptionBlock { get; set; }

        public string picture { get; set; }
        public string area { get; set; }
        [Display(Name = "Salary")]
        public string name { get; set; }

    }
}