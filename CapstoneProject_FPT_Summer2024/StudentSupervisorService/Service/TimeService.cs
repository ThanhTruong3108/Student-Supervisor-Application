using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.TimeResponse;
using StudentSupervisorService.Models.Request.TimeRequest;

namespace StudentSupervisorService.Service
{
    public interface TimeService
    {
        Task<DataResponse<List<ResponseOfTime>>> GetAllTimes(string sortOrder);
        Task<DataResponse<ResponseOfTime>> GetTimeById(int id);
        Task<DataResponse<ResponseOfTime>> CreateTime(RequestOfTime request);
        Task DeleteTime(int timeId);
        Task<DataResponse<ResponseOfTime>> UpdateTime(int id, RequestOfTime request);
        Task<DataResponse<List<ResponseOfTime>>> SearchTimes(byte? slot, TimeSpan? time, string sortOrder);
    }
}
