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
        Task<DataResponse<ResponseOfViolation>> UpdateViolationForStudentSupervisor(int id, RequestOfUpdateViolationForStudentSupervisor request);
        Task<DataResponse<ResponseOfViolation>> UpdateViolationForSupervisor(int id, RequestOfUpdateViolationForSupervisor request);
        Task<DataResponse<List<ResponseOfViolation>>> SearchViolations(
                int? classId,
                int? violationTypeId,
                int? studentInClassId,
                int? userId,
                string? name,
                string? description,
                DateTime? date,
                string? status,
                string sortOrder);

        Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByStudentCode(string studentCode);
        Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByStudentCodeAndYear(string studentCode, short year);
        Task<DataResponse<Dictionary<int, int>>> GetViolationCountByYear(string studentCode);
        Task<DataResponse<List<ResponseOfViolation>>> GetViolationsBySchoolId(int schoolId);
    }
}
