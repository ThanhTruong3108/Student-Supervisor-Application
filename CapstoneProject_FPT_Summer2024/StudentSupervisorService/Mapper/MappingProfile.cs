using AutoMapper;
using Domain.Entity;
using StudentSupervisorService.Models.Request.SchoolYearRequest;
using StudentSupervisorService.Models.Response.ClassGroupResponse;
using StudentSupervisorService.Models.Response.ClassResponse;
using StudentSupervisorService.Models.Response.HighschoolResponse;
using StudentSupervisorService.Models.Response.SchoolYearResponse;
using StudentSupervisorService.Models.Response.StudentResponse;
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
            CreateMap<Class, ClassResponse>();
            CreateMap<ClassGroup, ClassGroupResponse>();
            CreateMap<Student, StudentResponse>();





            //-------------------------------------------------------------------------------------------------------------       
            CreateMap<SchoolYear, ResponseOfSchoolYear>()
               .ForMember(re => re.SchoolName, act => act.MapFrom(src => src.School.Name));

            CreateMap<RequestCreateSchoolYear, SchoolYear>();
    //--------------------------------------------------------------------------------------------------------------



        }
    }
}
