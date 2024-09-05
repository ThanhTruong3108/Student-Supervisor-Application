using StudentSupervisorService.Models.Response.SemesterResponse;
using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service
{
    public interface SemesterService
    {
        Task<DataResponse<List<ResponseOfSemester>>> GetSemestersBySchoolId(int schoolId);
    }
}
