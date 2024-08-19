using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Response.ClassGroupResponse
{
    public class ClassGroupResponse
    {
        public int ClassGroupId { get; set; }
        public int? SchoolId { get; set; }
        public int? TeacherId { get; set; }
        public string Name { get; set; }
        public int Grade { get; set; }
        public string? Status { get; set; }
    }
}
