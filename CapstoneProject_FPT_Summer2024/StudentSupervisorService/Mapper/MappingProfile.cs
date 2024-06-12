using AutoMapper;
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
using StudentSupervisorService.Models.Response.TeacherResponse;
using StudentSupervisorService.Models.Request.TeacherRequest;

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

            //------------------------------------------------------------------------------------------------------------       
            CreateMap<SchoolYear, ResponseOfSchoolYear>()
               .ForMember(re => re.SchoolName, act => act.MapFrom(src => src.School.Name));

            CreateMap<RequestCreateSchoolYear, SchoolYear>();

            CreateMap<Teacher, TeacherResponse>()
               .ForMember(re => re.Code, act => act.MapFrom(src => src.User.Code))
               .ForMember(re => re.TeacherName, act => act.MapFrom(src => src.User.Name))
               .ForMember(re => re.SchoolName, act => act.MapFrom(src => src.School.Name))
               .ForMember(re => re.Phone, act => act.MapFrom(src => src.User.Phone))
               .ForMember(re => re.Password, act => act.MapFrom(src => src.User.Password))
               .ForMember(re => re.Address, act => act.MapFrom(src => src.User.Address))
               .ForMember(re => re.RoleId, act => act.MapFrom(src => src.User.RoleId));

            CreateMap<RequestOfTeacher, Teacher>()
                .ForPath(re => re.User.SchoolAdminId, act => act.MapFrom(src => src.SchoolAdminId))
                .ForPath(re => re.User.Code, act => act.MapFrom(src => src.Code))
                .ForPath(re => re.User.Name, act => act.MapFrom(src => src.TeacherName))
                .ForPath(re => re.User.Phone, act => act.MapFrom(src => "84" + src.Phone)) // Prefix "84" to Phone
                .ForPath(re => re.User.Password, act => act.MapFrom(src => src.Password))
                .ForPath(re => re.User.Address, act => act.MapFrom(src => src.Address));
            //.ForPath(re => re.User.RoleId, act => act.MapFrom(src => src.RoleId));
            //.ForPath(re => re.User.Status, act => act.MapFrom(src => src.Status));

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
            //-------------------------------------------------------------------------------------------------------------
        }
    }
}
