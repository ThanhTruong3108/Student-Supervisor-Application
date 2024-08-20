using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.ClassGroupRequest
{
    public class ClassGroupCreateRequest
    {
        public int SchoolId { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }

    }

    public class ClassGroupUpdateRequest
    {
        public int ClassGroupID { get; set; }
        public int? SchoolId { get; set; }
        public int? TeacherId { get; set; }
        public string Name { get; set; }
    }
}
