using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ViolationReportResponse;
using StudentSupervisorService.Models.Request.ViolationReportRequest;

namespace StudentSupervisorService.Service
{
    public interface ViolationReportService
    {
        Task<DataResponse<List<ResponseOfVioReport>>> GetAllVioReports();
        Task<DataResponse<ResponseOfVioReport>> GetVioReportById(int id);
        Task<DataResponse<ResponseOfVioReport>> CreateVioReport(RequestOfVioReport request);
        Task DeleteVioReport(int id);
        Task<DataResponse<ResponseOfVioReport>> UpdateVioReport(int id, RequestOfVioReport request);
        Task<DataResponse<List<ResponseOfVioReport>>> SearchVioReports(int? studentInClassId, int? violationId, string sortOrder);
    }
}
