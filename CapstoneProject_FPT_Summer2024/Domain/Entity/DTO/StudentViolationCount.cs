using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.DTO
{
    public class StudentViolationCount
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string ClassName { get; set; }
        public int ViolationCount { get; set; }
    }
}
