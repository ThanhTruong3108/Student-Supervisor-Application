using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Response.TimeResponse
{
    public class ResponseOfTime
    {
        public int TimeId { get; set; }
        public int ClassGroupId { get; set; }
        public string ClassGroupName { get; set; } = null!;
        public string Hall { get; set; } = null!;
        public byte Slot { get; set; }
        public TimeSpan Time1 { get; set; }
    }
}
