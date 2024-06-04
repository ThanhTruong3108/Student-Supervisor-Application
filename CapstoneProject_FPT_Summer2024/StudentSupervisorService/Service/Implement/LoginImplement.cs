using AutoMapper;
using Infrastructures.Interfaces.IUnitOfWork;
using Microsoft.IdentityModel.Tokens;
using StudentSupervisorService.Authentication;
using StudentSupervisorService.Helpers.MemoryCache;
using StudentSupervisorService.Models.Response.UserResponse;
using StudentSupervisorService.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace StudentSupervisorService.Service.Implement
{
    public class LoginImplement : LoginService
    {
        private readonly IAuthentication _authentication;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheManager _cacheManager;
        private readonly AppConfiguration _appConfiguration;

        public LoginImplement(IAuthentication authentication, IUnitOfWork unitOfWork, IMapper mapper, AppConfiguration appConfiguration, ICacheManager cacheManager)
        {
            _authentication = authentication;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appConfiguration = appConfiguration;
            _cacheManager = cacheManager;
        }

        public async Task<bool> Logout(int accountId)
        {
            try
            {
                _cacheManager.Remove(accountId.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<AuthResponse<ResponseOfUser>> ValidateUser(RequestLogin accountLogin)
        {
            var user = await _unitOfWork.User.GetAccountByPhone(accountLogin.Phone);
            var response = new AuthResponse<ResponseOfUser>();

            if (user == null)
            {
                response.Success = false;
                response.Message = "Phone Not Exist";
                return response;
            }

            var result = _authentication.Verify(user.Password, accountLogin.Password);
            if (!result)
            {
                response.Success = false;
                response.Message = "Invalid Password";
                return response;
            }

            string role = user.Role.RoleName;

            response.Data = _mapper.Map<ResponseOfUser>(user);
            response.Token = _authentication.GenerateToken(user, _appConfiguration.JWTSecretKey, role);
            response.Success = true;
            response.Message = "Login Success";
            return response;
        }

        public async Task<AuthResponse<AccountResponse>> ValidateToken(string token)
        {
            var response = new AuthResponse<AccountResponse>();
            if (string.IsNullOrEmpty(token))
            {
                response.Message = "Token is required";
                response.Success = false;
                return response;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var tokenData = tokenHandler.ReadJwtToken(token);
                var tokenExp = tokenData.Claims.FirstOrDefault(claim => claim.Type.Equals("exp"))!.Value;
                var tokenDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(tokenExp)).UtcDateTime;
                var now = DateTime.UtcNow;

                if (now > tokenDate)
                {
                    response.Success = false;
                    response.Message = "Token is expired";
                    return response;
                }

                var phoneClaim = tokenData.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.MobilePhone))?.Value;
                var roleClaim = tokenData.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Role))?.Value;

                if (string.IsNullOrEmpty(phoneClaim) || string.IsNullOrEmpty(roleClaim))
                {
                    response.Success = false;
                    response.Message = "Invalid token claims";
                    return response;
                }

                var user = await _unitOfWork.User.GetAccountByPhone(phoneClaim);

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Account not found";
                    return response;
                }

                response.Data = _mapper.Map<AccountResponse>(user);
                response.Token = _authentication.GenerateToken(user, _appConfiguration.JWTSecretKey, roleClaim);
                response.Success = true;
                response.Message = "Token validated";
                return response;
            }
            catch
            {
                response.Message = "Invalid token";
                response.Success = false;
                return response;
            }
        }

    }
}
