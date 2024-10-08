﻿using AutoMapper;
using Domain.Entity;
using StudentSupervisorService.Models.Request.HighSchoolRequest;
using StudentSupervisorService.Models.Request.SchoolYearRequest;
using StudentSupervisorService.Models.Response.ClassGroupResponse;
using StudentSupervisorService.Models.Response.ClassResponse;
using StudentSupervisorService.Models.Response.HighschoolResponse;
using StudentSupervisorService.Models.Response.SchoolYearResponse;
using StudentSupervisorService.Models.Response.StudentResponse;
using StudentSupervisorService.Models.Request.UserRequest;
using StudentSupervisorService.Models.Request.ViolationConfigRequest;
using StudentSupervisorService.Models.Request.ViolationGroupRequest;
using StudentSupervisorService.Models.Request.ViolationRequest;
using StudentSupervisorService.Models.Request.ViolationTypeRequest;
using StudentSupervisorService.Models.Request.YearPackageRequest;
using StudentSupervisorService.Models.Response.UserResponse;
using StudentSupervisorService.Models.Response.ViolationConfigResponse;
using StudentSupervisorService.Models.Response.ViolationGroupResponse;
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
using StudentSupervisorService.Models.Response.PenaltyResponse;
using StudentSupervisorService.Models.Response.RegisteredSchoolResponse;
using StudentSupervisorService.Models.Response.DisciplineResponse;
using StudentSupervisorService.Models.Response.EvaluationResponse;
using StudentSupervisorService.Models.Response.StudentInClassResponse;
using StudentSupervisorService.Models.Response.StudentSupervisorResponse;
using StudentSupervisorService.Models.Request.StudentSupervisorRequest;
using StudentSupervisorService.Models.Request.PackageRequest;
using StudentSupervisorService.Models.Response.PackageResponse;
using StudentSupervisorService.Models.Response.PatrolScheduleResponse;
using StudentSupervisorService.Models.Response.AdminResponse;
using StudentSupervisorService.Models.Response.OrderResponse;
using StudentSupervisorService.Models.Request.OrderRequest;
using StudentSupervisorService.Models.Response.SemesterResponse;
using StudentSupervisorService.Models.Request.SemesterRequest;

