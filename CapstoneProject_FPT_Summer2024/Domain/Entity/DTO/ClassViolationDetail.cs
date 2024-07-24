using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.DTO
{
    public class ClassViolationDetail
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int StudentCount { get; set; }
        public List<StudentDetail> Students { get; set; }
    }
}
