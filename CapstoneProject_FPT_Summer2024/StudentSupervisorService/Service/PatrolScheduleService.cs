using StudentSupervisorService.Models.Request.PatrolScheduleRequest;
using StudentSupervisorService.Models.Response.PatrolScheduleResponse;
using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service
{
    public interface PatrolScheduleService
    {
        Task<DataResponse<List<PatrolScheduleResponse>>> GetAllPatrolSchedules(string sortOrder);
        Task<DataResponse<PatrolScheduleResponse>> GetPatrolScheduleById(int id);
        Task<DataResponse<List<PatrolScheduleResponse>>> SearchPatrolSchedules(
            int? classId,
            int? supervisorId,
            int? teacherId,
            DateTime? from,
            DateTime? to,
            string sortOrder);
        Task<DataResponse<PatrolScheduleResponse>> CreatePatrolSchedule(PatrolScheduleCreateRequest request);
        Task<DataResponse<PatrolScheduleResponse>> UpdatePatrolSchedule(PatrolScheduleUpdateRequest request);
        Task<DataResponse<PatrolScheduleResponse>> DeletePatrolSchedule(int id);
    }
}
