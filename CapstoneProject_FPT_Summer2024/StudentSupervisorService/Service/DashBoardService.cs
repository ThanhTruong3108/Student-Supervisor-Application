using Domain.Entity.DTO;
using StudentSupervisorService.Models.Response.ViolationResponse;
using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service
{
    public interface DashBoardService
    {
        Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByMonthAndWeek(int schoolId, short year, int month, int? weekNumber = null);
        Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByYearAndClassName(int schoolId, short year, string className);
        Task<DataResponse<List<ViolationTypeSummary>>> GetTopFrequentViolations(int schoolId, short year);
        Task<DataResponse<List<ClassViolationSummary>>> GetClassesWithMostViolations(int schoolId, short year, int month, int? weekNumber = null);
        Task<DataResponse<List<StudentViolationCount>>> GetTop5StudentsWithMostViolations(int schoolId, short year);
        Task<DataResponse<List<ClassViolationDetail>>> GetClassWithMostStudentViolations(int schoolId, short year, int month, int? weekNumber = null);
    }
}
