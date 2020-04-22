using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Avengers.Models
{
    public class Heros
    {

        [Key]
        [ForeignKey("Civil")]

        public int HerosID { get; set; }

        public string Pseudonyme { get; set; }

        public string Telephone_Secret { get; set; }

        [Display(Name = "Photo")]
        public string Image_Heros { get; set; }

        public Boolean Disponible { get; set; }



        public virtual Civil Civil { get; set; }

        public ICollection<Mission> Missions { get; set; }
    }
}