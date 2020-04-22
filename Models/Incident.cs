using Avengers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Avengers.Models
{
    public class Incident
    {

        [Key]
        public int IncidentID { get; set; }

        public int UserId { get; set; }


        [Display(Name = "Motif")]
        public int Incident_MotifID { get; set; }

        [Required]
        public int PaysID { get; set; }

        public string Contexte { get; set; }

        public string Adresse { get; set; }


        [DataType(DataType.Date)]
        public DateTime Date_Incident { get; set; }


        public virtual Incident_Motif Incident_Motif { get; set; }

        public virtual Pays Pays { get; set; }


        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}