﻿using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ViolationResponse;
using StudentSupervisorService.Models.Request.ViolationRequest;

namespace StudentSupervisorService.Service
{
    public interface ViolationService
    {
        Task<DataResponse<List<ResponseOfViolation>>> GetAllViolations(string sortOrder);
        Task<DataResponse<ResponseOfViolation>> GetViolationById(int id);
        Task<DataResponse<ResponseOfViolation>> CreateViolationForStudentSupervisor(RequestOfCreateViolation request);
        Task<DataResponse<ResponseOfViolation>> CreateViolationForSupervisor(RequestOfCreateViolation request);
        Task<DataResponse<ResponseOfViolation>> ApproveViolation(int id);
        Task<DataResponse<ResponseOfViolation>> RejectViolation(int id);
        Task<DataResponse<ResponseOfViolation>> DeleteViolation(int id);
        Task<DataResponse<ResponseOfViolation>> UpdateViolation(RequestOfUpdateViolation request);
        Task<DataResponse<List<ResponseOfViolation>>> SearchViolations(
                int? classId,
                int? violationTypeId,
                int? studentInClassId,
                int? teacherId,
                string? name,
                string? description,
                DateTime? date,
                string? status,
                string sortOrder);
    }
}
