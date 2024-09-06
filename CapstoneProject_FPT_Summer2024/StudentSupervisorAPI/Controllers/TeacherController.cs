using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.TeacherResponse;
using StudentSupervisorService.Models.Request.TeacherRequest;
using Microsoft.AspNetCore.Authorization;
using StudentSupervisorService.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/teachers")]
    [ApiController]
    [Authorize]
    public class TeacherController : ControllerBase
    {
        private TeacherService _service;
        private IAuthentication _authenService;
        public TeacherController(TeacherService service, IAuthentication authentication)
        {
            _service = service;
            _authenService = authentication;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SCHOOL_ADMIN")]
        [HttpPost("import-teachers")]
        public async Task<IActionResult> ImportTeachersFromExcel(IFormFile file)
        {
            try
            {
                if (file.FileName.EndsWith(".xls") || file.FileName.EndsWith(".xlsx"))
                {
                    // Lấy userId từ JWT
                    var userId = _authenService.GetUserIdFromContext(HttpContext);
                    if (userId == null)
                    {
                        return Unauthorized("Không lấy được UserID từ JWT");
                    }
                    var result = await _service.ImportTeachersFromExcel((int)userId, file);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("File không đúng định dạng");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<TeacherResponse>>>> GetTeachers(string sortOrder = "asc")
        {
            try
            {
                var teachers = await _service.GetAllTeachers(sortOrder);
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<TeacherResponse>>> GetTeacherById(int id)
        {
            try
            {
                var teacher = await _service.GetTeacherById(id);
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<TeacherResponse>> CreateTeacherAccount(RequestOfTeacher request)
        {
            try
            {
                var teacher = await _service.CreateAccountTeacher(request);
                return teacher == null ? NotFound() : Ok(new { Success = true, Data = teacher });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("supervisors")]
        public async Task<ActionResult<TeacherResponse>> CreateSupervisorAccount(RequestOfTeacher request)
        {
            try
            {
                var teacher = await _service.CreateAccountSupervisor(request);
                return teacher == null ? NotFound() : Ok(new { Success = true, Data = teacher });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DataResponse<TeacherResponse>>> UpdateTeacher(int id, RequestOfTeacher request)
        {
            try
            {
                var updatedTeacher = await _service.UpdateTeacher(id, request);
                return Ok(updatedTeacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DataResponse<TeacherResponse>>> DeleteTeacher(int id)
        {
            try
            {
                var deletedTeacher = await _service.DeleteTeacher(id);
                return Ok(deletedTeacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("school/{schoolId}")]
        public async Task<ActionResult<DataResponse<List<TeacherResponse>>>> GetTeachersBySchoolId(int schoolId)
        {
            try
            {
                var teachers = await _service.GetTeachersBySchoolId(schoolId);
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("role/teacher/{schoolId}")]
        public async Task<ActionResult<DataResponse<List<TeacherResponse>>>> GetAllTeachersWithRoleTeacher(int schoolId)
        {
            try
            {
                var response = await _service.GetAllTeachersWithRoleTeacher(schoolId);
                if (!response.Success)
                {
                    return NotFound(response.Message);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("role/supervisor/{schoolId}")]
        public async Task<ActionResult<DataResponse<List<TeacherResponse>>>> GetAllTeachersWithRoleSupervisor(int schoolId)
        {
            try
            {
                var response = await _service.GetAllTeachersWithRoleSupervisor(schoolId);
                if (!response.Success)
                {
                    return NotFound(response.Message);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("without-class/school/{schoolId}/year/{year}")]
        public async Task<ActionResult<DataResponse<List<TeacherResponse>>>> GetTeachersWithoutClass(int schoolId, short year)
        {
            try
            {
                var teachers = await _service.GetTeachersWithoutClass(schoolId, year);
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
