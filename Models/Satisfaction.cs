using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Avengers.Models
{
    public enum Niveau_Satisfaction { Tout_pourri = 0, Passable = 1, Presque_Correct = 2, Moyen = 3, Super = 4, Parfait = 5 }
    public class Satisfaction
    {
        [Key]
        [ForeignKey("Mission")]
        public int SatisfactionID { get; set; }

        //public int MissionID { get; set; }

        public Niveau_Satisfaction Niveau_Satisfaction { get; set; }

        public string Commentaires { get; set; }

        public string Identification_Heros_Mechant { get; set; }

        public Boolean Depot_Plainte { get; set; }

        public string Motif_Plainte { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date_Satisfaction { get; set; }


        public virtual Mission Mission { get; set; }


    }
}