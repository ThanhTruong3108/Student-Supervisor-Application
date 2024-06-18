using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models.Response.StudentSupervisorResponse
{
    public class StudentSupervisorResponse
    {
        public int StudentSupervisorId { get; set; }

        public string SupervisorCode { get; set; } = null!;

        public string? Description { get; set; }

        public string UserCode { get; set; }

        public string SupervisorName { get; set; }

        public string Phone { get; set; } 

        public string Password { get; set; } 

        public string? Address { get; set; }

        public byte RoleId { get; set; }

    }
}
