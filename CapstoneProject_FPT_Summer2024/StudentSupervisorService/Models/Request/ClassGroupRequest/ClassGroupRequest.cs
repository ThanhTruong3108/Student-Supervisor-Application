using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.ClassGroupRequest
{
    public class ClassGroupCreateRequest
    {
        public string ClassGroupName { get; set; }
        public string Hall { get; set; }
        public string Status { get; set; }
    }

    public class ClassGroupUpdateRequest
    {
        public int ClassGroupID { get; set; }
        public string? ClassGroupName { get; set; }
        public string? Hall { get; set; }
        public string? Status { get; set; }
    }
}
