using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariGroupe.Models
{
    public class Rentingannonce
    {
        public int id { get; set; }
        public string title { get; set; }
        public string adresse { get; set; }
        public string video { get; set; }
        public double price { get; set; }
        public double innerSurface { get; set; }
        public double planeSurface { get; set; }
        public int roomNumber { get; set; }
        public double statePrice { get; set; }
        public string photoIdentity { get; set; }
        public string engagementLettre { get; set; }
        public bool favoriteAnnonce { get; set; }
        public DateTime dateDebut { get; set; }
        public DateTime dateFin { get; set; }
        public int nbrPersonne { get; set; }
        public bool rented { get; set; }
        public DateTime createdAt { get; set; }
        public string adState { get; set; }
    }
}
    
