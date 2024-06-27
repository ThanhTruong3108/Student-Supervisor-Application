using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.TeacherResponse;
using StudentSupervisorService.Models.Request.TeacherRequest;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/teachers")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private TeacherService _service;
        public TeacherController(TeacherService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<TeacherResponse>>>> GetTeachers(string sortOrder)
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

        [HttpGet("search")]
        public async Task<IActionResult> SearchTeachers(int? schoolId = null, int? userId = null, bool sex = true, string sortOrder = "asc")
        {
            try
            {
                var teacher = await _service.SearchTeachers(schoolId, userId, sex, sortOrder);
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
            var updatedTeacher = await _service.UpdateTeacher(id, request);
            return updatedTeacher == null ? NotFound() : Ok(updatedTeacher);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeacher(int id)
        {
            var deletedTeacher = _service.DeleteTeacher(id);
            return deletedTeacher == null ? NoContent() : Ok(deletedTeacher);
        }
    }
}
