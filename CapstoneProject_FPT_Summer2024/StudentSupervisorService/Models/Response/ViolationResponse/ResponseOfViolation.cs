using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Response.ViolationResponse
{
    public class ResponseOfViolation
    {
        public int ViolationId { get; set; }

        public int ClassId { get; set; }
        public string ClassName { get; set; } = null!;

        public int ViolationTypeId { get; set; }
        public string VioTypeName { get; set; } = null!;

        public int? TeacherId { get; set; }

        public string Code { get; set; } = null!;

        public string ViolationName { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime Date { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
