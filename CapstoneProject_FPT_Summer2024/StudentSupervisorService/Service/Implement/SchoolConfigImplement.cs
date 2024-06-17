using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.SchoolConfigRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ClassGroupResponse;
using StudentSupervisorService.Models.Response.SchoolConfigResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service.Implement
{
    public class SchoolConfigImplement : SchoolConfigService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SchoolConfigImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<List<SchoolConfigResponse>>> GetAllSchoolConfigs(string sortOrder)
        {
            var response = new DataResponse<List<SchoolConfigResponse>>();
            try
            {
                var schoolConfigEntities = await _unitOfWork.SchoolConfig.GetAllSchoolConfigs();
                if (schoolConfigEntities is null || !schoolConfigEntities.Any())
                {
                    response.Message = "The SchoolConfig list is empty";
                    response.Success = true;
                    return response;
                }

                schoolConfigEntities = sortOrder == "desc"
                    ? schoolConfigEntities.OrderByDescending(r => r.Code).ToList()
                    : schoolConfigEntities.OrderBy(r => r.Code).ToList();

                response.Data = _mapper.Map<List<SchoolConfigResponse>>(schoolConfigEntities);
                response.Message = "List SchoolConfig";
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<SchoolConfigResponse>> GetSchoolConfigById(int id)
        {
            var response = new DataResponse<SchoolConfigResponse>();
            try
            {
                var schoolConfigEntity = await _unitOfWork.SchoolConfig.GetSchoolConfigById(id);
                if (schoolConfigEntity == null)
                {
                    response.Message = "SchoolConfig not found";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<SchoolConfigResponse>(schoolConfigEntity);
                response.Message = "Found a SchoolConfig";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<SchoolConfigResponse>>> SearchSchoolConfigs(int? schoolId, string? name, string? code, string? description, string? status, string sortOrder)
        {
            var response = new DataResponse<List<SchoolConfigResponse>>();

            try
            {
                var schoolConfigEntities = await _unitOfWork.SchoolConfig.SearchSchoolConfigs(schoolId, name, code, description, status);
                if (schoolConfigEntities is null || schoolConfigEntities.Count == 0)
                {
                    response.Message = "No SchoolConfig matches the search criteria";
                    response.Success = true;
                }
                else
                {
                    if (sortOrder == "desc")
                    {
                        schoolConfigEntities = schoolConfigEntities.OrderByDescending(r => r.Code).ToList();
                    }
                    else
                    {
                        schoolConfigEntities = schoolConfigEntities.OrderBy(r => r.Code).ToList();
                    }
                    response.Data = _mapper.Map<List<SchoolConfigResponse>>(schoolConfigEntities);
                    response.Message = "List SchoolConfig";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<SchoolConfigResponse>> CreateSchoolConfig(SchoolConfigCreateRequest request)
        {
            var response = new DataResponse<SchoolConfigResponse>();
            try
            {
                var schoolConfigEntity = new SchoolConfig
                {
                    SchoolId = request.SchoolId,
                    Name = request.Name,
                    Code = request.Code,
                    Description = request.Description,
                    Status = SchoolConfigStatusEnums.ACTIVE.ToString()
                };

                var created = await _unitOfWork.SchoolConfig.CreateSchoolConfig(schoolConfigEntity);

                response.Data = _mapper.Map<SchoolConfigResponse>(created);
                response.Message = "SchoolConfig created successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Create SchoolConfig failed: " + ex.Message + ex.InnerException.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<SchoolConfigResponse>> UpdateSchoolConfig(SchoolConfigUpdateRequest request)
        {
            var response = new DataResponse<SchoolConfigResponse>();

            try
            {
                var existingSchoolConfig = await _unitOfWork.SchoolConfig.GetSchoolConfigById(request.ConfigId);
                if (existingSchoolConfig == null)
                {
                    response.Data = "Empty";
                    response.Message = "SchoolConfig not found";
                    response.Success = false;
                    return response;
                }

                existingSchoolConfig.SchoolId = request.SchoolId ?? existingSchoolConfig.SchoolId;
                existingSchoolConfig.Name = request.Name ?? existingSchoolConfig.Name;
                existingSchoolConfig.Code = request.Code ?? existingSchoolConfig.Code;
                existingSchoolConfig.Description = request.Description ?? existingSchoolConfig.Description;

                await _unitOfWork.SchoolConfig.UpdateSchoolConfig(existingSchoolConfig);

                response.Data = _mapper.Map<SchoolConfigResponse>(existingSchoolConfig);
                response.Message = "SchoolConfig updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Update SchoolConfig failed: " + ex.Message + ex.InnerException.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<SchoolConfigResponse>> DeleteSchoolConfig(int id)
        {
            var response = new DataResponse<SchoolConfigResponse>();
            try
            {
                var existingSchoolConfig = await _unitOfWork.SchoolConfig.GetSchoolConfigById(id);
                if (existingSchoolConfig == null)
                {
                    response.Data = "Empty";
                    response.Message = "SchoolConfig not found";
                    response.Success = false;
                    return response;
                }
                await _unitOfWork.SchoolConfig.DeleteSchoolConfig(id);
                response.Data = "Empty";
                response.Message = "SchoolConfig deleted successfully";
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
