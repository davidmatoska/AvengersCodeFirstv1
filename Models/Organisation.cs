using Avengers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Avengers.Models
{
    public class Organisation
    {

        [ForeignKey("User")]

        public int OrganisationID { get; set; }



        [Required]
        public string Denomination { get; set; }

        [Required]
        public string Adresse { get; set; }

        [Required]
        public int PaysID { get; set; }




        [Required]
        [DataType(DataType.Date)]

        [Display(Name = "Date de création")]
        public DateTime Date_de_creation { get; set; }



       




        public virtual ApplicationUser User { get; set; }

        public virtual Pays Pays { get; set; }

    }
}