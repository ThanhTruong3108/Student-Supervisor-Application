using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ViolationResponse;
using StudentSupervisorService.Models.Request.ViolationRequest;
using Domain.Entity.DTO;

namespace StudentSupervisorService.Service
{
    public interface ViolationService
    {
        Task<DataResponse<List<ResponseOfViolation>>> GetAllViolations(string sortOrder);
        Task<DataResponse<ResponseOfViolation>> GetViolationById(int id);
        Task<DataResponse<ResponseOfViolation>> CreateViolationForStudentSupervisor(int userId, RequestOfStuSupervisorCreateViolation request);
        Task<DataResponse<ResponseOfViolation>> CreateViolationForSupervisor(int userId, RequestOfSupervisorCreateViolation request);
        Task<DataResponse<ResponseOfViolation>> ApproveViolation(int id);
        Task<DataResponse<ResponseOfViolation>> RejectViolation(int id);
        Task<DataResponse<ResponseOfViolation>> DeleteViolation(int id);
        Task<DataResponse<ResponseOfViolation>> CompleteViolation(int violationId);
        Task<DataResponse<ResponseOfViolation>> UpdateViolationForStudentSupervisor(int id, RequestOfUpdateViolationForStudentSupervisor request);
        Task<DataResponse<ResponseOfViolation>> UpdateViolationForSupervisor(int id, RequestOfUpdateViolationForSupervisor request);
        Task<DataResponse<List<ResponseOfViolation>>> GetViolationsBySchoolId(int schoolId);
        Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByUserId(int userId, string sortOrder);
        Task<DataResponse<ResponseOfViolation>> GetViolationByDisciplineId(int disciplineId);
        Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByUserRoleStudentSupervisor(int userId, string sortOrder);
        Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByUserRoleSupervisor(int userId, string sortOrder);
        Task<DataResponse<List<ResponseOfViolation>>> GetViolationsBySupervisorUserId(int userId);
    }
}
