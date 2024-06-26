using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Response.SchoolAdminResponse
{
    public class SchoolAdminResponse
    {
        public int SchoolAdminId { get; set; }
        public int? AdminId { get; set; }
        public string? AdminName { get; set; }
        public int? SchoolId { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolName { get; set; }
        public string Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}
