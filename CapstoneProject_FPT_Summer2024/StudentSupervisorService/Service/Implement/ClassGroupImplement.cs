﻿using AutoMapper;
using Azure.Core;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.ClassGroupRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ClassGroupResponse;


namespace StudentSupervisorService.Service.Implement
{
    public class ClassGroupImplement : ClassGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassGroupImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DataResponse<List<ClassGroupResponse>>> GetAllClassGroups(string sortOrder)
        {
            var response = new DataResponse<List<ClassGroupResponse>>();
            try
            {

                var classGroupEntities = await _unitOfWork.ClassGroup.GetAllClassGroups();
                if (classGroupEntities is null || !classGroupEntities.Any())
                {
                    response.Message = "Danh sách nhóm lớp trống!";
                    response.Success = true;
                    return response;
                }

                classGroupEntities = sortOrder == "desc"
                    ? classGroupEntities.OrderByDescending(r => r.ClassGroupId).ToList()
                    : classGroupEntities.OrderBy(r => r.ClassGroupId).ToList();

                response.Data = _mapper.Map<List<ClassGroupResponse>>(classGroupEntities);
                response.Message = "Danh sách các nhóm lớp";
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
        public async Task<DataResponse<ClassGroupResponse>> GetClassGroupById(int id)
        {
            var response = new DataResponse<ClassGroupResponse>();
            try
            {
                var classGroupEntity = await _unitOfWork.ClassGroup.GetClassGroupById(id);
                if (classGroupEntity == null)
                {
                    response.Message = "Không thể tìm thấy nhóm lớp !!";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<ClassGroupResponse>(classGroupEntity);
                response.Message = "Đã tìm thấy nhóm lớp";
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
        public async Task<DataResponse<ClassGroupResponse>> CreateClassGroup(ClassGroupCreateRequest request)
        {
            var response = new DataResponse<ClassGroupResponse>();
            try
            {
                var classGroupEntity = new ClassGroup
                {
                    SchoolId = request.SchoolId,
                    TeacherId = request.TeacherId,
                    Name = request.Name,
                    Status = ClassGroupStatusEnums.ACTIVE.ToString()
                };

                var created = await _unitOfWork.ClassGroup.CreateClassGroup(classGroupEntity);

                response.Data = _mapper.Map<ClassGroupResponse>(created);
                response.Message = "Tạo thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Tạo thất bại \n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }
        public async Task<DataResponse<ClassGroupResponse>> UpdateClassGroup(ClassGroupUpdateRequest request)
        {
            var response = new DataResponse<ClassGroupResponse>();
            try
            {
                var existingClassGroup = await _unitOfWork.ClassGroup.GetClassGroupById(request.ClassGroupID);
                if (existingClassGroup == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy nhóm lớp !!";
                    response.Success = false;
                    return response;
                }

                if (existingClassGroup.Status.Equals(ClassGroupStatusEnums.INACTIVE.ToString()))
                {
                    response.Data = null;
                    response.Message = "Không thể cập nhật nhóm lớp đã xóa";
                    response.Success = false;
                    return response;
                }

                existingClassGroup.SchoolId = request.SchoolId ?? existingClassGroup.SchoolId;
                existingClassGroup.TeacherId = request.TeacherId ?? existingClassGroup.TeacherId;
                existingClassGroup.Name = request.Name ?? existingClassGroup.Name;

                await _unitOfWork.ClassGroup.UpdateClassGroup(existingClassGroup);

                response.Data = _mapper.Map<ClassGroupResponse>(existingClassGroup);
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

        public async Task<DataResponse<ClassGroupResponse>> DeleteClassGroup(int id)
        {
            var response = new DataResponse<ClassGroupResponse>();
            try
            {
                var existingClassGroup = await _unitOfWork.ClassGroup.GetClassGroupById(id);
                if (existingClassGroup == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy nhóm lớp !!";
                    response.Success = false;
                    return response;
                }

                if (existingClassGroup.Status == ClassGroupStatusEnums.INACTIVE.ToString())
                {
                    response.Data = null;
                    response.Message = "Nhóm lớp đã bị xóa!!";
                    response.Success = false;
                    return response;
                }

                await _unitOfWork.ClassGroup.DeleteClassGroup(id);
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

        public async Task<DataResponse<List<ClassGroupResponse>>> GetClassGroupsBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<ClassGroupResponse>>();

            try
            {
                var classGroups = await _unitOfWork.ClassGroup.GetClassGroupsBySchoolId(schoolId);
                if (classGroups == null || !classGroups.Any())
                {
                    response.Message = "Không tìm thấy nhóm lớp nào cho SchoolId được chỉ định !!";
                    response.Success = false;
                }
                else
                {
                    var classGroupDTO = _mapper.Map<List<ClassGroupResponse>>(classGroups);
                    response.Data = classGroupDTO;
                    response.Message = "Nhóm lớp đã được tìm thấy";
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

        public async Task<DataResponse<List<ClassGroupResponse>>> GetClassGroupsByUserId(int userId)
        {
            var response = new DataResponse<List<ClassGroupResponse>>();

            try
            {
                var classGroups = await _unitOfWork.ClassGroup.GetClassGroupsByUserId(userId);
                if (classGroups == null || !classGroups.Any())
                {
                    response.Message = "Không tìm thấy nhóm lớp nào cho UserId được chỉ định !!";
                    response.Success = false;
                }
                else
                {
                    var classGroupDTO = _mapper.Map<List<ClassGroupResponse>>(classGroups);
                    response.Data = classGroupDTO;
                    response.Message = "Nhóm lớp đã được tìm thấy";
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
