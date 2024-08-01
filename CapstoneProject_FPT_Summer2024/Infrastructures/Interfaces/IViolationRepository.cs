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
        Task<Violation> GetViolationById(int id);
        Task<Violation> CreateViolation(Violation violationEntity);
        Task<Violation> UpdateViolation(Violation violationEntity);
        Task DeleteViolation(int id);
        Task<List<Violation>> GetViolationsByStudentId(int studentId);
        Task<List<Violation>> GetViolationsByStudentIdAndYear(int studentId, int schoolYearId);
        Task<Dictionary<int, int>> GetViolationCountByYear(int studentId);
        Task<List<Violation>> GetViolationsBySchoolId(int schoolId);

        Task<List<Violation>> GetViolationsByMonthAndWeek(int schoolId, short year, int month, int? weekNumber = null);
        Task<List<Violation>> GetViolationsByYearAndClassName(int schoolId, short year, string className);
        Task<List<ViolationTypeSummary>> GetTopFrequentViolations(int schoolId, short year);
        Task<List<ClassViolationSummary>> GetClassesWithMostViolations(int schoolId, short year, int month, int? weekNumber = null);
        Task<List<StudentViolationCount>> GetTop5StudentsWithMostViolations(int schoolId, short year);
        Task<List<ClassViolationDetail>> GetClassWithMostStudentViolations(int schoolId, short year, int month, int? weekNumber = null);
        Task<List<Violation>> GetViolationsByUserId(int userId);
    }
}