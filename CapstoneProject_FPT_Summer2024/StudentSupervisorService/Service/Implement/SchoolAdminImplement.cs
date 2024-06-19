using AutoMapper;
using Domain.Entity;
using Domain.Enums.Role;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.SchoolAdminRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ClassGroupResponse;
using StudentSupervisorService.Models.Response.SchoolAdminResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service.Implement
{
    public class SchoolAdminImplement : SchoolAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SchoolAdminImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<List<SchoolAdminResponse>>> GetAllSchoolAdmins(string sortOrder)
        {
            var response = new DataResponse<List<SchoolAdminResponse>>();
            try
            {
                var schoolAdminEntities = await _unitOfWork.SchoolAdmin.GetAllSchoolAdmins();
                if (schoolAdminEntities is null || !schoolAdminEntities.Any())
                {
                    response.Message = "The SchoolAdmin list is empty";
                    response.Success = true;
                    return response;
                }

                schoolAdminEntities = sortOrder == "desc"
                    ? schoolAdminEntities.OrderByDescending(r => r.User.Code).ToList()
                    : schoolAdminEntities.OrderBy(r => r.User.Code).ToList();

                response.Data = _mapper.Map<List<SchoolAdminResponse>>(schoolAdminEntities);
                response.Message = "List SchoolAdmin";
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<SchoolAdminResponse>> GetBySchoolAdminId(int schoolAdminId)
        {
            var response = new DataResponse<SchoolAdminResponse>();
            try
            {
                var schoolAdminEntity = await _unitOfWork.SchoolAdmin.GetBySchoolAdminId(schoolAdminId);
                if (schoolAdminEntity == null)
                {
                    response.Message = "SchoolAdmin not found";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<SchoolAdminResponse>(schoolAdminEntity);
                response.Message = "Found a SchoolAdmin";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<SchoolAdminResponse>> GetByUserId(int userId)
        {
            var response = new DataResponse<SchoolAdminResponse>();
            try
            {
                var schoolAdminEntity = await _unitOfWork.SchoolAdmin.GetByUserId(userId);
                if (schoolAdminEntity == null)
                {
                    response.Message = "SchoolAdmin not found";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<SchoolAdminResponse>(schoolAdminEntity);
                response.Message = "Found a SchoolAdmin";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<SchoolAdminResponse>>> SearchSchoolAdmins(int? schoolId, int? userId)
        {
            var response = new DataResponse<List<SchoolAdminResponse>>();

            try
            {
                var schoolAdminEntities = await _unitOfWork.SchoolAdmin.SearchSchoolAdmins(schoolId, userId);
                if (schoolAdminEntities is null || schoolAdminEntities.Count == 0)
                {
                    response.Message = "No SchoolAdmin matches the search criteria";
                    response.Success = true;
                    return response;
                }

                response.Data = _mapper.Map<List<SchoolAdminResponse>>(schoolAdminEntities);
                response.Message = "List SchoolAdmin";
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }
        public async Task<DataResponse<SchoolAdminResponse>> CreateSchoolAdmin(SchoolAdminCreateRequest request)
        {
            var response = new DataResponse<SchoolAdminResponse>();
            try
            {
                var isExist = await _unitOfWork.User.GetAccountByPhone(request.Phone);
                if (isExist != null)
                {
                    throw new Exception("Phone already in use!");
                }

                var user = new User
                {
                    Code = request.Code,
                    Name = request.Name,
                    Phone = request.Phone,
                    Password = request.Password,
                    Address = request.Address,
                    RoleId = (byte)RoleAccountEnum.SCHOOLADMIN,
                    Status = UserEnum.ACTIVE.ToString()
                };
                _unitOfWork.User.Add(user);
                _unitOfWork.Save();

                var schoolAdmin = new SchoolAdmin
                {
                    SchoolId = request.SchoolId,
                    User = user,
                };
                user.SchoolAdmin = schoolAdmin;
                _unitOfWork.Save();

                response.Data = _mapper.Map<SchoolAdminResponse>(schoolAdmin);
                response.Message = "SchoolAdmin created successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Create SchoolAdmin failed: " + ex.Message + ex.InnerException.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<SchoolAdminResponse>> UpdateSchoolAdmin(SchoolAdminUpdateRequest request)
        {
            var response = new DataResponse<SchoolAdminResponse>();
            try
            {
                var existingSchoolAdmin = await _unitOfWork.SchoolAdmin.GetBySchoolAdminId(request.SchoolAdminId);
                if (existingSchoolAdmin == null)
                {
                    response.Data = "Empty";
                    response.Message = "SchoolAdmin not found";
                    response.Success = false;
                    return response;
                }

                existingSchoolAdmin.SchoolId = request.SchoolId ?? existingSchoolAdmin.SchoolId;
                existingSchoolAdmin.User.Code = request.Code ?? existingSchoolAdmin.User.Code;
                existingSchoolAdmin.User.Name = request.Name ?? existingSchoolAdmin.User.Name;
                existingSchoolAdmin.User.Phone = request.Phone ?? existingSchoolAdmin.User.Phone;
                existingSchoolAdmin.User.Password = request.Password ?? existingSchoolAdmin.User.Password;
                existingSchoolAdmin.User.Address = request.Address ?? existingSchoolAdmin.User.Address;
                existingSchoolAdmin.User.Status = request.Status ?? existingSchoolAdmin.User.Status;

                await _unitOfWork.SchoolAdmin.UpdateSchoolAdmin(existingSchoolAdmin);

                response.Data = _mapper.Map<SchoolAdminResponse>(existingSchoolAdmin);
                response.Message = "SchoolAdmin updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Update SchoolAdmin failed: " + ex.Message + ex.InnerException.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<SchoolAdminResponse>> DeleteSchoolAdmin(int id)
        {
            var response = new DataResponse<SchoolAdminResponse>();
            try
            {
                var existingSchoolAdmin = await _unitOfWork.SchoolAdmin.GetBySchoolAdminId(id);
                if (existingSchoolAdmin == null)
                {
                    response.Data = "Empty";
                    response.Message = "SchoolAdmin not found";
                    response.Success = false;
                    return response;
                }
                await _unitOfWork.SchoolAdmin.DeleteSchoolAdmin(id);
                response.Data = "Empty";
                response.Message = "SchoolAdmin deleted successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }   
    }
}
