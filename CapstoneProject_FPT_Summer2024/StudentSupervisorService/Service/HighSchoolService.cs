using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.HighschoolResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service
{
    public interface HighSchoolService
    {
        Task<DataResponse<List<ResponseOfHighSchool>>> GetAllHighSchools();
        Task<DataResponse<ResponseOfHighSchool>> GetHighSchoolById(int id);
    }
}
