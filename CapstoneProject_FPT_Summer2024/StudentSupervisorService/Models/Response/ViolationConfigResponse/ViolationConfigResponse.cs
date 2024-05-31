using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Response.ViolationConfigResponse
{
    public class ViolationConfigResponse
    {
        public int ViolationConfigId { get; set; }

        public int EvaluationId { get; set; }

        public int ViolationTypeId { get; set; }

        public string ViolationTypeName { get; set; } = null!;

        public string ViolationConfigName { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }
    }
}
