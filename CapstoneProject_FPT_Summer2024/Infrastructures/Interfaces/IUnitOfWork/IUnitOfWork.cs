using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IClassGroupRepository ClassGroup {  get; }
        IClassReposirory Class {  get; }
        IDisciplineRepository Discipline {  get; }
        IEvaluationDetailRepository EvaluationDetail { get; }
        IEvaluationRepository Evaluation {  get; }
        IImageUrlRepository ImageUrl { get; }
        IPackageRepository Package { get; }
        IPatrolScheduleRepository PatrolSchedule { get; }
        IPenaltyRepository Penalty { get; }
        IRegisteredSchoolRepository RegisteredSchool { get; }
        ISchoolAdminRepository SchoolAdmin { get; }
        ISchoolConfigRepository SchoolConfig { get; }
        IHighSchoolRepository HighSchool { get; }
        ISchoolYearRepository SchoolYear { get; }
        IStudentInClassRepository StudentInClass { get; }
        IStudentRepository Student {  get; }
        IStudentSupervisorRepository StudentSupervisor { get; }
        ITeacherRepository Teacher { get; }
        ITimeRepository Time {  get; }
        IUserRepository User { get; }
        IViolationConfigRepository ViolationConfig { get; }
        IViolationGroupRepository ViolationGroup { get; }
        IViolationReportRepository ViolationReport { get; }
        IViolationRepository Violation { get; }
        IViolationTypeRepository ViolationType { get; }
        IYearPackageRepository YearPackage { get; }

        IDbContextTransaction StartTransaction(string name);
        void StopTransaction(IDbContextTransaction commit);
        void RollBack(IDbContextTransaction commit, string name);
        int Save();
    }
}
