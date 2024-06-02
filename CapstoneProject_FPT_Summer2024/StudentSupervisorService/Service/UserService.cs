using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.UserResponse;
using StudentSupervisorService.Models.Request.UserRequest;

namespace StudentSupervisorService.Service
{
    public interface UserService
    {
        Task<DataResponse<List<ResponseOfUser>>> GetAllUsers(int page, int pageSize, string sortOrder);
        Task<DataResponse<ResponseOfUser>> GetUserById(int id);
        Task<DataResponse<ResponseOfUser>> CreateUser(RequestOfUser request);
        Task DeleteUser(int userId);
        Task<DataResponse<ResponseOfUser>> UpdateUser(int id, RequestOfUser request);
        Task<DataResponse<List<ResponseOfUser>>> SearchUsers(int? role, string? code, string? name, string? phone, string sortOrder);
    }
}
