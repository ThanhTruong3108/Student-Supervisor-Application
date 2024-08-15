using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.EvaluationRequest
{
    public class EvaluationCreateRequest
    {
        [Required(ErrorMessage = "The ClassId field is required.")]
        public int? ClassId { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "The From field is required.")]
        public DateTime From { get; set; }
        [Required(ErrorMessage = "The To field is required.")]
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
