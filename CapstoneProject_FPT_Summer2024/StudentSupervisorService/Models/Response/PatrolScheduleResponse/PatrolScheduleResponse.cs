using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Response.PatrolScheduleResponse
{
    public class PatrolScheduleResponse
    {
        public int ScheduleId { get; set; }
        public int ClassId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int SupervisorId { get; set; }
        public string SupervisorName { get; set; }
        public string? Name { get; set; }
        public int? Slot { get; set; }
        public TimeSpan? Time { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string? Status { get; set; }
    }
}
