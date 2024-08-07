using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.DTO
{
    public class ClassRankResponse
    {
        public int? ClassId { get; set; }
        public string ClassName { get; set; }
        public int? TotalPoints { get; set; }
        public int Rank { get; set; }
    }
}
