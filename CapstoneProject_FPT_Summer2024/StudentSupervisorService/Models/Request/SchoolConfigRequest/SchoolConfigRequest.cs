using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.SchoolConfigRequest
{
    public class SchoolConfigCreateRequest
    {
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
    }

    public class SchoolConfigUpdateRequest
    {
        public int ConfigId { get; set; }
        public int? SchoolId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
    }
}
