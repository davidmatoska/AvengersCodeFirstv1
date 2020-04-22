using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Avengers.Models
{
    public class Incident_Motif
    {
        [Required]
        [Display(Name = "Motif")]
        public int Incident_MotifID { get; set; }
        [Required]
        public string Motif { get; set; }

        

        public ICollection<Incident> Incidents { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}