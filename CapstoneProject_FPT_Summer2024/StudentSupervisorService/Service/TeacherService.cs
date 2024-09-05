using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentSupervisorService.Models.Response.TeacherResponse;
using StudentSupervisorService.Models.Request.TeacherRequest;
using Microsoft.AspNetCore.Http;

namespace StudentSupervisorService.Service
{
    public interface TeacherService
    {
        Task<DataResponse<List<TeacherResponse>>> GetAllTeachers(string sortOrder);
        Task<DataResponse<TeacherResponse>> GetTeacherById(int id);
        Task<DataResponse<TeacherResponse>> CreateAccountTeacher(RequestOfTeacher request);
        Task<DataResponse<TeacherResponse>> CreateAccountSupervisor(RequestOfTeacher request);
        Task<DataResponse<TeacherResponse>> DeleteTeacher(int id);
        Task<DataResponse<TeacherResponse>> UpdateTeacher(int id, RequestOfTeacher request);
        Task<DataResponse<List<TeacherResponse>>> GetTeachersBySchoolId(int schoolId);
        Task<DataResponse<List<TeacherResponse>>> GetAllTeachersWithRoleTeacher(int schoolId);
        Task<DataResponse<List<TeacherResponse>>> GetAllTeachersWithRoleSupervisor(int schoolId);
        Task<DataResponse<List<TeacherResponse>>> GetTeachersWithoutClass(int schoolId, short year);
        Task<DataResponse<string>> ImportTeachersFromExcel(int userId, IFormFile file);
    }
}
