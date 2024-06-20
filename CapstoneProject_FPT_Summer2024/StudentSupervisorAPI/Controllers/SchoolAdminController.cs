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

        private readonly UserService userService;
        public SchoolAdminController(SchoolAdminService schoolAdminService, UserService userService)
        {
            this.schoolAdminService = schoolAdminService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<SchoolAdminResponse>>>> GetAllSchoolAdmins(string sortOrder = "asc")
        {
            try
            {
                var schoolAdminsResponse = await schoolAdminService.GetAllSchoolAdmins(sortOrder);
                return Ok(schoolAdminsResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //get school admin by school admin id
        [HttpGet("{schoolAdminId}")]
        public async Task<ActionResult<DataResponse<SchoolAdminResponse>>> GetSchoolAdminBySchoolAdminId(int schoolAdminId)
        {
            try
            {
                var schoolAdminResponse = await schoolAdminService.GetBySchoolAdminId(schoolAdminId);
                return Ok(schoolAdminResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //get school admin by user id
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<DataResponse<SchoolAdminResponse>>> GetSchoolAdminByUserId(int userId)
        {
            try
            {
                var schoolAdminResponse = await schoolAdminService.GetByUserId(userId);
                return Ok(schoolAdminResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<DataResponse<List<SchoolAdminResponse>>>> SearchSchoolAdmins(
                       int? schoolId,
                       int? userId)
        {
            try
            {
                var schoolAdminsResponse = await schoolAdminService.SearchSchoolAdmins(schoolId, userId);
                return Ok(schoolAdminsResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<SchoolAdminResponse>>> CreateSchoolAdmin(SchoolAdminCreateRequest request)
        {
            try
            {
                var schoolAdminResponse = await schoolAdminService.CreateSchoolAdmin(request);
                return Created("", schoolAdminResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<DataResponse<SchoolAdminResponse>>> UpdateSchoolAdmin(SchoolAdminUpdateRequest request)
        {
            try
            {
                var schoolAdminResponse = await schoolAdminService.UpdateSchoolAdmin(request);
                return Ok(schoolAdminResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DataResponse<SchoolAdminResponse>>> DeleteSchoolAdmin(int id)
        {
            try
            {
                var schoolAdminResponse = await schoolAdminService.DeleteSchoolAdmin(id);
                return Ok(schoolAdminResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{schoolAdminId}/users")]
        public async Task<ActionResult<DataResponse<List<ResponseOfUser>>>> GetUsersBySchoolAdminId(int schoolAdminId)
        {
            try
            {
                var users = await userService.GetUsersBySchoolAdminId(schoolAdminId);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
