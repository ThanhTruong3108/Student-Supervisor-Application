using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentSupervisorService.Models.Response.TeacherResponse;
using StudentSupervisorService.Models.Request.TeacherRequest;

namespace StudentSupervisorService.Service
{
    public interface TeacherService
    {
        Task<DataResponse<List<TeacherResponse>>> GetAllTeachers(string sortOrder);
        Task<DataResponse<TeacherResponse>> GetTeacherById(int id);
        Task<DataResponse<TeacherResponse>> CreateTeacher(RequestOfTeacher request);
        Task DeleteTeacher(int id);
        Task<DataResponse<TeacherResponse>> UpdateTeacher(int id, RequestOfTeacher request);
        Task<DataResponse<List<TeacherResponse>>> SearchTeachers(int? schoolId, int? userId, bool sex, string sortOrder);
    }
}
