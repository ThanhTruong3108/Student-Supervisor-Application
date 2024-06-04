using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService
{
    public class AppConfiguration
    {
        public string DatabaseConnection { get; set; }
        public string JWTSecretKey { get; set; }
    }
}
