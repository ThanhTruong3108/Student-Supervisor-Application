using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ViolationResponse;
using StudentSupervisorService.Models.Request.ViolationRequest;

namespace StudentSupervisorService.Service
{
    public interface ViolationService
    {
        Task<DataResponse<List<ResponseOfViolation>>> GetAllViolations(int page, int pageSize, string sortOrder);
        Task<DataResponse<ResponseOfViolation>> GetViolationById(int id);
        Task<DataResponse<ResponseOfViolation>> CreateViolation(RequestOfCreateViolation request);
        Task DeleteViolation(int id);
        Task<DataResponse<ResponseOfViolation>> UpdateViolation(int id, RequestOfUpdateViolation request);
        Task<DataResponse<List<ResponseOfViolation>>> SearchViolations(int? classId, int? teacherId, int? vioTypeId, string? code, string? name, DateTime? date, string sortOrder);
    }
}
