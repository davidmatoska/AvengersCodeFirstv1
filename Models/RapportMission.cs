using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Avengers.Models
{
    public class RapportMission
    {
        

        public int RapportMissionID { get; set; }

        public int IncidentID { get; set; }

        public int HerosID { get; set; }

        public string CommentaireMission { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateRapport { get; set; }



    }
}
