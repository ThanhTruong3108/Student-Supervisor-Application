using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Response.RegisteredSchoolResponse
{
    public class RegisteredSchoolResponse
    {
        public int RegisteredId { get; set; }
        public int SchoolId { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
    }
}
