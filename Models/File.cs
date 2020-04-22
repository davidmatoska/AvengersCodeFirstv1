using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Avengers.Models
{
    public class File
    {
        public int FileId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }

        

      
        public virtual Incident_Motif Incident_Motif { get; set; }

        public virtual Incident Incident { get; set; }
        public virtual Pays Pays { get; set; }
    }
}