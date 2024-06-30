using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Response.DisciplineResponse
{
    public class DisciplineResponse
    {
        public int DisciplineId { get; set; }
        public int ViolationId { get; set; }
        public int PennaltyId { get; set; }
        public string? Description { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string Status { get; set; }
    }
}
