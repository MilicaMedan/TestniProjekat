using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Taskk
{
    public class Taskk
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Column(TypeName = "varchar(60)")]
        public string title { get; set; }

        [Required]
        [Column(TypeName = "datetime2(7)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        
        public DateTime startDate { get; set; }
        
        [Column(TypeName = "datetime2(7)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      
        public DateTime endDate { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(250)")]
        
        public string note { get; set; }


        public int UserId { get; set; }
        public virtual User.User User { get; set; }

        public int PriorityId { get; set; }
        public virtual Priority.Priority Priority { get; set; }

        public int StatusId { get; set; }
        public virtual Status.Status Status { get; set; }
    }
}
