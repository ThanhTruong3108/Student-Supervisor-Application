using AutoMapper;
using Azure.Core;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.RegisteredSchoolRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ClassGroupResponse;
using StudentSupervisorService.Models.Response.RegisteredSchoolResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service.Implement
{
    public class RegisteredSchoolImplement : RegisteredSchoolService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisteredSchoolImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<List<RegisteredSchoolResponse>>> GetAllRegisteredSchools(string sortOrder)
        {
            var response = new DataResponse<List<RegisteredSchoolResponse>>();
            try
            {

                var registeredSchoolEntities = await _unitOfWork.RegisteredSchool.GetAllRegisteredSchools();
                if (registeredSchoolEntities is null || !registeredSchoolEntities.Any())
                {
                    response.Message = "The RegisteredSchool list is empty";
                    response.Success = true;
                    return response;
                }

                registeredSchoolEntities = sortOrder == "desc"
                    ? registeredSchoolEntities.OrderByDescending(r => r.RegisteredDate).ToList()
                    : registeredSchoolEntities.OrderBy(r => r.RegisteredDate).ToList();

                response.Data = _mapper.Map<List<RegisteredSchoolResponse>>(registeredSchoolEntities);
                response.Message = "List RegisteredSchool";
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

        public async Task<DataResponse<RegisteredSchoolResponse>> GetRegisteredSchoolById(int id)
        {
            var response = new DataResponse<RegisteredSchoolResponse>();
            try
            {
                var registeredSchoolEntity = await _unitOfWork.RegisteredSchool.GetRegisteredSchoolById(id);
                if (registeredSchoolEntity == null)
                {
                    response.Message = "RegisteredSchool not found";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<RegisteredSchoolResponse>(registeredSchoolEntity);
                response.Message = "Found a RegisteredSchool";
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

        public async Task<DataResponse<List<RegisteredSchoolResponse>>> SearchRegisteredSchools(int? schoolId, DateTime? registerdDate, string? description, string? status, string sortOrder)
        {
            var response = new DataResponse<List<RegisteredSchoolResponse>>();

            try
            {
                var registeredSchoolEntities = await _unitOfWork.RegisteredSchool.SearchRegisteredSchools(schoolId, registerdDate, description, status);
                if (registeredSchoolEntities is null || registeredSchoolEntities.Count == 0)
                {
                    response.Message = "No RegisteredSchool matches the search criteria";
                    response.Success = true;
                }
                else
                {
                    if (sortOrder == "desc")
                    {
                        registeredSchoolEntities = registeredSchoolEntities.OrderByDescending(r => r.RegisteredDate).ToList();
                    }
                    else
                    {
                        registeredSchoolEntities = registeredSchoolEntities.OrderBy(r => r.RegisteredDate).ToList();
                    }
                    response.Data = _mapper.Map<List<RegisteredSchoolResponse>>(registeredSchoolEntities);
                    response.Message = "List RegisteredSchool";
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
        public async Task<DataResponse<RegisteredSchoolResponse>> CreateRegisteredSchool(RegisteredSchoolCreateRequest request)
        {
            var response = new DataResponse<RegisteredSchoolResponse>();
            try
            {
                var registeredSchoolEntity = new RegisteredSchool
                {
                    SchoolId = request.SchoolId,
                    RegisteredDate = request.RegisteredDate,
                    Description = request.Description,
                    Status = RegisteredSchoolStatusEnums.ACTIVE.ToString()
                };

                var created = await _unitOfWork.RegisteredSchool.CreateRegisteredSchool(registeredSchoolEntity);

                response.Data = _mapper.Map<RegisteredSchoolResponse>(created);
                response.Message = "RegisteredSchool created successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Create RegisteredSchool failed: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<RegisteredSchoolResponse>> UpdateRegisteredSchool(RegisteredSchoolUpdateRequest request)
        {
            var response = new DataResponse<RegisteredSchoolResponse>();
            try
            {
                var existingRegisteredSchool = await _unitOfWork.RegisteredSchool.GetRegisteredSchoolById(request.RegisteredId);
                if (existingRegisteredSchool == null)
                {
                    response.Data = "Empty";
                    response.Message = "RegisteredSchool not found";
                    response.Success = false;
                    return response;
                }
                existingRegisteredSchool.SchoolId = request.SchoolId ?? existingRegisteredSchool.SchoolId;
                existingRegisteredSchool.RegisteredDate = request.RegisteredDate ?? existingRegisteredSchool.RegisteredDate;
                existingRegisteredSchool.Description = request.Description ?? existingRegisteredSchool.Description;
                existingRegisteredSchool.Status = request.Status ?? existingRegisteredSchool.Status;

                await _unitOfWork.RegisteredSchool.UpdateRegisteredSchool(existingRegisteredSchool);

                response.Data = _mapper.Map<RegisteredSchoolResponse>(existingRegisteredSchool);
                response.Message = "RegisteredSchool updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Update RegisteredSchool failed: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<RegisteredSchoolResponse>> DeleteRegisteredSchool(int id)
        {
            var response = new DataResponse<RegisteredSchoolResponse>();
            try
            {
                var existingRegisteredSchool = await _unitOfWork.RegisteredSchool.GetRegisteredSchoolById(id);
                if (existingRegisteredSchool == null)
                {
                    response.Data = "Empty";
                    response.Message = "RegisteredSchool not found";
                    response.Success = false;
                    return response;
                }
                await _unitOfWork.RegisteredSchool.DeleteRegisteredSchool(id);
                response.Data = "Empty";
                response.Message = "RegisteredSchool deleted successfully";
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
