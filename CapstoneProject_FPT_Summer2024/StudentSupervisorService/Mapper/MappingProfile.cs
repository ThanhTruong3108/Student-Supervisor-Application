using AutoMapper;
using Domain.Entity;
using StudentSupervisorService.Models.Request.HighSchoolRequest;
using StudentSupervisorService.Models.Request.SchoolYearRequest;
using StudentSupervisorService.Models.Request.TimeRequest;
using StudentSupervisorService.Models.Request.UserRequest;
using StudentSupervisorService.Models.Response.HighschoolResponse;
using StudentSupervisorService.Models.Response.SchoolYearResponse;
using StudentSupervisorService.Models.Response.TimeResponse;
using StudentSupervisorService.Models.Response.UserResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HighSchool, ResponseOfHighSchool>();
            CreateMap<RequestOfHighSchool, HighSchool>();




            //-------------------------------------------------------------------------------------------------------------       
            CreateMap<SchoolYear, ResponseOfSchoolYear>()
               .ForMember(re => re.SchoolName, act => act.MapFrom(src => src.School.Name));

            CreateMap<RequestCreateSchoolYear, SchoolYear>();


            CreateMap<Time, ResponseOfTime>()
               .ForMember(re => re.ClassGroupName, act => act.MapFrom(src => src.ClassGroup.ClassGroupName))
               .ForMember(re => re.Hall, act => act.MapFrom(src => src.ClassGroup.Hall));

            CreateMap<RequestOfTime, Time>();

            CreateMap<User, ResponseOfUser>()
               .ForMember(re => re.RoleName, act => act.MapFrom(src => src.Role.RoleName));

            CreateMap<RequestOfUser, User>();
            //--------------------------------------------------------------------------------------------------------------



        }
    }
}
