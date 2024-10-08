﻿using StudentSupervisorService.Models.Request.PatrolScheduleRequest;
using StudentSupervisorService.Models.Response.PatrolScheduleResponse;
using StudentSupervisorService.Models.Response;

namespace StudentSupervisorService.Service
{
    public interface PatrolScheduleService
    {
        Task<DataResponse<List<PatrolScheduleResponse>>> GetAllPatrolSchedules(string sortOrder);
        Task<DataResponse<PatrolScheduleResponse>> GetPatrolScheduleById(int id);
        Task<DataResponse<PatrolScheduleResponse>> CreatePatrolSchedule(PatrolScheduleCreateRequest request);
        Task<DataResponse<PatrolScheduleResponse>> UpdatePatrolSchedule(PatrolScheduleUpdateRequest request);
        Task<DataResponse<PatrolScheduleResponse>> DeletePatrolSchedule(int id);
        Task<DataResponse<List<PatrolScheduleResponse>>> GetPatrolSchedulesBySchoolId(int schoolId);
        Task<DataResponse<List<PatrolScheduleResponse>>> GetPatrolSchedulesByUserId(int userId);
        Task<DataResponse<List<PatrolScheduleResponse>>> GetPatrolSchedulesBySupervisorUserId(int userId);
    }
}
