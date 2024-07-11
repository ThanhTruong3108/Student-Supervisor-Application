using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ViolationResponse;
using StudentSupervisorService.Models.Request.ViolationRequest;

namespace StudentSupervisorService.Service
{
    public interface ViolationService
    {
        Task<DataResponse<List<ResponseOfViolation>>> GetAllViolations(string sortOrder);
        Task<DataResponse<ResponseOfViolation>> GetViolationById(int id);
        Task<DataResponse<ResponseOfViolation>> CreateViolationForStudentSupervisor(RequestOfCreateViolation request);
        Task<DataResponse<ResponseOfViolation>> CreateViolationForSupervisor(RequestOfCreateViolation request);
        Task<DataResponse<ResponseOfViolation>> ApproveViolation(int id);
        Task<DataResponse<ResponseOfViolation>> RejectViolation(int id);
        Task DeleteViolation(int id);
        Task<DataResponse<ResponseOfViolation>> UpdateViolation(int id, RequestOfUpdateViolation request);
        Task<DataResponse<List<ResponseOfViolation>>> SearchViolations(
                int? classId,
                int? violationTypeId,
                int? studentInClassId,
                int? teacherId,
                string? name,
                string? description,
                DateTime? date,
                string? status,
                string sortOrder);

        Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByStudentCode(string studentCode);
        Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByStudentCodeAndYear(string studentCode, short year);
        Task<DataResponse<Dictionary<int, int>>> GetViolationCountByYear(string studentCode);
        Task<DataResponse<List<ResponseOfViolation>>> GetApprovedViolations();
        Task<DataResponse<List<ResponseOfViolation>>> GetPendingViolations();
        Task<DataResponse<List<ResponseOfViolation>>> GetRejectedViolations();
        Task<DataResponse<List<ResponseOfViolation>>> GetInactiveViolations();
    }
}
