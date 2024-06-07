using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.UserResponse;
using StudentSupervisorService.Models.Request.UserRequest;
using Microsoft.AspNetCore.Authorization;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _service;
        public UserController(UserService service)
        {
            _service = service;
        }
        [Authorize(Roles = "SCHOOLADMIN")]
        [HttpGet]
        public async Task<ActionResult<DataResponse<List<ResponseOfUser>>>> GetUsers(int page = 1, int pageSize = 5, string sortOrder = "asc")
        {
            try 
            {
                var users = await _service.GetAllUsers(page, pageSize, sortOrder);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfUser>>> GetUserById(int id)
        {
            try
            {
                var user = await _service.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers(int? role = null, string? code = null, string? name = null, string? phone = null, string sortOrder = "asc")
        {
            try
            {
                var users = await _service.SearchUsers(role, code, name, phone, sortOrder);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<ResponseOfUser>>> CreateUser(RequestOfUser request)
        {
            var createdUser = await _service.CreateUser(request);
            return createdUser == null ? NotFound() : Ok(createdUser);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfUser>>> UpdateSchoolYear(int id, RequestOfUser request)
        {
            var updatedUser = await _service.UpdateUser(id, request);
            return updatedUser == null ? NotFound() : Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var deletedUser = _service.DeleteUser(id);
            return deletedUser == null ? NoContent() : Ok(deletedUser);
        }
    }
}
