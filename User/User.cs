using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Backend.User
{
    public class User
    {
        [Key]
        [ForeignKey("User")]
        public int id { get; set; }

        [Required]
        [Column(TypeName = "varchar(60)")]
        public string name { get; set; }

        [Required]
        [Column(TypeName = "varchar(60)")]
        public string surname { get; set; }

        [Required]
        [Column(TypeName = "varchar(60)")]
        public string telephone { get; set; }

        [Required]
        [Column(TypeName = "varchar(60)")]
        public string mail { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string passwordHash { get; set; }
    }
}
