using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.HighschoolResponse;
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
    }
}
