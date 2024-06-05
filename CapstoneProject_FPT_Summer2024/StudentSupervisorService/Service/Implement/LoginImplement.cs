using AutoMapper;
using Infrastructures.Interfaces.IUnitOfWork;
using Microsoft.IdentityModel.Tokens;

using StudentSupervisorService.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Domain.Entity;
using Domain.Enums.Role;
using Infrastructures.Interfaces;

namespace StudentSupervisorService.Service.Implement
{
    public class LoginImplement : LoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;
        public LoginImplement(IUserRepository userRepository, JwtSettings jwtSettings, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings;
            _mapper = mapper;
        }
        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            var user = await _userRepository.GetAccountByPhone(request.Phone);
            if (user == null || user.Password != request.Password)
            {
                throw new Exception("Invalid credentials");
            }

            var token = GenerateJwtToken(user);
            return new AuthenticationResponse { Token = token };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, ((RoleAccountEnum)user.RoleId).ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
