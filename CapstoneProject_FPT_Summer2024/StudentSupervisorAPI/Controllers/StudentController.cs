using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.StudentResponse;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService studentService;
        public StudentController(StudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<StudentResponse>>>> GetAllStudents(int page = 1, int pageSize = 5, string sortOrder = "asc")
        {
            try
            {
                var studentsResponse = await studentService.GetAllStudents(page, pageSize, sortOrder);
                return Ok(studentsResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<StudentResponse>>> GetStudentById(int id)
        {
            try
            {
                var studentReponse = await studentService.GetStudentById(id);
                return Ok(studentReponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<DataResponse<List<StudentResponse>>>> SearchStudents(
            int? schoolId,
            string? code,
            string? name,
            bool? sex,
            DateTime? birthday,
            string? address,
            string? phone,
            string? sortOrder)
        {
            try
            {
                var studentsResponse = await studentService.SearchStudents(schoolId, code, name, sex, birthday, address, phone, sortOrder);
                return Ok(studentsResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
