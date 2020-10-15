using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Priority
{
    public class Priority
    {
        [Key]
        [ForeignKey("Priority")]
        public int id { get; set; }

        [Required]
        [Column(TypeName = "varchar(60)")]
        public string name { get; set; }
    }
}
