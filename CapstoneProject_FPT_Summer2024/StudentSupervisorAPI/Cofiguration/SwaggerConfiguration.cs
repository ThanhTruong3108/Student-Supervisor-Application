using Microsoft.OpenApi.Models;

namespace StudentSupervisorAPI.Cofiguration
{
    //public static class SwaggerConfiguration
    //{
    //    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    //    {
    //        services.AddEndpointsApiExplorer();
    //        services.AddSwaggerGen(sw =>
    //        {
    //            sw.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "1.0" });
    //            sw.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //            {
    //                In = ParameterLocation.Header,
    //                Description = "Insert JWT Token",
    //                Name = "Authorization",
    //                Type = SecuritySchemeType.Http,
    //                BearerFormat = "JWT",
    //                Scheme = "bearer"
    //            });
    //            sw.AddSecurityRequirement(new OpenApiSecurityRequirement
    //            {
    //                {
    //                    new OpenApiSecurityScheme
    //                    {
    //                        Reference = new OpenApiReference
    //                        {
    //                            Type = ReferenceType.SecurityScheme,
    //                            Id = "Bearer"
    //                        }
    //                    },
    //                    new string[] { }
    //                }
    //            });
    //        });
    //        return services;
    //    }
    //}
}
