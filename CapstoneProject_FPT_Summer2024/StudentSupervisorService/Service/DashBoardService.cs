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
        Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByYearAndClassName(short year, string className, int schoolId);
        Task<DataResponse<List<ViolationTypeSummary>>> GetTopFrequentViolations(short year, int schoolId);
        Task<DataResponse<List<ClassViolationSummary>>> GetClassesWithMostViolations(short year, int schoolId);
        Task<DataResponse<List<StudentViolationCount>>> GetTop5StudentsWithMostViolations(short year, int schoolId);
        Task<DataResponse<List<ClassViolationDetail>>> GetClassWithMostStudentViolations(short year, int schoolId);
    }
}
