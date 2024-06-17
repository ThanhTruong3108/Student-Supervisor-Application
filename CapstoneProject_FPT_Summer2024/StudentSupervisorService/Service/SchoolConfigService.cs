using StudentSupervisorService.Models.Request.ClassGroupRequest;
using StudentSupervisorService.Models.Response.ClassGroupResponse;
using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentSupervisorService.Models.Response.SchoolConfigResponse;
using StudentSupervisorService.Models.Request.SchoolConfigRequest;

namespace StudentSupervisorService.Service
{
    public interface SchoolConfigService
    {
        Task<DataResponse<List<SchoolConfigResponse>>> GetAllSchoolConfigs(string sortOrder);
        Task<DataResponse<SchoolConfigResponse>> GetSchoolConfigById(int id);
        Task<DataResponse<List<SchoolConfigResponse>>> SearchSchoolConfigs(int? schoolId, string? name, string? code, string? description, string? status, string sortOrder);
        Task<DataResponse<SchoolConfigResponse>> CreateSchoolConfig(SchoolConfigCreateRequest request);
        Task<DataResponse<SchoolConfigResponse>> UpdateSchoolConfig(SchoolConfigUpdateRequest request);
        Task<DataResponse<SchoolConfigResponse>> DeleteSchoolConfig(int id);
    }
}
