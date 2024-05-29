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
        public async Task<ActionResult<DataResponse<List<ResponseOfHighSchool>>>> GetHighSchools()
        {
            try
            {
                var highSchools = await _service.GetAllHighSchools();
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
    }
}
