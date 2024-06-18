using StudentSupervisorService.Models.Request.EvaluationDetailRequest;
using StudentSupervisorService.Models.Response.EvaluationDetailResponse;
using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service
{
    public interface EvaluationDetailService
    {
        Task<DataResponse<List<EvaluationDetailResponse>>> GetAllEvaluationDetails(string sortOrder);
        Task<DataResponse<EvaluationDetailResponse>> GetEvaluationDetailById(int id);
        Task<DataResponse<List<EvaluationDetailResponse>>> SearchEvaluationDetails(int? classId, int? evaluationId, string? status, string sortOrder);
        Task<DataResponse<EvaluationDetailResponse>> CreateEvaluationDetail(EvaluationDetailCreateRequest request);
        Task<DataResponse<EvaluationDetailResponse>> UpdateEvaluationDetail(EvaluationDetailUpdateRequest request);
        Task<DataResponse<EvaluationDetailResponse>> DeleteEvaluationDetail(int id);
    }
}
