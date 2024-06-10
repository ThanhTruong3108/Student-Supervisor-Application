using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response.UserResponse;
using StudentSupervisorService.Models;
using StudentSupervisorService.Service;
using Domain.Entity;
using Infrastructures.Interfaces.IUnitOfWork;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public LoginController(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await AuthenticateUser(login);

            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(new { token });
            }

            // Check if user exists to give a more specific error message
            var existingUser = await _unitOfWork.User.GetAccountByPhone(login.Phone);
            if (existingUser == null)
            {
                return Unauthorized(new { message = "Invalid phone number." });
            }
            else
            {
                return Unauthorized(new { message = "Invalid password." });
            }
        }

        private async Task<User> AuthenticateUser(LoginModel login)
        {
            var user = await _unitOfWork.User.GetAccountByPhone(login.Phone);
            if (user != null && user.Password == login.Password)
            {
                return user;
            }
            return null;
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Phone),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role.RoleName) // Add RoleName to claims
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginModel
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
