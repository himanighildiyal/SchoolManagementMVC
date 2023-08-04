using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Models
{
    public class ClassDto
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
