using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Avengers.Models
{
    public class Litige
    {

        [Key]
        public int LitigeID { get; set; }

        public int UserId { get; set; }

        public string RaisonLitige { get; set; }

      
        [DataType(DataType.Date)]
        public DateTime DateLitige { get; set; }


       

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<File> Files { get; set; }



    }
}