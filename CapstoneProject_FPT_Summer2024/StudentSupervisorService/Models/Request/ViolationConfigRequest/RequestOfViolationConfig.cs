using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentSupervisorService.Models.Request.ViolationConfigRequest
{
    public class RequestOfViolationConfig
    {
        public RequestOfViolationConfig(int evaluationId, int violationTypeId, string violationConfigName, string code, string description)
        {
            EvaluationId = evaluationId;
            ViolationTypeId = violationTypeId;
            ViolationConfigName = violationConfigName;
            Code = code;
            Description = description;

        }
        [Required(ErrorMessage = "The EvaluationId field is required.")]
        public int EvaluationId { get; set; }
        [Required(ErrorMessage = "The ViolationTypeId field is required.")]
        public int ViolationTypeId { get; set; }
        [Required(ErrorMessage = "The ViolationConfigName field is required.")]
        public string ViolationConfigName { get; set; } = null!;
        [Required(ErrorMessage = "The Code field is required.")]
        public string Code { get; set; } = null!;
        [Required(ErrorMessage = "The Description field is required.")]
        public string? Description { get; set; }
    }
}
