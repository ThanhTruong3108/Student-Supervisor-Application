using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.TimeRequest
{
    public class RequestOfTime
    {
        [Required(ErrorMessage = "The ClassGroupId field is required.")]
        public int ClassGroupId { get; set; }
        [Required(ErrorMessage = "The Slot field is required.")]
        public byte Slot { get; set; }
        [Required(ErrorMessage = "The Time field is required.")]
        public string Time1 { get; set; }
    }
}
