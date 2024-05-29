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
        Task<DataResponse<List<ResponseOfHighSchool>>> GetAllHighSchools(int page, int pageSize, string sortOrder);
        Task<DataResponse<ResponseOfHighSchool>> GetHighSchoolById(int id);
        Task<DataResponse<List<ResponseOfHighSchool>>> SearchHighSchools(string? code, string? name, string? address, string? phone, string sortOrder);
    }
}
