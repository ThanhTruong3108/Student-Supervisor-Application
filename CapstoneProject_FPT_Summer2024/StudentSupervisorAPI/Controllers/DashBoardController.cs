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

        [HttpGet("top-5-frequent-violations")]
        public async Task<IActionResult> GetTopFrequentViolations([FromQuery] int schoolId, [FromQuery] short year, [FromQuery] string? semesterName = null, [FromQuery] int? month = null, [FromQuery] int? weekNumber = null)
        {
            var response = await _service.GetTopFrequentViolations(schoolId, year, semesterName, month, weekNumber);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("classes-most-violations")]
        public async Task<IActionResult> GetClassesWithMostViolations([FromQuery] int schoolId, [FromQuery] short year, [FromQuery] string? semesterName = null, [FromQuery] int? month = null, [FromQuery] int? weekNumber = null)
        {
            var response = await _service.GetClassesWithMostViolations(schoolId, year, semesterName, month, weekNumber);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpGet("top-5-students-most-violations")]
        public async Task<IActionResult> GetTop5StudentsWithMostViolations([FromQuery] int schoolId, [FromQuery] short year, [FromQuery] string? semesterName = null, [FromQuery] int? month = null, [FromQuery] int? weekNumber = null)
        {
            var response = await _service.GetTop5StudentsWithMostViolations(schoolId, year, semesterName, month, weekNumber);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpGet("class-with-most-students-violations")]
        public async Task<IActionResult> GetClassWithMostStudentViolations([FromQuery] int schoolId, [FromQuery] short year, [FromQuery] string? semesterName = null, [FromQuery] int? month = null, [FromQuery] int? weekNumber = null)
        {
            var response = await _service.GetClassWithMostStudentViolations(schoolId, year, semesterName, month, weekNumber);
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

        [HttpGet("count-violations")]
        public async Task<IActionResult> CountViolations([FromQuery] int schoolId, [FromQuery] short year, [FromQuery] int? month = null, [FromQuery] int? weekNumber = null)
        {
            var response = await _service.CountViolations(schoolId, year, month, weekNumber);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("monthly-violations")]
        public async Task<IActionResult> GetMonthlyViolations([FromQuery] int schoolId, [FromQuery] short year)
        {
            var response = await _service.GetMonthlyViolations(schoolId, year);

            if (response.Success)
            {
                // Chuyển đổi kiểu dữ liệu từ object sang List<KeyValuePair<string, int>>
                var monthlyViolations = response.Data as List<KeyValuePair<string, int>>;

                if (monthlyViolations != null)
                {
                    var result = new
                    {
                        title = "Số lượng vi phạm hàng tháng",
                        unit = "Violations",
                        values = monthlyViolations
                            .Select(v => new { name = v.Key, data = v.Value })
                            .ToList()
                    };

                    // Trả về dữ liệu theo định dạng yêu cầu
                    return Ok(new DataResponse<object>
                    {
                        Data = new
                        {
                            title = result.title,
                            unit = result.unit,
                            values = result.values
                        },
                        Success = response.Success,
                        Message = response.Message
                    });
                }
                else
                {
                    return BadRequest(new DataResponse<object>
                    {
                        Data = null,
                        Success = false,
                        Message = "Dữ liệu không hợp lệ."
                    });
                }
            }

            return BadRequest(response);
        }
    }
}
