using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ViolationGroupResponse;
using StudentSupervisorService.Models.Request.ViolationGroupRequest;

namespace StudentSupervisorService.Service
{
    public interface ViolationGroupService
    {
        Task<DataResponse<List<ResponseOfVioGroup>>> GetAllVioGroups(int page, int pageSize, string sortOrder);
        Task<DataResponse<ResponseOfVioGroup>> GetVioGroupById(int id);
        Task<DataResponse<ResponseOfVioGroup>> CreateVioGroup(RequestOfVioGroup request);
        Task DeleteVioGroup(int id);
        Task<DataResponse<ResponseOfVioGroup>> UpdateVioGroup(int id, RequestOfVioGroup request);
        Task<DataResponse<List<ResponseOfVioGroup>>> SearchVioGroups(string? code, string? name, string sortOrder);
    }
}
