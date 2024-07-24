using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Service;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/dashboards")]
    [ApiController]
    //[Authorize]
    public class DashBoardController : ControllerBase
    {
        private DashBoardService _service;
        public DashBoardController(DashBoardService service)
        {
            _service = service;
        }
        [HttpGet("violations-by-month-and-week")]
        public async Task<IActionResult> GetViolationsByMonthAndWeek([FromQuery] int schoolId, [FromQuery] short year, [FromQuery] int month, [FromQuery] int? weekNumber = null)
        {
            var response = await _service.GetViolationsByMonthAndWeek(schoolId, year, month, weekNumber);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("violations-by-year-and-classname")]
        public async Task<IActionResult> GetViolationsByYearAndClassName([FromQuery] short year, [FromQuery] string className, [FromQuery] int schoolId)
        {
            var response = await _service.GetViolationsByYearAndClassName(year, className, schoolId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("top-3-frequent-violations")]
        public async Task<IActionResult> GetTopFrequentViolations([FromQuery] short year, [FromQuery] int schoolId)
        {
            var response = await _service.GetTopFrequentViolations(year, schoolId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("classes-most-violations")]
        public async Task<IActionResult> GetClassesWithMostViolations([FromQuery] short year, [FromQuery] int schoolId)
        {
            var response = await _service.GetClassesWithMostViolations(year, schoolId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("top-5-students-most-violations")]
        public async Task<IActionResult> GetTop5StudentsWithMostViolations([FromQuery] short year, [FromQuery] int schoolId)
        {
            var response = await _service.GetTop5StudentsWithMostViolations(year, schoolId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("class-with-most-students-violations")]
        public async Task<IActionResult> GetClassWithMostStudentViolations([FromQuery] short year, [FromQuery] int schoolId)
        {
            var response = await _service.GetClassWithMostStudentViolations(year, schoolId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
