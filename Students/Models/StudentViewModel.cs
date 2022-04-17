using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Models
{
    public class StudentViewModel
    {
        public string FullName { get; set; }
        public string MothersFullName { get; set; }
        public string FathersFullName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
