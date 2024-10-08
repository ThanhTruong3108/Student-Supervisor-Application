﻿using StudentSupervisorService.Models.Request.DisciplineRequest;
using StudentSupervisorService.Models.Response.DisciplineResponse;
using StudentSupervisorService.Models.Response;


namespace StudentSupervisorService.Service
{
    public interface DisciplineService
    {
        Task<DataResponse<List<DisciplineResponse>>> GetAllDisciplines(string sortOrder);
        Task<DataResponse<DisciplineResponse>> GetDisciplineById(int id);
        Task<DataResponse<DisciplineResponse>> CreateDiscipline(DisciplineCreateRequest request);
        Task<DataResponse<DisciplineResponse>> UpdateDiscipline(int id, DisciplineUpdateRequest request);
        Task<DataResponse<DisciplineResponse>> DeleteDiscipline(int id);
        Task<DataResponse<List<DisciplineResponse>>> GetDisciplinesBySchoolId(int schoolId, string sortOrder = "asc", short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null);
        Task<DataResponse<DisciplineResponse>> ExecutingDiscipline(int disciplineId);
        Task<DataResponse<DisciplineResponse>> DoneDiscipline(int disciplineId);
        Task<DataResponse<DisciplineResponse>> ComplainDiscipline(int disciplineId);
        Task<DataResponse<List<DisciplineResponse>>> GetDisciplinesByUserId(int userId, string sortOrder, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null);
        Task<DataResponse<List<DisciplineResponse>>> GetDisciplinesBySupervisorUserId(int userId, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null);
    }
}
