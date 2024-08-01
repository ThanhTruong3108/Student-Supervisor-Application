using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.EvaluationRequest
{
    public class EvaluationCreateRequest
    {
        public int? ClassId { get; set; }

        public string? Description { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        //public int? Points { get; set; }
    }

    public class EvaluationUpdateRequest
    {
        public int EvaluationId { get; set; }
        public int? ClassId { get; set; }

        public string? Description { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public int? Points { get; set; }
    }
}
