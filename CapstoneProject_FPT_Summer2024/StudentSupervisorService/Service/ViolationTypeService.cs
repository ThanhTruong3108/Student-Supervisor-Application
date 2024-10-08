﻿using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ViolationTypeResponse;
using StudentSupervisorService.Models.Request.ViolationTypeRequest;

namespace StudentSupervisorService.Service
{
    public interface ViolationTypeService
    {
        Task<DataResponse<List<ResponseOfVioType>>> GetAllVioTypes(string sortOrder);
        Task<DataResponse<ResponseOfVioType>> GetVioTypeById(int id);
        Task<DataResponse<ResponseOfVioType>> CreateVioType(RequestOfVioType request);
        Task<DataResponse<ResponseOfVioType>> DeleteVioType(int id);
        Task<DataResponse<ResponseOfVioType>> UpdateVioType(int id, RequestOfVioType request);
        Task<DataResponse<List<ResponseOfVioType>>> GetViolationTypesBySchoolId(int schoolId);
        Task<DataResponse<List<ResponseOfVioType>>> GetActiveViolationTypesBySchoolId(int schoolId);
        Task<DataResponse<List<ResponseOfVioType>>> GetViolationTypesByViolationGroupId(int violationGroupId);
        Task<DataResponse<List<ResponseOfVioType>>> GetViolationTypesByGroupForStudentSupervisor(int violationGroupId);
    }
}
