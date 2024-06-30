using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Request.TeacherRequest;
using StudentSupervisorService.Models.Response.TeacherResponse;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.StudentSupervisorResponse;
using StudentSupervisorService.Models.Request.StudentSupervisorRequest;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/student-supervisors")]
    [ApiController]
    public class StudentSupervisorController : ControllerBase
    {
        private StudentSupervisorServices _service;
        public StudentSupervisorController(StudentSupervisorServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<StudentSupervisorResponse>>>> GetStudentSupervisors(string sortOrder = "asc")
        {
            try
            {
                var stuSupers = await _service.GetAllStudentSupervisors(sortOrder);
                return Ok(stuSupers);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<StudentSupervisorResponse>>> GetStudentSupervisorById(int id)
        {
            try
            {
                var stuSuper = await _service.GetStudentSupervisorById(id);
                return Ok(stuSuper);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchStudentSupervisors(int? userId = null, int? studentInClassId = null, string sortOrder = "asc")
        {
            try
            {
                var stuSuper = await _service.SearchStudentSupervisors(userId, studentInClassId, sortOrder);
                return Ok(stuSuper);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-account")]
        public async Task<ActionResult<StudentSupervisorResponse>> CreateStudentSupervisorAccount(StudentSupervisorRequest request)
        {
            try
            {
                var stuSuper = await _service.CreateAccountStudentSupervisor(request);
                return stuSuper == null ? NotFound() : Ok(new { Success = true, Data = stuSuper });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DataResponse<StudentSupervisorResponse>>> UpdateStudentSupervisor(int id, StudentSupervisorRequest request)
        {
            var updatedStuSuper = await _service.UpdateStudentSupervisor(id, request);
            return updatedStuSuper == null ? NotFound() : Ok(updatedStuSuper);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudentSupervisor(int id)
        {
            var deletedStuSuper = _service.DeleteStudentSupervisor(id);
            return deletedStuSuper == null ? NoContent() : Ok(deletedStuSuper);
        }
    }
}
