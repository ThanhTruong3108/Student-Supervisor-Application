﻿using AutoMapper;
using Azure.Core;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.PenaltyRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.PenaltyResponse;

namespace StudentSupervisorService.Service.Implement
{
    public class PenaltyImplement : PenaltyService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PenaltyImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DataResponse<List<PenaltyResponse>>> GetAllPenalties(string sortOrder)
        {
            var response = new DataResponse<List<PenaltyResponse>>();
            try
            {
                var penaltyEntities = await _unitOfWork.Penalty.GetAllPenalties();
                if (penaltyEntities is null || !penaltyEntities.Any())
                {
                    response.Message = "Danh sách Hình phạt trống!";
                    response.Success = true;
                    return response;
                }

                penaltyEntities = sortOrder == "desc"
                    ? penaltyEntities.OrderByDescending(r => r.PenaltyId).ToList()
                    : penaltyEntities.OrderBy(r => r.PenaltyId).ToList();

                response.Data = _mapper.Map<List<PenaltyResponse>>(penaltyEntities);
                response.Message = "Danh sách các Hình phạt";
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

        public async Task<DataResponse<PenaltyResponse>> GetPenaltyById(int id)
        {
            var response = new DataResponse<PenaltyResponse>();
            try
            {
                var penaltyEntity = await _unitOfWork.Penalty.GetPenaltyById(id);
                if (penaltyEntity == null)
                {
                    response.Message = "Không tìm thấy Hình phạt";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<PenaltyResponse>(penaltyEntity);
                response.Message = "Tìm thấy Hình phạt";
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

        public async Task<DataResponse<PenaltyResponse>> CreatePenalty(PenaltyCreateRequest penaltyCreateRequest)
        {
            var response = new DataResponse<PenaltyResponse>();
            try
            {
                var isExistCode = _unitOfWork.Penalty.Find(s => s.Code == penaltyCreateRequest.Code).FirstOrDefault();
                if (isExistCode != null)
                {
                    response.Message = "Mã Hình phạt đã được sử dụng!!";
                    response.Success = false;
                    return response;
                }

                var penaltyEntity = new Penalty
                {
                    SchoolId = penaltyCreateRequest.SchoolId,
                    Code = penaltyCreateRequest.Code,
                    Level = penaltyCreateRequest.Level,
                    Name = penaltyCreateRequest.Name,
                    Description = penaltyCreateRequest.Description,
                    Status = PenaltyStatusEnums.ACTIVE.ToString()
                };

                var created = await _unitOfWork.Penalty.CreatePenalty(penaltyEntity);

                response.Data = _mapper.Map<PenaltyResponse>(created);
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

        public async Task<DataResponse<PenaltyResponse>> UpdatePenalty(int id, PenaltyUpdateRequest penaltyUpdateRequest)
        {
            var response = new DataResponse<PenaltyResponse>();
            try
            {
                var existingPenalty = await _unitOfWork.Penalty.GetPenaltyById(id);
                if (existingPenalty == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy Hình phạt";
                    response.Success = false;
                    return response;
                }

                if (existingPenalty.Status == PenaltyStatusEnums.INACTIVE.ToString())
                {
                    response.Data = "Empty";
                    response.Message = "Hình phạt đã bị xóa, không thể cập nhật!!";
                    response.Success = false;
                    return response;
                }

                var isExistCode = _unitOfWork.Penalty.Find(s => s.Code == penaltyUpdateRequest.Code && s.PenaltyId != id).FirstOrDefault();
                if (isExistCode != null)
                {
                    response.Message = "Mã Hình phạt đã được sử dụng!!";
                    response.Success = false;
                    return response;
                }

                existingPenalty.SchoolId = penaltyUpdateRequest.SchoolId ?? existingPenalty.SchoolId;
                existingPenalty.Code = penaltyUpdateRequest.Code ?? existingPenalty.Code;
                existingPenalty.Level = penaltyUpdateRequest.Level ?? existingPenalty.Level;
                existingPenalty.Name = penaltyUpdateRequest.Name ?? existingPenalty.Name;
                existingPenalty.Description = penaltyUpdateRequest.Description ?? existingPenalty.Description;

                await _unitOfWork.Penalty.UpdatePenalty(existingPenalty);

                response.Data = _mapper.Map<PenaltyResponse>(existingPenalty);
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

        public async Task<DataResponse<PenaltyResponse>> DeletePenalty(int id)
        {
            var response = new DataResponse<PenaltyResponse>();
            try
            {
                var existingPenalty = await _unitOfWork.Penalty.GetPenaltyById(id);
                if (existingPenalty == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy hình phạt!!";
                    response.Success = false;
                    return response;
                }

                if (existingPenalty.Status == PenaltyStatusEnums.INACTIVE.ToString())
                {
                    response.Data = null;
                    response.Message = "Hình phạt đã được xóa!!";
                    response.Success = false;
                    return response;
                }

                await _unitOfWork.Penalty.DeletePenalty(id);
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

        public async Task<DataResponse<List<PenaltyResponse>>> GetPenaltiesBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<PenaltyResponse>>();

            try
            {
                var penalties = await _unitOfWork.Penalty.GetPenaltiesBySchoolId(schoolId);
                if (penalties == null || !penalties.Any())
                {
                    response.Message = "Không tìm thấy Hình phạt nào cho SchoolId được chỉ định!!";
                    response.Success = false;
                }
                else
                {
                    var penaltyDTOs = _mapper.Map<List<PenaltyResponse>>(penalties);
                    response.Data = penaltyDTOs;
                    response.Message = "Đã tìm thấy Hình phạt";
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

        public async Task<DataResponse<List<PenaltyResponse>>> GetActivePenaltiesBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<PenaltyResponse>>();

            try
            {
                var penalties = await _unitOfWork.Penalty.GetActivePenaltiesBySchoolId(schoolId);
                if (penalties == null || !penalties.Any())
                {
                    response.Message = "Không tìm thấy Hình phạt nào cho SchoolId được chỉ định!!";
                    response.Success = false;
                }
                else
                {
                    var penaltyDTOs = _mapper.Map<List<PenaltyResponse>>(penalties);
                    response.Data = penaltyDTOs;
                    response.Message = "Đã tìm thấy Hình phạt";
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
