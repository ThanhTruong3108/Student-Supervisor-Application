using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Request.SchoolAdminRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.SchoolAdminResponse;
using StudentSupervisorService.Models.Response.UserResponse;
using StudentSupervisorService.Service;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/school-admins")]
    [ApiController]
    public class SchoolAdminController : ControllerBase
    {
        private readonly SchoolAdminService schoolAdminService;
        private UserService _userService;

        public SchoolAdminController(SchoolAdminService schoolAdminService, UserService userService)
        {
            this.schoolAdminService = schoolAdminService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<SchoolAdminResponse>>>> GetSchoolAdmins(string sortOrder = "asc")
        {
            try
            {
                var schoolAdmins = await schoolAdminService.GetAllSchoolAdmins(sortOrder);
                return Ok(schoolAdmins);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<SchoolAdminResponse>>> GetSchoolAdminById(int id)
        {
            try
            {
                var schoolAdmin = await schoolAdminService.GetBySchoolAdminId(id);
                return Ok(schoolAdmin);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult<DataResponse<SchoolAdminResponse>>> GetUsersBySchoolAdminId(int id)
        {
            try
            {
                var schoolAdmin = await _userService.GetUsersBySchoolAdminId(id);
                return Ok(schoolAdmin);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchSchoolAdmins(int? schoolId = null, int? adminId = null, string sortOrder = "asc")
        {
            try
            {
                var schoolAdmins = await schoolAdminService.SearchSchoolAdmins(schoolId, adminId, sortOrder);
                return Ok(schoolAdmins);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
