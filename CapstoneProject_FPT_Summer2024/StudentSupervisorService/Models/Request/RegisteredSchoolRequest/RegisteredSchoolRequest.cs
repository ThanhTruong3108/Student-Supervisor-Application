using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Request.RegisteredSchoolRequest
{
    public class RegisteredSchoolCreateRequest
    {
        public int SchoolId { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string? Description { get; set; }
    }

    public class RegisteredSchoolUpdateRequest
    {
        public int RegisteredId { get; set; }
        public int? SchoolId { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
}
