using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Status
{
    public class Status
    {
        [Key]
        [ForeignKey("Status")]
        public int id { get; set; }

        [Required]
        [Column(TypeName = "varchar(60)")]
        public string name { get; set; }
    }
}
