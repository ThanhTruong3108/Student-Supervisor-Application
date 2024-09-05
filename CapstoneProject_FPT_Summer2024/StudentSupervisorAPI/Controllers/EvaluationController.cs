using Domain.Entity.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Request.EvaluationRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.EvaluationResponse;
using StudentSupervisorService.Service;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/evaluations")]
    [ApiController]
    [Authorize]
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<DataResponse<EvaluationResponse>>> DeleteEvaluation(int id)
        {
            try
            {
                var deleteEvaluation = await evaluationService.DeleteEvaluation(id);
                return deleteEvaluation == null ? NoContent() : Ok(deleteEvaluation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("school/{schoolId}")]
        public async Task<ActionResult<DataResponse<List<EvaluationResponse>>>> GetEvaluationsBySchoolId(int schoolId, string sortOrder = "asc", [FromQuery] short? year = null, [FromQuery] string? semesterName = null, [FromQuery] int? month = null, [FromQuery] int? weekNumber = null)
        {
            try
            {
                var evaluations = await evaluationService.GetEvaluationsBySchoolId(schoolId, sortOrder, year, semesterName, month, weekNumber);
                return Ok(evaluations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetEvaluationRankings")]
        public async Task<IActionResult> GetEvaluationRankings(int schoolId, short year, string? semesterName = null, int? month = null, int? week = null)
        {
            try
            {
                var rankings = await evaluationService.GetEvaluationRankings(schoolId, year, semesterName, month, week);
                return Ok(rankings);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
