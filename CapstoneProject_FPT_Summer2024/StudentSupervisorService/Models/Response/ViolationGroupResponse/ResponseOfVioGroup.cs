using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Response.ViolationGroupResponse
{
    public class ResponseOfVioGroup
    {
        public int ViolationGroupId { get; set; }

        public string? Code { get; set; }

        public string VioGroupName { get; set; } = null!;

        public string? Description { get; set; }
    }
}
