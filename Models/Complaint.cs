using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DariGroupe.Models
{
    public class Complaint
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string content { get; set; }
        public TypeRec type { get; set; }
        public string complaintStatus { get; set; }
        public string file { get; set; }
        public long customer_id { get; set; }
    }
}