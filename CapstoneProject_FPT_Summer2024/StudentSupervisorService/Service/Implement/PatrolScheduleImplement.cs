using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.PatrolScheduleRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ClassResponse;
using StudentSupervisorService.Models.Response.DisciplineResponse;
using StudentSupervisorService.Models.Response.PatrolScheduleResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service.Implement
{
    public class PatrolScheduleImplement : PatrolScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatrolScheduleImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<List<PatrolScheduleResponse>>> GetAllPatrolSchedules(string sortOrder)
        {
            var response = new DataResponse<List<PatrolScheduleResponse>>();
            try
            {

                var pScheduleEntities = await _unitOfWork.PatrolSchedule.GetAllPatrolSchedules();
                if (pScheduleEntities is null || !pScheduleEntities.Any())
                {
                    response.Message = "The PatrolSchedule list is empty";
                    response.Success = true;
                    return response;
                }

                pScheduleEntities = sortOrder == "desc"
                    ? pScheduleEntities.OrderByDescending(r => r.ScheduleId).ToList()
                    : pScheduleEntities.OrderBy(r => r.ScheduleId).ToList();

                response.Data = _mapper.Map<List<PatrolScheduleResponse>>(pScheduleEntities);
                response.Message = "List PatrolSchedule";
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

        public async Task<DataResponse<PatrolScheduleResponse>> GetPatrolScheduleById(int id)
        {
            var response = new DataResponse<PatrolScheduleResponse>();
            try
            {
                var pScheduleEntity = await _unitOfWork.PatrolSchedule.GetPatrolScheduleById(id);
                if (pScheduleEntity == null)
                {
                    response.Message = "PatrolSchedule not found";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<PatrolScheduleResponse>(pScheduleEntity);
                response.Message = "Found a PatrolSchedule";
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

        public async Task<DataResponse<List<PatrolScheduleResponse>>> SearchPatrolSchedules(int? classId, int? supervisorId, int? teacherId, DateTime? from, DateTime? to, string sortOrder)
        {
            var response = new DataResponse<List<PatrolScheduleResponse>>();

            try
            {
                var pScheduleEntities = await _unitOfWork.PatrolSchedule.SearchPatrolSchedules(classId, supervisorId, teacherId, from, to);
                if (pScheduleEntities is null || pScheduleEntities.Count == 0)
                {
                    response.Message = "No PatrolSchedule matches the search criteria";
                    response.Success = true;
                }
                else
                {
                    if (sortOrder == "desc")
                    {
                        pScheduleEntities = pScheduleEntities.OrderByDescending(r => r.ScheduleId).ToList();
                    }
                    else
                    {
                        pScheduleEntities = pScheduleEntities.OrderBy(r => r.ScheduleId).ToList();
                    }
                    response.Data = _mapper.Map<List<PatrolScheduleResponse>>(pScheduleEntities);
                    response.Message = "List PatrolSchedule";
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

        public async Task<DataResponse<PatrolScheduleResponse>> CreatePatrolSchedule(PatrolScheduleCreateRequest request)
        {
            var response = new DataResponse<PatrolScheduleResponse>();
            try
            {
                var pScheduleEntity = new PatrolSchedule
                {
                    ClassId = request.ClassId,
                    SupervisorId = request.SupervisorId,
                    TeacherId = request.TeacherId,
                    From = request.From,
                    To = request.To,
                };

                var created = await _unitOfWork.PatrolSchedule.CreatePatrolSchedule(pScheduleEntity);

                response.Data = _mapper.Map<PatrolScheduleResponse>(created);
                response.Message = "PatrolSchedule created successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Create PatrolSchedule failed: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<PatrolScheduleResponse>> UpdatePatrolSchedule(PatrolScheduleUpdateRequest request)
        {
            var response = new DataResponse<PatrolScheduleResponse>();
            try
            {
                var existingPatrolSchedule = await _unitOfWork.PatrolSchedule.GetPatrolScheduleById(request.ScheduleId);
                if (existingPatrolSchedule == null)
                {
                    response.Data = "Empty";
                    response.Message = "PatrolSchedule not found";
                    response.Success = false;
                    return response;
                }

                existingPatrolSchedule.ClassId = request.ClassId ?? existingPatrolSchedule.ClassId;
                existingPatrolSchedule.SupervisorId = request.SupervisorId ?? existingPatrolSchedule.SupervisorId;
                existingPatrolSchedule.TeacherId = request.TeacherId ?? existingPatrolSchedule.TeacherId;
                existingPatrolSchedule.From = request.From ?? existingPatrolSchedule.From;
                existingPatrolSchedule.To = request.To ?? existingPatrolSchedule.To;

                await _unitOfWork.PatrolSchedule.UpdatePatrolSchedule(existingPatrolSchedule);

                response.Data = _mapper.Map<PatrolScheduleResponse>(existingPatrolSchedule);
                response.Message = "PatrolSchedule updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Update PatrolSchedule failed: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<PatrolScheduleResponse>> DeletePatrolSchedule(int id)
        {
            var response = new DataResponse<PatrolScheduleResponse>();
            try
            {
                var existingPSchedule = await _unitOfWork.PatrolSchedule.GetPatrolScheduleById(id);
                if (existingPSchedule == null)
                {
                    response.Data = "Empty";
                    response.Message = "PatrolSchedule not found";
                    response.Success = false;
                    return response;
                }
                await _unitOfWork.PatrolSchedule.DeletePatrolSchedule(id);
                response.Data = "Empty";
                response.Message = "PatrolSchedule deleted successfully";
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
