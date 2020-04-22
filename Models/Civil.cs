using Avengers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Avengers.Models

{
  

        public enum Civilite { M, Mme }
        public class Civil


        {
            [ForeignKey("User")]

            public int CivilID { get; set; }




            [Required]
            public string Prenom { get; set; }
            [Required]
            public string Nom { get; set; }
            [Required]
            public Civilite Civilite { get; set; }

        [Required]
        public string Adresse { get; set; }

        [Required]
        public int PaysID { get; set; }


        [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Date de naissance")]
            public DateTime Date_de_naissance { get; set; }

      

            /*public string NomComplet
            {
                get
                {
                    return String.Format("{0} {1}, {2}",Civilite, Prenom, Nom);
                }
            }*/

            public string NomComplet
            {
                get
                {
                    return Prenom + ", " + Nom + ",né(e) le " + Date_de_naissance;
                }
            }





            public virtual Heros Heros { get; set; }

            public virtual Mechant Mechant { get; set; }

            public virtual ApplicationUser User { get; set; }

            public virtual Pays Pays { get; set; }




    }


    
}