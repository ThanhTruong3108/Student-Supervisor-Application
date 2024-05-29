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

            CreateMap<SchoolYear, ResponseOfSchoolYear>()
                .ForMember(re => re.SchoolYearId, act => act.MapFrom(src => src.SchoolYearId))
                .ForMember(re => re.Year, act => act.MapFrom(src => src.Year))
                .ForMember(re => re.StartDate, act => act.MapFrom(src => src.StartDate))
                .ForMember(re => re.EndDate, act => act.MapFrom(src => src.EndDate))
                .ForMember(re => re.SchoolId, act => act.MapFrom(src => src.SchoolId))
                .ForMember(re => re.SchoolName, act => act.MapFrom(src => src.School.Name));

            CreateMap<RequestCreateSchoolYear, SchoolYear>()
               .ForMember(re => re.SchoolId, act => act.MapFrom(src => src.SchoolId))
               .ForMember(re => re.Year, act => act.MapFrom(src => src.Year))
               .ForMember(re => re.StartDate, act => act.MapFrom(src => src.StartDate))
               .ForMember(re => re.EndDate, act => act.MapFrom(src => src.EndDate));
        }
    }
}
