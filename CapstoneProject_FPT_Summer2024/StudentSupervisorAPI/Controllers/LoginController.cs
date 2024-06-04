using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response.UserResponse;
using StudentSupervisorService.Models;
using StudentSupervisorService.Service;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _login;
        public LoginController(LoginService loginService)
        {
            _login = loginService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponse<ResponseOfUser>>> Login(RequestLogin login)
        {
            var response = await _login.ValidateUser(login);
            return Ok(response);
        }

        [HttpPost("logout")]
        public async Task<ActionResult<bool>> Logout(int accountId)
        {
            var response = await _login.Logout(accountId);
            return Ok(response);
        }

        [HttpGet("token/renew")]
        public async Task<ActionResult<AuthResponse<AccountResponse>>> RenewToken()
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var response = await _login.ValidateToken(token);
            return Ok(response);
        }
    }
}
