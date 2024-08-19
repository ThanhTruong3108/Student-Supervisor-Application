using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.StudentSupervisorResponse;
using StudentSupervisorService.Models.Request.StudentSupervisorRequest;
using Microsoft.AspNetCore.Authorization;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/student-supervisors")]
    [ApiController]
    [Authorize]
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
            try
            {
                var updatedStuSuper = await _service.UpdateStudentSupervisor(id, request);
                return updatedStuSuper == null ? NotFound() : Ok(updatedStuSuper);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DataResponse<StudentSupervisorResponse>>> DeleteStudentSupervisor(int id)
        {
            try
            {
                var deletedStuSuper = await _service.DeleteStudentSupervisor(id);
                return deletedStuSuper == null ? NoContent() : Ok(deletedStuSuper);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("school/{schoolId}")]
        public async Task<ActionResult<DataResponse<List<StudentSupervisorResponse>>>> GetStudentSupervisorsBySchoolId(int schoolId)
        {
            try
            {
                var stuSupers = await _service.GetStudentSupervisorsBySchoolId(schoolId);
                return Ok(stuSupers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("active-stu-supervisors/{schoolId}")]
        public async Task<ActionResult<DataResponse<List<StudentSupervisorResponse>>>> GetActiveStudentSupervisorsWithLessThanTwoSchedules(int schoolId)
        {
            try
            {
                var stuSupers = await _service.GetActiveStudentSupervisorsWithLessThanTwoSchedules(schoolId);
                return Ok(stuSupers);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
