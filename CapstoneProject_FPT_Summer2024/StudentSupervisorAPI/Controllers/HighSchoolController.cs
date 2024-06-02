using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Request.HighSchoolRequest;
using StudentSupervisorService.Models.Request.SchoolYearRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.HighschoolResponse;
using StudentSupervisorService.Models.Response.SchoolYearResponse;
using StudentSupervisorService.Service;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/highschools")]
    [ApiController]
    public class HighSchoolController : ControllerBase
    {
        private HighSchoolService _service;
        public HighSchoolController(HighSchoolService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<ResponseOfHighSchool>>>> GetHighSchools(int page = 1, int pageSize = 5, string sortOrder = "asc")
        {
            try
            {
                var highSchools = await _service.GetAllHighSchools(page, pageSize, sortOrder);
                return Ok(highSchools);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfHighSchool>>> GetHighSchoolById(int id)
        {
            try
            {
                var highSchool = await _service.GetHighSchoolById(id);
                return Ok(highSchool);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts(string? code = null, string? name = null, string? address = null, string? phone = null, string sortOrder = "asc")
        {
            try
            {
                var highSchools = await _service.SearchHighSchools(code, name, address, phone, sortOrder);
                return Ok(highSchools);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<DataResponse<ResponseOfHighSchool>>> CreateHighSchool(RequestOfHighSchool request)
        {
            var createdHighSchool = await _service.CreateHighSchool(request);
            return createdHighSchool == null ? NotFound() : Ok(createdHighSchool);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfHighSchool>>> UpdateHighSchool(int id, RequestOfHighSchool request)
        {
            var updatedHighSchool = await _service.UpdateHighSchool(id, request);
            return updatedHighSchool == null ? NotFound() : Ok(updatedHighSchool);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHighSchool(int id)
        {
            var deletedHighSchool = _service.DeleteHighSchool(id);
            return deletedHighSchool == null ? NoContent() : Ok(deletedHighSchool);
        }
    }
}
