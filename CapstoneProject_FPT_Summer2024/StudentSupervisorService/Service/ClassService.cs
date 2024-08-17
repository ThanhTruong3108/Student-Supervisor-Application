using StudentSupervisorService.Models.Request.ClassRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ClassResponse;


namespace StudentSupervisorService.Service
{
    public interface ClassService
    {
        Task<DataResponse<List<ClassResponse>>> GetAllClasses(string sortOrder);
        Task<DataResponse<ClassResponse>> GetClassById(int id);
        Task<DataResponse<ClassResponse>> CreateClass(ClassCreateRequest classCreateRequest);
        Task<DataResponse<ClassResponse>> UpdateClass(int id, ClassUpdateRequest request);
        Task<DataResponse<ClassResponse>> DeleteClass(int id);
        Task<DataResponse<List<ClassResponse>>> GetClassesBySchoolId(int schoolId);
        Task<DataResponse<ClassResponse>> GetClassByScheduleId(int scheduleId);
        Task<DataResponse<List<ClassResponse>>> GetClassesByUserId(int userId);
        Task<DataResponse<List<ClassResponse>>> GetClassesBySupervisorId(int userId);
    }
}
