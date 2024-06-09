﻿using AutoMapper;
using Domain.Entity;
using StudentSupervisorService.Models.Request.HighSchoolRequest;
using StudentSupervisorService.Models.Request.SchoolYearRequest;
using StudentSupervisorService.Models.Response.ClassGroupResponse;
using StudentSupervisorService.Models.Response.ClassResponse;
using StudentSupervisorService.Models.Response.HighschoolResponse;
using StudentSupervisorService.Models.Response.SchoolYearResponse;
using StudentSupervisorService.Models.Response.StudentResponse;
using StudentSupervisorService.Models.Request.TimeRequest;
using StudentSupervisorService.Models.Request.UserRequest;
using StudentSupervisorService.Models.Request.ViolationConfigRequest;
using StudentSupervisorService.Models.Request.ViolationGroupRequest;
using StudentSupervisorService.Models.Request.ViolationReportRequest;
using StudentSupervisorService.Models.Request.ViolationRequest;
using StudentSupervisorService.Models.Request.ViolationTypeRequest;
using StudentSupervisorService.Models.Request.YearPackageRequest;
using StudentSupervisorService.Models.Response.TimeResponse;
using StudentSupervisorService.Models.Response.UserResponse;
using StudentSupervisorService.Models.Response.ViolationConfigResponse;
using StudentSupervisorService.Models.Response.ViolationGroupResponse;
using StudentSupervisorService.Models.Response.ViolationReportResponse;
using StudentSupervisorService.Models.Response.ViolationResponse;
using StudentSupervisorService.Models.Response.ViolationTypeResponse;
using StudentSupervisorService.Models.Response.YearPackageResponse;
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


            CreateMap<Violation, ResponseOfViolation>()
               .ForMember(re => re.ViolationName, act => act.MapFrom(src => src.Name));

            CreateMap<RequestOfCreateViolation, Violation>()
                .ForMember(re => re.Name, act => act.MapFrom(src => src.ViolationName));
            CreateMap<RequestOfUpdateViolation, Violation>()
                .ForMember(re => re.Name, act => act.MapFrom(src => src.ViolationName));

            CreateMap<ViolationConfig, ViolationConfigResponse>()
               .ForMember(re => re.ViolationTypeName, act => act.MapFrom(src => src.ViolationType.Name))
               .ForMember(re => re.ViolationConfigName, act => act.MapFrom(src => src.Name));

            CreateMap<RequestOfViolationConfig, ViolationConfig>()
                .ForMember(re => re.Name, act => act.MapFrom(src => src.ViolationConfigName));

            CreateMap<ViolationGroup, ResponseOfVioGroup>()
              .ForMember(re => re.VioGroupName, act => act.MapFrom(src => src.Name));

            CreateMap<RequestOfVioGroup, ViolationGroup>();

            CreateMap<ViolationReport, ResponseOfVioReport>()
              .ForMember(re => re.EnrollDate, act => act.MapFrom(src => src.StudentInClass.EnrollDate))
              .ForMember(re => re.ViolationName, act => act.MapFrom(src => src.Violation.Name));

            CreateMap<RequestOfVioReport, ViolationReport>();

            CreateMap<ViolationType, ResponseOfVioType>()
              .ForMember(re => re.VioTypeName, act => act.MapFrom(src => src.Name))
              .ForMember(re => re.VioGroupName, act => act.MapFrom(src => src.ViolationGroup.Name));

            CreateMap<RequestOfVioType, ViolationType>()
                .ForMember(re => re.Name, act => act.MapFrom(src => src.VioTypeName));

            CreateMap<YearPackage, ResponseOfYearPackage>()
              .ForMember(re => re.Year, act => act.MapFrom(src => src.SchoolYear.Year))
              .ForMember(re => re.PackageName, act => act.MapFrom(src => src.Package.Name));

            CreateMap<RequestOfYearPackage, YearPackage>();
            //--------------------------------------------------------------------------------------------------------------

        }
    }
}
