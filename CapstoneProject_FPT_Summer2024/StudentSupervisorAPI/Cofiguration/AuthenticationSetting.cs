using Domain.Enums.Role;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace StudentSupervisorAPI.Cofiguration
{
    //public static class AuthenticationSetting
    //{
    //    public static IServiceCollection ConfigureSecurity(this IServiceCollection services, string secretKey)
    //    {
    //        var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
    //        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
    //        {
    //            opt.TokenValidationParameters = new TokenValidationParameters
    //            {
    //                //auto generate token
    //                ValidateIssuer = false,
    //                ValidateAudience = false,

    //                //sign in token
    //                ValidateIssuerSigningKey = true,
    //                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
    //                ClockSkew = TimeSpan.Zero
    //            };
    //        });

    //        services.AddAuthorization(options =>
    //        {
    //            options.AddPolicy("SchoolAdmin", policy =>
    //            {
    //                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
    //                policy.RequireAuthenticatedUser();
    //                policy.RequireClaim("Role", RoleAccountEnum.SCHOOLADMIN.ToString());
    //            });

    //            options.AddPolicy("Teacher", policy =>
    //            {
    //                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
    //                policy.RequireAuthenticatedUser();
    //                policy.RequireClaim("Role", RoleAccountEnum.TEACHER.ToString());
    //            });

    //            options.AddPolicy("StudentSupervisor", policy =>
    //            {
    //                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
    //                policy.RequireAuthenticatedUser();
    //                policy.RequireClaim("Role", RoleAccountEnum.STUDENTSUPERVISOR.ToString());
    //            });
    //        });

    //        return services;
    //    }
    //}
}
