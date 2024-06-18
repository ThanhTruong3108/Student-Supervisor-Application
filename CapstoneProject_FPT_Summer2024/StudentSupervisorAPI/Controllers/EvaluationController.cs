using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Request.EvaluationRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.EvaluationResponse;
using StudentSupervisorService.Service;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/evaluations")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        private readonly EvaluationService evaluationService;
        public EvaluationController(EvaluationService evaluationService)
        {
            this.evaluationService = evaluationService;
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<EvaluationResponse>>>> GetAllEvaluations(string sortOrder = "asc")
        {
            try
            {
                var evaluationsResponse = await evaluationService.GetAllEvaluations(sortOrder);
                return Ok(evaluationsResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<EvaluationResponse>>> GetEvaluationById(int id)
        {
            try
            {
                var evaluationResponse = await evaluationService.GetEvaluationById(id);
                return Ok(evaluationResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<DataResponse<List<EvaluationResponse>>>> SearchEvaluations(
                       int? schoolYearId,
                       string? description,
                       DateTime? from,
                       DateTime? to,
                       short? point,
                       string sortOrder = "asc")
        {
            try
            {
                var evaluationsResponse = await evaluationService.SearchEvaluations(schoolYearId, description, from, to, point, sortOrder);
                return Ok(evaluationsResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<EvaluationResponse>>> CreateEvaluation(EvaluationCreateRequest request)
        {
            try
            {
                var evaluationResponse = await evaluationService.CreateEvaluation(request);
                return Ok(evaluationResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<DataResponse<EvaluationResponse>>> UpdateEvaluation(EvaluationUpdateRequest request)
        {
            try
            {
                var evaluationResponse = await evaluationService.UpdateEvaluation(request);
                return Ok(evaluationResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
