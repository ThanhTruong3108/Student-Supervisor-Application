﻿using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.PatrolScheduleRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.PatrolScheduleResponse;
using StudentSupervisorService.Models.Response.ViolationResponse;
using System.Security.Cryptography;

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
                    response.Message = "Danh sách Lịch tuần tra trống!!";
                    response.Success = true;
                    return response;
                }

                pScheduleEntities = sortOrder == "desc"
                    ? pScheduleEntities.OrderByDescending(r => r.ScheduleId).ToList()
                    : pScheduleEntities.OrderBy(r => r.ScheduleId).ToList();

                response.Data = _mapper.Map<List<PatrolScheduleResponse>>(pScheduleEntities);
                response.Message = "Danh sách các lịch tuần tra";
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
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
                    response.Message = "Không tìm thấy Lịch tuần tra!!";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<PatrolScheduleResponse>(pScheduleEntity);
                response.Message = "Đã tìm thấy Lịch tuần tra.";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
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
                    UserId = request.UserId,
                    SupervisorId = request.SupervisorId,
                    Name = request.Name,
                    Slot = request.Slot,
                    Time = request.Time,
                    From = request.From,
                    To = request.To,
                    Status = PatrolScheduleStatusEnums.ONGOING.ToString()
                };

                var created = await _unitOfWork.PatrolSchedule.CreatePatrolSchedule(pScheduleEntity);

                response.Data = _mapper.Map<PatrolScheduleResponse>(created);
                response.Message = "Tạo thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Tạo thất bại.\n" + ex.Message
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
                    response.Message = "Không tìm thấy Lịch tuần tra !";
                    response.Success = false;
                    return response;
                }

                if (existingPatrolSchedule.Status == PatrolScheduleStatusEnums.FINISHED.ToString() 
                    || existingPatrolSchedule.Status == PatrolScheduleStatusEnums.INACTIVE.ToString())
                {
                    response.Message = "Lịch trực đã kết thúc hoặc đã bị xóa, không thể cập nhật";
                    response.Success = false;
                    return response;
                }

                existingPatrolSchedule.ClassId = request.ClassId ?? existingPatrolSchedule.ClassId;
                existingPatrolSchedule.UserId = request.UserId ?? existingPatrolSchedule.UserId;
                existingPatrolSchedule.Supervisor.StudentSupervisorId = request.SupervisorId ?? existingPatrolSchedule.SupervisorId;
                existingPatrolSchedule.Name = request.Name ?? existingPatrolSchedule.Name;
                existingPatrolSchedule.Slot = request.Slot ?? existingPatrolSchedule.Slot;
                existingPatrolSchedule.Time = request.Time ?? existingPatrolSchedule.Time;
                existingPatrolSchedule.From = request.From ?? existingPatrolSchedule.From;
                existingPatrolSchedule.To = request.To ?? existingPatrolSchedule.To;
                existingPatrolSchedule.Status = request.Status.ToString() ?? existingPatrolSchedule.Status;

                await _unitOfWork.PatrolSchedule.UpdatePatrolSchedule(existingPatrolSchedule);

                response.Data = _mapper.Map<PatrolScheduleResponse>(existingPatrolSchedule);
                response.Message = "Cập nhật thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Cập nhật thất bại.\n" + ex.Message
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
                    response.Message = "Không tìm thấy Lịch tuần tra !";
                    response.Success = false;
                    return response;
                }

                if (existingPSchedule.Status == PatrolScheduleStatusEnums.INACTIVE.ToString())
                {
                    response.Data = null;
                    response.Message = "Lịch tuần tra đã bị xóa";
                    response.Success = false;
                    return response;
                }

                await _unitOfWork.PatrolSchedule.DeletePatrolSchedule(id);
                response.Data = "Empty";
                response.Message = "Xóa thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Xóa thất bại.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<PatrolScheduleResponse>>> GetPatrolSchedulesBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<PatrolScheduleResponse>>();
            try
            {
                var patrolSchedules = await _unitOfWork.PatrolSchedule.GetPatrolSchedulesBySchoolId(schoolId);
                if (patrolSchedules == null || !patrolSchedules.Any())
                {
                    response.Message = "Không tìm thấy Lịch tuần tra nào cho SchoolId được chỉ định!!";
                    response.Success = false;
                }
                else
                {
                    var patrolScheduleDTOs = _mapper.Map<List<PatrolScheduleResponse>>(patrolSchedules);
                    response.Data = patrolScheduleDTOs;
                    response.Message = "Đã tìm thấy Lịch tuần tra";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<PatrolScheduleResponse>>> GetPatrolSchedulesByUserId(int userId)
        {
            var response = new DataResponse<List<PatrolScheduleResponse>>();
            try
            {
                var patrolSchedules = await _unitOfWork.PatrolSchedule.GetPatrolSchedulesByUserId(userId);
                if (patrolSchedules == null || !patrolSchedules.Any())
                {
                    response.Message = "Không tìm thấy Lịch tuần tra nào cho StudentSupervisorId được chỉ định!!";
                    response.Success = false;
                }
                else
                {
                    var patrolScheduleDTOs = _mapper.Map<List<PatrolScheduleResponse>>(patrolSchedules);
                    response.Data = patrolScheduleDTOs;
                    response.Message = "Đã tìm thấy Lịch tuần tra";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<PatrolScheduleResponse>>> GetPatrolSchedulesBySupervisorUserId(int userId)
        {
            var response = new DataResponse<List<PatrolScheduleResponse>>();
            try
            {
                var schedules = await _unitOfWork.PatrolSchedule.GetPatrolSchedulesBySupervisorUserId(userId);
                if (schedules == null || !schedules.Any())
                {
                    response.Message = "Không tìm thấy lịch trực nào đối với UserId được chỉ định";
                    response.Success = false;
                }
                else
                {
                    var violationDTOS = _mapper.Map<List<PatrolScheduleResponse>>(schedules);
                    response.Data = violationDTOS;
                    response.Message = "Đã tìm thấy lịch trực";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }
    }
}
