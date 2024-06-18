using StudentSupervisorService.Models.Request.StudentInClassRequest;
using StudentSupervisorService.Models.Response.StudentInClassResponse;
using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service
{
    public interface StudentInClassService
    {
        Task<DataResponse<List<StudentInClassResponse>>> GetAllStudentInClass(string sortOrder);
        Task<DataResponse<StudentInClassResponse>> GetStudentInClassById(int id);
        Task<DataResponse<List<StudentInClassResponse>>> SearchStudentInClass(
            int? classId, 
            int? studentId, 
            DateTime? enrollDate, 
            bool? isSupervisor, 
            string? status, 
            string sortOrder);
        Task<DataResponse<StudentInClassResponse>> CreateStudentInClass(StudentInClassCreateRequest request);
        Task<DataResponse<StudentInClassResponse>> UpdateStudentInClass(StudentInClassUpdateRequest request);
        Task<DataResponse<StudentInClassResponse>> DeleteStudentInClass(int id);
    }
}
