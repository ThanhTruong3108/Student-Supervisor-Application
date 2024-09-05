using StudentSupervisorService.Models.Request.EvaluationRequest;
using StudentSupervisorService.Models.Response.EvaluationResponse;
using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentSupervisorService.Models.Response.ViolationTypeResponse;
using Domain.Entity.DTO;

namespace StudentSupervisorService.Service
{
    public interface EvaluationService
    {
        Task<DataResponse<List<EvaluationResponse>>> GetAllEvaluations(string sortOrder);
        Task<DataResponse<EvaluationResponse>> GetEvaluationById(int id);
        Task<DataResponse<EvaluationResponse>> CreateEvaluation(EvaluationCreateRequest request);
        Task<DataResponse<EvaluationResponse>> UpdateEvaluation(EvaluationUpdateRequest request);
        Task<DataResponse<EvaluationResponse>> DeleteEvaluation(int id);
        Task<DataResponse<List<EvaluationResponse>>> GetEvaluationsBySchoolId(int schoolId, string sortOrder, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null);
        Task<DataResponse<List<EvaluationRanking>>> GetEvaluationRankings(int schoolId, short year, string? semesterName = null, int? month = null, int? week = null);
    }
}