namespace StudentSupervisorService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Admin, AdminResponse>();
            CreateMap<HighSchool, ResponseOfHighSchool>();
            CreateMap<Class, ClassResponse>()
                .ForMember(re => re.Year, act => act.MapFrom(src => src.SchoolYear.Year))
                .ForMember(re => re.TeacherName, act => act.MapFrom(src => src.Teacher.User.Name));

            CreateMap<ClassGroup, ClassGroupResponse>()
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Classes.Any() ? src.Classes.Max(c => c.Grade) : 0));
            CreateMap<Student, StudentResponse>();
            CreateMap<RequestOfHighSchool, HighSchool>();
            CreateMap<Penalty, PenaltyResponse>();
            CreateMap<RegisteredSchool, RegisteredSchoolResponse>()
                .ForMember(re => re.Status, act => act.MapFrom(src => src.Status))
                .ForMember(re => re.SchoolCode, act => act.MapFrom(src => src.School.Code))
                .ForMember(re => re.SchoolName, act => act.MapFrom(src => src.School.Name))
                .ForMember(re => re.City, act => act.MapFrom(src => src.School.City))
                .ForMember(re => re.Address, act => act.MapFrom(src => src.School.Address))
                .ForMember(re => re.Phone, act => act.MapFrom(src => src.School.Phone))
                .ForMember(re => re.WebURL, act => act.MapFrom(src => src.School.WebUrl));

            CreateMap<Discipline, DisciplineResponse>()
                .ForMember(re => re.Year, act => act.MapFrom(src => src.Violation.Class.SchoolYear.Year))
                .ForMember(re => re.PenaltyName, act => act.MapFrom(src => src.Pennalty.Name))
                .ForMember(re => re.StudentCode, act => act.MapFrom(src => src.Violation.StudentInClass.Student.Code))
                .ForMember(re => re.StudentName, act => act.MapFrom(src => src.Violation.StudentInClass.Student.Name));

            CreateMap<Evaluation, EvaluationResponse>()
                .ForMember(re => re.Year, act => act.MapFrom(src => src.Class.SchoolYear.Year));
            CreateMap<StudentInClass, StudentInClassResponse>()
                .ForMember(re => re.Grade, act => act.MapFrom(src => src.Class.Grade))
                .ForMember(re => re.StudentCode, act => act.MapFrom(src => src.Student.Code))
                .ForMember(re => re.StudentName, act => act.MapFrom(src => src.Student.Name))
                .ForMember(re => re.Sex, act => act.MapFrom(src => src.Student.Sex))
                .ForMember(re => re.Birthday, act => act.MapFrom(src => src.Student.Birthday))
                .ForMember(re => re.Address, act => act.MapFrom(src => src.Student.Address))
                .ForMember(re => re.Year, act => act.MapFrom(src => src.Class.SchoolYear.Year))
                .ForMember(re => re.Phone, act => act.MapFrom(src => src.Student.Phone));


            //------------------------------------------------------------------------------------------------------------       
            CreateMap<OrderResponse, Order>();
            CreateMap<OrderCreateRequest, Order>();
            CreateMap<OrderUpdateRequest, Order>();
            CreateMap<OrderUpdateRequest, OrderResponse>();
            CreateMap<OrderResponse, OrderUpdateRequest>();
            CreateMap<Order, OrderResponse>()
                .ForMember(re => re.PackageName, act => act.MapFrom(src => src.Package.Name))
                .ForMember(re => re.SchoolName, act => act.MapFrom(src => src.User.School.Name));

            CreateMap<SchoolYear, ResponseOfSchoolYear>()
               .ForMember(re => re.SchoolName, act => act.MapFrom(src => src.School.Name));

            CreateMap<RequestCreateSchoolYear, SchoolYear>();

            CreateMap<Semester, ResponseOfSemester>()
              .ForMember(re => re.Year, act => act.MapFrom(src => src.SchoolYear.Year));

            CreateMap<RequestOfSemester, Semester>();

            CreateMap<Package, ResponseOfPackage>();

            CreateMap<PackageRequest, Package>();

            CreateMap<StudentSupervisor, StudentSupervisorResponse>()
               .ForMember(re => re.IsSupervisor, act => act.MapFrom(src => src.StudentInClass.IsSupervisor))
               .ForMember(re => re.SchoolId, act => act.MapFrom(src => src.User.SchoolId))
               .ForMember(re => re.Code, act => act.MapFrom(src => src.User.Code))
               .ForMember(re => re.SupervisorName, act => act.MapFrom(src => src.User.Name))
               .ForMember(re => re.Phone, act => act.MapFrom(src => src.User.Phone))
               .ForMember(re => re.Password, act => act.MapFrom(src => src.User.Password))
               .ForMember(re => re.Address, act => act.MapFrom(src => src.User.Address))
               .ForMember(re => re.Year, act => act.MapFrom(src => src.StudentInClass.Class.SchoolYear.Year))
               .ForMember(re => re.RoleId, act => act.MapFrom(src => src.User.RoleId))
               .ForMember(re => re.Status, act => act.MapFrom(src => src.User.Status));

            CreateMap<StudentSupervisorRequest, StudentSupervisor>()
                .ForPath(re => re.User.SchoolId, act => act.MapFrom(src => src.SchoolId))
                .ForPath(re => re.User.Code, act => act.MapFrom(src => src.Code))
                .ForPath(re => re.User.Name, act => act.MapFrom(src => src.SupervisorName))
                .ForPath(re => re.User.Phone, act => act.MapFrom(src => src.Phone))
                .ForPath(re => re.User.Password, act => act.MapFrom(src => src.Password))
                .ForPath(re => re.User.Address, act => act.MapFrom(src => src.Address));


            CreateMap<Teacher, TeacherResponse>()
               .ForMember(re => re.Code, act => act.MapFrom(src => src.User.Code))
               .ForMember(re => re.TeacherName, act => act.MapFrom(src => src.User.Name))
               .ForMember(re => re.SchoolName, act => act.MapFrom(src => src.School.Name))
               .ForMember(re => re.Phone, act => act.MapFrom(src => src.User.Phone))
               .ForMember(re => re.Password, act => act.MapFrom(src => src.User.Password))
               .ForMember(re => re.Address, act => act.MapFrom(src => src.User.Address))
               .ForMember(re => re.RoleId, act => act.MapFrom(src => src.User.RoleId))
               .ForMember(re => re.Status, act => act.MapFrom(src => src.User.Status));

            CreateMap<RequestOfTeacher, Teacher>()
                .ForPath(re => re.User.Code, act => act.MapFrom(src => src.Code))
                .ForPath(re => re.User.Name, act => act.MapFrom(src => src.TeacherName))
                .ForPath(re => re.User.Phone, act => act.MapFrom(src => src.Phone))
                .ForPath(re => re.User.Password, act => act.MapFrom(src => src.Password))
                .ForPath(re => re.User.Address, act => act.MapFrom(src => src.Address));


            CreateMap<User, ResponseOfUser>()
                .ForMember(re => re.SchoolName, act => act.MapFrom(src => src.School.Name))
                .ForMember(re => re.UserName, act => act.MapFrom(src => src.Name))
                .ForMember(re => re.Phone, act => act.MapFrom(src => src.Phone))
               .ForMember(re => re.RoleName, act => act.MapFrom(src => src.Role.RoleName));

            CreateMap<RequestOfUser, User>();


            CreateMap<Violation, ResponseOfViolation>()
               .ForMember(re => re.ScheduleName, act => act.MapFrom(src => src.Schedule.Name))
               .ForMember(re => re.ClassCode, act => act.MapFrom(src => src.Class.Code))
               .ForMember(re => re.ClassName, act => act.MapFrom(src => src.Class.Name))
               .ForMember(re => re.Year, act => act.MapFrom(src => src.Class.SchoolYear.Year))
               .ForMember(re => re.CreatedBy, act => act.MapFrom(src => src.User.Name))
               .ForMember(re => re.ViolationName, act => act.MapFrom(src => src.Name))
               .ForMember(re => re.ViolationTypeName, act => act.MapFrom(src => src.ViolationType.Name))
               .ForMember(re => re.ViolationGroupId, act => act.MapFrom(src => src.ViolationType.ViolationGroupId))
               .ForMember(re => re.ViolationGroupName, act => act.MapFrom(src => src.ViolationType.ViolationGroup.Name))
               .ForMember(dest => dest.StudentInClassId, opt => opt.MapFrom(
                   src => src.StudentInClass.StudentInClassId))
               .ForMember(dest => dest.StudentName, opt => opt.MapFrom(
                   src => src.StudentInClass.Student.Name))
               .ForMember(dest => dest.StudentCode, opt => opt.MapFrom(
                   src => src.StudentInClass.Student.Code))
               .ForMember(dest => dest.ViolationGroupName, opt => opt.MapFrom(
                   src => src.ViolationType.ViolationGroup.Name))
               .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.ImageUrls.Select(img => img.Url)));

            CreateMap<PatrolSchedule, PatrolScheduleResponse>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Name))
                .ForMember(dest => dest.SupervisorName, opt => opt.MapFrom(src => src.Supervisor.User.Name))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Class.SchoolYear.Year))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));

            CreateMap<RequestOfStuSupervisorCreateViolation, Violation>()
                .ForMember(re => re.Name, act => act.MapFrom(src => src.ViolationName));
            CreateMap<RequestOfSupervisorCreateViolation, Violation>()
                .ForMember(re => re.Name, act => act.MapFrom(src => src.ViolationName));
            CreateMap<RequestOfUpdateViolationForStudentSupervisor, Violation>()
                .ForMember(re => re.Name, act => act.MapFrom(src => src.ViolationName));
            CreateMap<RequestOfUpdateViolationForSupervisor, Violation>()
                .ForMember(re => re.Name, act => act.MapFrom(src => src.ViolationName));

            CreateMap<ViolationConfig, ViolationConfigResponse>()
               .ForMember(re => re.ViolationTypeName, act => act.MapFrom(src => src.ViolationType.Name));

            CreateMap<RequestOfViolationConfig, ViolationConfig>();

            CreateMap<ViolationGroup, ResponseOfVioGroup>()
                 .ForMember(re => re.SchoolName, act => act.MapFrom(src => src.School.Name))
              .ForMember(re => re.VioGroupCode, act => act.MapFrom(src => src.Code))
              .ForMember(re => re.VioGroupName, act => act.MapFrom(src => src.Name));

            CreateMap<RequestOfVioGroup, ViolationGroup>();


            CreateMap<ViolationType, ResponseOfVioType>()
              .ForMember(re => re.VioTypeName, act => act.MapFrom(src => src.Name))
              .ForMember(re => re.VioGroupName, act => act.MapFrom(src => src.ViolationGroup.Name));

            CreateMap<RequestOfVioType, ViolationType>()
            .ForMember(dest => dest.ViolationGroupId, act => act.MapFrom(src => src.ViolationGroupId))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.VioTypeName))
            .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description));

            CreateMap<YearPackage, ResponseOfYearPackage>()
              .ForMember(re => re.SchoolName, act => act.MapFrom(src => src.SchoolYear.School.Name))
              .ForMember(re => re.Code, act => act.MapFrom(src => src.SchoolYear.School.Code))
              .ForMember(re => re.Year, act => act.MapFrom(src => src.SchoolYear.Year))
              .ForMember(re => re.PackageName, act => act.MapFrom(src => src.Package.Name));

            CreateMap<RequestOfYearPackage, YearPackage>();
            //-------------------------------------------------------------------------------------------------------------
        }
    }
}
