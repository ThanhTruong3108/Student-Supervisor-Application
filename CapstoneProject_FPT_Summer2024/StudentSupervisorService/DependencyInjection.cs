using Domain.Entity;
using Infrastructures.Interfaces.IUnitOfWork;
using Infrastructures.Interfaces;
using Infrastructures.Repository.UnitOfWork;
using Infrastructures.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentSupervisorService.Service;
using StudentSupervisorService.Service.Implement;
using StudentSupervisorService.Mapper;

namespace StudentSupervisorService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Add Database
            services.AddDbContext<SchoolRulesContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            //Add DI Container
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IHighSchoolRepository, HighSchoolRepository>();
            services.AddTransient<HighSchoolService, HighSchoolImplement>();

            services.AddTransient<ISchoolYearRepository, SchoolYearRepository>();
            services.AddTransient<SchoolYearService, SchoolYearImplement>();


            services.AddTransient<IServiceCollection, ServiceCollection>();



            //AUTOMAPPER
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            return services;
        }
    }
}
