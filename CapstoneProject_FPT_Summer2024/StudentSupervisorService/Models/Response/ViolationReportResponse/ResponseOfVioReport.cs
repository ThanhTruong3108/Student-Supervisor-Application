using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Response.ViolationReportResponse
{
    public class ResponseOfVioReport
    {
        public int ViolationReportId { get; set; }
        public int StudentInClassId { get; set; }
        public DateTime EnrollDate { get; set; }
        public int ViolationId { get; set; }
        public string ViolationName { get; set; } = null!;
    }
}
