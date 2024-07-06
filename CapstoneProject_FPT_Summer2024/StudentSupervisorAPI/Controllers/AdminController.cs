using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response.UserResponse;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.AdminResponse;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/admins")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private AdminService _service;
        public AdminController(AdminService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<DataResponse<List<AdminResponse>>>> GetUsers(string sortOrder = "asc")
        {
            try
            {
                var admins = await _service.GetAllAdmins(sortOrder);
                return Ok(admins);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
