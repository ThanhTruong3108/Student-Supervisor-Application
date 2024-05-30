using AutoMapper;
using Domain.Entity;
using StudentSupervisorService.Models.Request.SchoolYearRequest;
using StudentSupervisorService.Models.Response.HighschoolResponse;
using StudentSupervisorService.Models.Response.SchoolYearResponse;
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

            
            
            
            
            
     //-------------------------------------------------------------------------------------------------------------       
            CreateMap<SchoolYear, ResponseOfSchoolYear>()
               .ForMember(re => re.SchoolName, act => act.MapFrom(src => src.School.Name));

            CreateMap<RequestCreateSchoolYear, SchoolYear>();
    //--------------------------------------------------------------------------------------------------------------



        }
    }
}
