using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Taskk
{
    public class TaskPom
    {
        public int id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string note { get; set; }
        public string priority { get; set; }
        public string status { get; set; }
        public int UserId { get; set; }
    }
}
