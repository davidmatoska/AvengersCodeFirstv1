using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Avengers.Models
{
   

        public enum Statut_Mission { En_Attente = 0, Mission_activée = 1, Mission_refusée = 2 }
        public class Mission
        {
            [Key]
            public int MissionID { get; set; }

            public int IncidentID { get; set; }

            public Statut_Mission Statut_Mission { get; set; }

            public int HerosID { get; set; }

            public int? MechantID { get; set; }

            public string Commentaire { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date_Mission { get; set; }


            public virtual Incident Incident { get; set; }

            public virtual Heros Heros { get; set; }

            public virtual Mechant Mechant { get; set; }

            public virtual Satisfaction Satisfaction { get; set; }








        }
    
}