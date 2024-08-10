using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response.ViolationResponse;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/dashboards")]
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> GetViolationsByYearAndClassName([FromQuery] int schoolId, [FromQuery] short year, [FromQuery] string className, [FromQuery] int? month = null, [FromQuery] int? weekNumber = null)
        {
            var response = await _service.GetViolationsByYearAndClassName(schoolId, year, className, month, weekNumber);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("top-5-frequent-violations")]
        public async Task<IActionResult> GetTopFrequentViolations([FromQuery] int schoolId, [FromQuery] short year, [FromQuery] int? month = null, [FromQuery] int? weekNumber = null)
        {
            var response = await _service.GetTopFrequentViolations(schoolId, year, month, weekNumber);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("classes-most-violations")]
        public async Task<IActionResult> GetClassesWithMostViolations([FromQuery] int schoolId, [FromQuery] short year, [FromQuery] int month, [FromQuery] int? weekNumber = null)
        {
            var response = await _service.GetClassesWithMostViolations(schoolId, year, month, weekNumber);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpGet("top-5-students-most-violations")]
        public async Task<IActionResult> GetTop5StudentsWithMostViolations([FromQuery] int schoolId, [FromQuery] short year, [FromQuery] int? month = null, [FromQuery] int? weekNumber = null)
        {
            var response = await _service.GetTop5StudentsWithMostViolations(schoolId, year, month, weekNumber);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("class-with-most-students-violations")]
        public async Task<IActionResult> GetClassWithMostStudentViolations([FromQuery] int schoolId, [FromQuery] short year, [FromQuery] int month, [FromQuery] int? weekNumber = null)
        {
            var response = await _service.GetClassWithMostStudentViolations(schoolId, year, month, weekNumber);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("students/{studentCode}")]
        public async Task<ActionResult<DataResponse<List<ResponseOfViolation>>>> GetViolationsByStudentCode(string studentCode)
        {
            try
            {
                var violationsResponse = await _service.GetViolationsByStudentCode(studentCode);
                return Ok(violationsResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{studentCode}/{year}")]
        public async Task<ActionResult<DataResponse<List<ResponseOfViolation>>>> GetViolationsByStudentCodeAndYear(string studentCode, short year)
        {
            try
            {
                var violationsResponse = await _service.GetViolationsByStudentCodeAndYear(studentCode, year);
                return Ok(violationsResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("count/{studentCode}")]
        public async Task<ActionResult<DataResponse<Dictionary<int, int>>>> GetViolationCountByYear(string studentCode)
        {
            try
            {
                var violationCountResponse = await _service.GetViolationCountByYear(studentCode);
                return Ok(violationCountResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("count-violations-by-year")]
        public async Task<IActionResult> CountViolationsByYear([FromQuery] int schoolId, [FromQuery] short year)
        {
            var response = await _service.CountViolationsByYear(schoolId, year);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("count-violations-by-year-month")]
        public async Task<IActionResult> CountViolationsByYearAndMonth([FromQuery] int schoolId, [FromQuery] short year, [FromQuery] int month)
        {
            var response = await _service.CountViolationsByYearAndMonth(schoolId, year, month);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("count-violations-by-year-month-week")]
        public async Task<IActionResult> CountViolationsByYearMonthAndWeek([FromQuery] int schoolId, [FromQuery] short year, [FromQuery] int month, [FromQuery] int weekNumber)
        {
            var response = await _service.CountViolationsByYearMonthAndWeek(schoolId, year, month, weekNumber);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


    }
}
