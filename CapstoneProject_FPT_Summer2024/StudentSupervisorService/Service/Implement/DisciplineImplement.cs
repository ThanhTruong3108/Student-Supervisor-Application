using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.DisciplineRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ClassGroupResponse;
using StudentSupervisorService.Models.Response.DisciplineResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service.Implement
{
    public class DisciplineImplement : DisciplineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DisciplineImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DataResponse<List<DisciplineResponse>>> GetAllDisciplines(string sortOrder)
        {
            var response = new DataResponse<List<DisciplineResponse>>();
            try
            {

                var disciplineEntities = await _unitOfWork.Discipline.GetAllDisciplines();
                if (disciplineEntities is null || !disciplineEntities.Any())
                {
                    response.Message = "The Discipline list is empty";
                    response.Success = true;
                    return response;
                }

                disciplineEntities = sortOrder == "desc"
                    ? disciplineEntities.OrderByDescending(r => r.DisciplineId).ToList()
                    : disciplineEntities.OrderBy(r => r.DisciplineId).ToList();

                response.Data = _mapper.Map<List<DisciplineResponse>>(disciplineEntities);
                response.Message = "List Discipline";
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<DisciplineResponse>> GetDisciplineById(int id)
        {
            var response = new DataResponse<DisciplineResponse>();
            try
            {
                var disciplineEntity = await _unitOfWork.Discipline.GetDisciplineById(id);
                if (disciplineEntity == null)
                {
                    response.Message = "Discipline not found";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<DisciplineResponse>(disciplineEntity);
                response.Message = "Found a Discipline";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<DisciplineResponse>>> SearchDisciplines(int? violationId, int? penaltyId, string? description, DateTime? startDate, DateTime? endDate, string? status, string sortOrder)
        {
            var response = new DataResponse<List<DisciplineResponse>>();

            try
            {
                var disciplineEntities = await _unitOfWork.Discipline.SearchDisciplines(violationId, penaltyId, description, startDate, endDate, status);
                if (disciplineEntities is null || disciplineEntities.Count == 0)
                {
                    response.Message = "No Discipline matches the search criteria";
                    response.Success = true;
                }
                else
                {
                    if (sortOrder == "desc")
                    {
                        disciplineEntities = disciplineEntities.OrderByDescending(r => r.DisciplineId).ToList();
                    }
                    else
                    {
                        disciplineEntities = disciplineEntities.OrderBy(r => r.DisciplineId).ToList();
                    }
                    response.Data = _mapper.Map<List<DisciplineResponse>>(disciplineEntities);
                    response.Message = "List Discipline";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<DisciplineResponse>> CreateDiscipline(DisciplineCreateRequest request)
        {
            var response = new DataResponse<DisciplineResponse>();
            try
            {
                var disciplineEntity = new Discipline
                {
                    ViolationId = request.ViolationId,
                    PennaltyId = request.PennaltyId,
                    Description = request.Description,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Status = DisciplineStatusEnums.ACTIVE.ToString()
                };

                var created = await _unitOfWork.Discipline.CreateDiscipline(disciplineEntity);

                response.Data = _mapper.Map<DisciplineResponse>(created);
                response.Message = "Discipline created successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Create Discipline failed: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }
        public async Task<DataResponse<DisciplineResponse>> UpdateDiscipline(DisciplineUpdateRequest request)
        {
            var response = new DataResponse<DisciplineResponse>();
            try
            {
                var existingDiscipline = await _unitOfWork.Discipline.GetDisciplineById(request.DisciplineId);
                if (existingDiscipline == null)
                {
                    response.Data = "Empty";
                    response.Message = "Discipline not found";
                    response.Success = false;
                    return response;
                }

                existingDiscipline.ViolationId = request.ViolationId ?? existingDiscipline.ViolationId;
                existingDiscipline.PennaltyId = request.PennaltyId ?? existingDiscipline.PennaltyId;
                existingDiscipline.Description = request.Description ?? existingDiscipline.Description;
                existingDiscipline.StartDate = request.StartDate ?? existingDiscipline.StartDate;
                existingDiscipline.EndDate = request.EndDate ?? existingDiscipline.EndDate;
                existingDiscipline.Status = request.Status ?? existingDiscipline.Status;

                await _unitOfWork.Discipline.UpdateDiscipline(existingDiscipline);

                response.Data = _mapper.Map<DisciplineResponse>(existingDiscipline);
                response.Message = "Discipline updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Update Discipline failed: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<DisciplineResponse>> DeleteDiscipline(int id)
        {
            var response = new DataResponse<DisciplineResponse>();
            try
            {
                var existingDiscipline = await _unitOfWork.Discipline.GetDisciplineById(id);
                if (existingDiscipline == null)
                {
                    response.Data = "Empty";
                    response.Message = "Discipline not found";
                    response.Success = false;
                    return response;
                }
                await _unitOfWork.Discipline.DeleteDiscipline(id);
                response.Data = "Empty";
                response.Message = "Discipline deleted successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        } 
    }
}
