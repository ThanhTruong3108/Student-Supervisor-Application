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
        public RequestOfTime(int classGroupId, byte slot, TimeSpan time1)
        {
            ClassGroupId = classGroupId;
            Slot = slot;
            Time1 = time1;
        }
        [Required(ErrorMessage = "The ClassGroupId field is required.")]
        public int ClassGroupId { get; set; }
        [Required(ErrorMessage = "The Slot field is required.")]
        public byte Slot { get; set; }
        [Required(ErrorMessage = "The Time field is required.")]
        public TimeSpan Time1 { get; set; }
    }
}
