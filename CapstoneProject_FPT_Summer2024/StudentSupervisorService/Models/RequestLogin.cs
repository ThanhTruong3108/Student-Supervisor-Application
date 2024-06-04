using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models
{
    public class RequestLogin
    {
        public string Phone { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
