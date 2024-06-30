using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentSupervisorService.Models.Response.StudentSupervisorResponse;
using StudentSupervisorService.Models.Request.StudentSupervisorRequest;

namespace StudentSupervisorService.Service
{
    public interface StudentSupervisorServices
    {
        Task<DataResponse<List<StudentSupervisorResponse>>> GetAllStudentSupervisors(string sortOrder);
        Task<DataResponse<StudentSupervisorResponse>> GetStudentSupervisorById(int id);
        Task<StudentSupervisorResponse> CreateAccountStudentSupervisor(StudentSupervisorRequest request);
        Task DeleteStudentSupervisor(int id);
        Task<DataResponse<StudentSupervisorResponse>> UpdateStudentSupervisor(int id, StudentSupervisorRequest request);
        Task<DataResponse<List<StudentSupervisorResponse>>> SearchStudentSupervisors(int? userId, int? studentInClassId, string sortOrder);
    }
}
