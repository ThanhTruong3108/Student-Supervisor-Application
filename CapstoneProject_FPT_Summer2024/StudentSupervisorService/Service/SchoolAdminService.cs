using Domain.Entity;
using StudentSupervisorService.Models.Request.SchoolAdminRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.SchoolAdminResponse;


namespace StudentSupervisorService.Service
{
    public interface SchoolAdminService 
    {
        Task<DataResponse<List<SchoolAdminResponse>>> GetAllSchoolAdmins(string sortOrder);
        Task<DataResponse<SchoolAdminResponse>> GetBySchoolAdminId(int id);
        Task<DataResponse<List<SchoolAdminResponse>>> SearchSchoolAdmins(int? schoolId, int? adminId, string sortOrder);
        //Task<DataResponse<SchoolAdminResponse>> CreateSchoolAdmin(SchoolAdminRequest request);
        //Task<DataResponse<SchoolAdminResponse>> UpdateSchoolAdmin(SchoolAdminRequest request);
        //Task<DataResponse<SchoolAdminResponse>> DeleteSchoolAdmin(int id);
    }
}
