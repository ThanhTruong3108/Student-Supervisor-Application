using StudentSupervisorService.Models.Request.ClassRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ClassResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service
{
    public interface ClassService
    {
        Task<DataResponse<List<ClassResponse>>> GetAllClasses(string sortOrder);
        Task<DataResponse<ClassResponse>> GetClassById(int id);
        Task<DataResponse<List<ClassResponse>>> SearchClasses(int? schoolYearId, int? classGroupId, string? code, string? name, int? totalPoint, string sortOrder);
        Task<DataResponse<ClassResponse>> CreateClass(ClassCreateRequest classCreateRequest);
        Task<DataResponse<ClassResponse>> UpdateClass(ClassUpdateRequest classUpdateRequest);
        Task<DataResponse<ClassResponse>> DeleteClass(int id);
    }
}
