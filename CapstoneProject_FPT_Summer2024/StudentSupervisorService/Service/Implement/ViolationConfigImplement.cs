﻿using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.ViolationConfigRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ViolationConfigResponse;


namespace StudentSupervisorService.Service.Implement
{
    public class ViolationConfigImplement : ViolationConfigService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ViolationConfigImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<ViolationConfigResponse>> DeleteViolationConfig(int id)
        {
            var response = new DataResponse<ViolationConfigResponse>();
            try
            {
                var violationConfig = _unitOfWork.ViolationConfig.GetById(id);
                if (violationConfig is null)
                {
                    response.Data = "Empty";
                    response.Message = "Không thể tìm thấy Cấu hình vi phạm có ID: " + id;
                    response.Success = false;
                    return response;
                }

                if (violationConfig.Status == ViolationConfigStatusEnums.INACTIVE.ToString())
                {
                    response.Data = "Empty";
                    response.Message = "Cấu hình vi phạm đã bị xóa.";
                    response.Success = false;
                    return response;
                }

                violationConfig.Status = ViolationConfigStatusEnums.INACTIVE.ToString();
                _unitOfWork.ViolationConfig.Update(violationConfig);
                _unitOfWork.Save();

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

        public async Task<DataResponse<List<ViolationConfigResponse>>> GetAllViolationConfigs(string sortOrder)
        {
            var response = new DataResponse<List<ViolationConfigResponse>>();

            try
            {
                var vioConfigs = await _unitOfWork.ViolationConfig.GetAllViolationConfigs();
                if (vioConfigs is null || !vioConfigs.Any())
                {
                    response.Message = "Danh sách Cấu hình Vi phạm trống";
                    response.Success = true;
                    return response;
                }
                // Sắp xếp danh sách ViolationConfig theo yêu cầu
                var vioConfigDTO = _mapper.Map<List<ViolationConfigResponse>>(vioConfigs);
                if (sortOrder == "desc")
                {
                    vioConfigDTO = vioConfigDTO.OrderByDescending(r => r.ViolationConfigId).ToList();
                }
                else
                {
                    vioConfigDTO = vioConfigDTO.OrderBy(r => r.ViolationConfigId).ToList();
                }
                response.Data = vioConfigDTO;
                response.Message = "Danh sách các cấu hình vi phạm";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ViolationConfigResponse>> GetViolationConfigById(int id)
        { 
            var response = new DataResponse<ViolationConfigResponse>();

            try
            {
                var violationConfig = await _unitOfWork.ViolationConfig.GetViolationConfigById(id);
                if (violationConfig is null)
                {
                    throw new Exception("Cấu hình vi phạm không tồn tại");
                }
                response.Data = _mapper.Map<ViolationConfigResponse>(violationConfig);
                response.Message = $"ViolationConfigId {violationConfig.ViolationConfigId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ViolationConfigResponse>>> GetViolationConfigsBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<ViolationConfigResponse>>();
            try
            {
                var violationConfigs = await _unitOfWork.ViolationConfig.GetViolationConfigsBySchoolId(schoolId);
                if (violationConfigs == null || !violationConfigs.Any())
                {
                    response.Message = "Không tìm thấy Cấu hình vi phạm nào cho SchoolId được chỉ định!!";
                    response.Success = false;
                }
                else
                {
                    var vioConfigDTOs = _mapper.Map<List<ViolationConfigResponse>>(violationConfigs);
                    response.Data = vioConfigDTOs;
                    response.Message = "Tìm thấy cấu hình vi phạm";
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

        public async Task<DataResponse<ViolationConfigResponse>> UpdateViolationConfig(int id, RequestOfViolationConfig request)
        {
            var response = new DataResponse<ViolationConfigResponse>();

            try
            {
                var violation = _unitOfWork.ViolationConfig.GetById(id);
                if (violation is null)
                {
                    response.Message = "Không thể tìm thấy Cấu hình Vi phạm!!";
                    response.Success = false;
                    return response;
                }

                if (violation.Status != ViolationConfigStatusEnums.ACTIVE.ToString())
                {
                    response.Message = "Cấu hình vi phạm đã bị xóa, không thể cập nhật";
                    response.Success = false;
                    return response;
                }

                //var violationType = _unitOfWork.ViolationType.GetById(request.ViolationTypeId);
                //if (violationType == null || violationType.Status == ViolationTypeStatusEnums.INACTIVE.ToString())
                //{
                //    response.Message = "Không thể cập nhật Cấu hình vi phạm vì Loại vi phạm không tồn tại hoặc đã bị vô hiệu hóa.";
                //    response.Success = false;
                //    return response;
                //}

                //var existingViolationConfig = await _unitOfWork.ViolationConfig.GetConfigByViolationTypeId(request.ViolationTypeId);
                //if (existingViolationConfig != null && existingViolationConfig.ViolationConfigId != id)
                //{
                //    response.Message = "Loại vi phạm này đã có Cấu hình vi phạm.";
                //    response.Success = false;
                //    return response;
                //}

                //violation.ViolationTypeId = request.ViolationTypeId;
                violation.MinusPoints = request.MinusPoints;
                violation.Description = request.Description;

                _unitOfWork.ViolationConfig.Update(violation);
                _unitOfWork.Save();

                response.Data = _mapper.Map<ViolationConfigResponse>(violation);
                response.Success = true;
                response.Message = "Cập nhật thành công";
            }
            catch (Exception ex)
            {
                response.Message = "Cập nhật thất bại.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ViolationConfigResponse>>> GetActiveViolationConfigsBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<ViolationConfigResponse>>();
            try
            {
                var violationConfigs = await _unitOfWork.ViolationConfig.GetActiveViolationConfigsBySchoolId(schoolId);
                if (violationConfigs == null || !violationConfigs.Any())
                {
                    response.Message = "Không tìm thấy Cấu hình vi phạm nào cho SchoolId được chỉ định!!";
                    response.Success = false;
                }
                else
                {
                    var violationConfigDTOs = _mapper.Map<List<ViolationConfigResponse>>(violationConfigs);
                    response.Data = violationConfigDTOs;
                    response.Message = "Đã tìm thấy danh sách Cấu hình vi phạm";
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

        //public async Task<DataResponse<ViolationConfigResponse>> CreateViolationConfig(RequestOfViolationConfig request)
        //{
        //    var response = new DataResponse<ViolationConfigResponse>();

        //    try
        //    {
        //        var violationType = _unitOfWork.ViolationType.GetById(request.ViolationTypeId);
        //        if (violationType == null || violationType.Status == ViolationTypeStatusEnums.INACTIVE.ToString())
        //        {
        //            response.Message = "Không thể tạo Cấu hình vi phạm vì Loại vi phạm không tồn tại hoặc đã bị vô hiệu hóa.";
        //            response.Success = false;
        //            return response;
        //        }

        //        var existingViolationConfig = await _unitOfWork.ViolationConfig.GetConfigByViolationTypeId(request.ViolationTypeId);
        //        if (existingViolationConfig != null)
        //        {
        //            response.Message = "Loại vi phạm này đã có Cấu hình vi phạm.";
        //            response.Success = false;
        //            return response;
        //        }

        //        var createViolationConfig = _mapper.Map<ViolationConfig>(request);
        //        createViolationConfig.Status = ViolationConfigStatusEnums.ACTIVE.ToString();

        //        _unitOfWork.ViolationConfig.Add(createViolationConfig);
        //        _unitOfWork.Save();

        //        response.Data = _mapper.Map<ViolationConfigResponse>(createViolationConfig);
        //        response.Message = "Tạo Cấu hình vi phạm thành công.";
        //        response.Success = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = "Tạo Cấu hình vi phạm thất bại: " + ex.Message
        //            + (ex.InnerException != null ? ex.InnerException.Message : "");
        //        response.Success = false;
        //    }
        //    return response;
        //}
    }
}
