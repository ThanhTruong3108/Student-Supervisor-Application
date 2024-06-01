using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.ViolationTypeRequest
{
    public class RequestOfVioType
    {
        public RequestOfVioType(int violationGroupId, string vioTypeName, string description)
        {
            ViolationGroupId = violationGroupId;
            VioTypeName = vioTypeName;
            Description = description;
        }
        [Required(ErrorMessage = "The ViolationGroupId field is required.")]
        public int ViolationGroupId { get; set; }
        [Required(ErrorMessage = "The VioTypeName field is required.")]
        public string VioTypeName { get; set; } = null!;
        [Required(ErrorMessage = "The Description field is required.")]
        public string? Description { get; set; }
    }
}
