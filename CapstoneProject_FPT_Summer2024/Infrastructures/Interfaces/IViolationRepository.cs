using Domain.Entity;
using Domain.Entity.DTO;
using Infrastructures.Interfaces.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Interfaces
{
    public interface IViolationRepository : IGenericRepository<Violation>
    {
        Task<List<Violation>> GetAllViolations();
        Task<Violation> GetByIdWithImages(int id);
        Task<List<Violation>> GetViolationsByClassId(int classId);
        Task<Violation> GetViolationById(int id);
        Task<Violation> CreateViolation(Violation violationEntity);
        Task<Violation> UpdateViolation(Violation violationEntity);
        Task<List<Violation>> GetApprovedViolationsOver1Day();
        Task AcceptMultipleViolations(List<Violation> violations);
        Task DeleteViolation(int id);
        Task<List<Violation>> GetViolationsByStudentId(int studentId);
        Task<List<Violation>> GetViolationsByStudentIdAndYear(int studentId, int schoolYearId);
        Task<Dictionary<int, int>> GetViolationCountByYear(int studentId);
        Task<List<Violation>> GetViolationsBySchoolId(int schoolId);

        Task<List<Violation>> GetViolationsByMonthAndWeek(int schoolId, short year, int? month = null, int? weekNumber = null);
        Task<List<Violation>> GetViolationsByYearAndClassName(int schoolId, short year, string className, int? month = null, int? weekNumber = null);
        Task<List<ViolationTypeSummary>> GetTopFrequentViolations(int schoolId, short year, int? month = null, int? weekNumber = null);
        Task<List<ClassViolationSummary>> GetClassesWithMostViolations(int schoolId, short year, int? month = null, int? weekNumber = null);
        Task<List<StudentViolationCount>> GetTop5StudentsWithMostViolations(int schoolId, short year, int? month = null, int? weekNumber = null);
        Task<List<ClassViolationDetail>> GetClassWithMostStudentViolations(int schoolId, short year, int? month = null, int? weekNumber = null);
        Task<List<Violation>> GetViolationsByUserId(int userId);
        Task<Violation> GetViolationByDisciplineId(int disciplineId);
        Task<List<Violation>> GetViolationsByUserRoleStudentSupervisor(int userId);
        Task<List<Violation>> GetViolationsByUserRoleSupervisor(int userId);
        Task<List<Violation>> GetViolationsBySupervisorUserId(int userId);
        Task<int> CountViolations(int schoolId, short year, int? month = null, int? weekNumber = null);
    }
}