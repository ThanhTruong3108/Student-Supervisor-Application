using StudentSupervisorService.Models.Request.DisciplineRequest;
using StudentSupervisorService.Models.Response.DisciplineResponse;
using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service
{
    public interface DisciplineService
    {
        Task<DataResponse<List<DisciplineResponse>>> GetAllDisciplines(string sortOrder);
        Task<DataResponse<DisciplineResponse>> GetDisciplineById(int id);
        Task<DataResponse<List<DisciplineResponse>>> SearchDisciplines(
            int? violationId, 
            int? penaltyId, 
            string? code, 
            string? name, 
            string? description, 
            DateTime? startDate, 
            DateTime? endDate, 
            string? status, 
            string sortOrder);
        Task<DataResponse<DisciplineResponse>> CreateDiscipline(DisciplineCreateRequest request);
        Task<DataResponse<DisciplineResponse>> UpdateDiscipline(DisciplineUpdateRequest request);
        Task<DataResponse<DisciplineResponse>> DeleteDiscipline(int id);
    }
}
