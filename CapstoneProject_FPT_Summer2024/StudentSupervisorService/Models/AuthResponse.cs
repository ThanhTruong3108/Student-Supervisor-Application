using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Models
{
    public class AuthResponse<T> : DataResponse<T>
    {
        public string? Token { get; set; }
    }
}
