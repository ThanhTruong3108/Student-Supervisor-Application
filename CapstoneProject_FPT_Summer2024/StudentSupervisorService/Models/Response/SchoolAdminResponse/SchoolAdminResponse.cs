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
        public int? SchoolId { get; set; }
        public int? UserId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; }
        public byte RoleId { get; set; }
        public string Status { get; set; }
    }
}
