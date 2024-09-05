using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Response.SemesterResponse
{
    public class ResponseOfSemester
    {
        public int SemesterId { get; set; }

        public int SchoolYearId { get; set; }
        public short Year { get; set; }

        public string? Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
