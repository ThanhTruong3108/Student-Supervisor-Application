using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response.SemesterResponse;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Request.ViolationTypeRequest;
using StudentSupervisorService.Models.Response.ViolationTypeResponse;
using StudentSupervisorService.Models.Request.SemesterRequest;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/semesters")]
    [ApiController]
    [Authorize]
    public class SemesterController : ControllerBase
    {
        private readonly SemesterService _service;

        public SemesterController(SemesterService service)
        {
            _service = service;
        }

        [HttpGet("school/{schoolId}")]
        public async Task<ActionResult<DataResponse<List<ResponseOfSemester>>>> GetSemestersBySchoolId(int schoolId)
        {
            try
            {
                var semesters = await _service.GetSemestersBySchoolId(schoolId);
                return Ok(semesters);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfSemester>>> UpdateSemester(int id, RequestOfSemester request)
        {
            try
            {
                var updateSemester = await _service.UpdateSemester(id, request);
                return updateSemester == null ? NotFound() : Ok(updateSemester);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
