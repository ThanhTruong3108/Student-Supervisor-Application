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
   // [Authorize]
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
        public async Task<ActionResult<DataResponse<List<EvaluationResponse>>>> GetEvaluationsBySchoolId(int schoolId)
        {
            try
            {
                var evaluations = await evaluationService.GetEvaluationsBySchoolId(schoolId);
                return Ok(evaluations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("weekly")]
        public async Task<ActionResult<DataResponse<List<ClassRankResponse>>>> GetWeeklyRanks(int schoolId, DateTime fromDate, DateTime toDate)
        {
            var response = await evaluationService.GetEvaluationRanks(schoolId, fromDate, toDate);
            return Ok(response);
        }

        [HttpGet("monthly")]
        public async Task<ActionResult<DataResponse<List<ClassRankResponse>>>> GetMonthlyRanks(int schoolId, int year, int month)
        {
            var fromDate = new DateTime(year, month, 1);
            var toDate = fromDate.AddMonths(1).AddDays(-1);
            var response = await evaluationService.GetEvaluationRanks(schoolId, fromDate, toDate);
            return Ok(response);
        }

        [HttpGet("quarterly")]
        public async Task<ActionResult<DataResponse<List<ClassRankResponse>>>> GetQuarterlyRanks(int schoolId, int year, int quarter)
        {
            DateTime fromDate, toDate;
            switch (quarter)
            {
                case 1:
                    fromDate = new DateTime(year, 1, 1);
                    toDate = new DateTime(year, 3, 31);
                    break;
                case 2:
                    fromDate = new DateTime(year, 4, 1);
                    toDate = new DateTime(year, 6, 30);
                    break;
                case 3:
                    fromDate = new DateTime(year, 7, 1);
                    toDate = new DateTime(year, 9, 30);
                    break;
                case 4:
                    fromDate = new DateTime(year, 10, 1);
                    toDate = new DateTime(year, 12, 31);
                    break;
                default:
                    return BadRequest("Quý không hợp lệ");
            }
            var response = await evaluationService.GetEvaluationRanks(schoolId, fromDate, toDate);
            return Ok(response);
        }

        [HttpGet("yearly")]
        public async Task<ActionResult<DataResponse<List<ClassRankResponse>>>> GetYearlyRanks(int schoolId, int year)
        {
            var fromDate = new DateTime(year, 1, 1);
            var toDate = new DateTime(year, 12, 31);
            var response = await evaluationService.GetEvaluationRanks(schoolId, fromDate, toDate);
            return Ok(response);
        }

    }
}
