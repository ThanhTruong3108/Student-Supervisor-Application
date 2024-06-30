using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.HighSchoolRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.HighschoolResponse;
using StudentSupervisorService.Models.Response.SchoolYearResponse;
using StudentSupervisorService.Models.Response.ViolationGroupResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service.Implement
{
    public class HighSchoolImplement : HighSchoolService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public HighSchoolImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<ResponseOfHighSchool>> CreateHighSchool(RequestOfHighSchool request)
        {
            var response = new DataResponse<ResponseOfHighSchool>();

            try
            {
                var createHighSchool = _mapper.Map<HighSchool>(request);
                createHighSchool.Status = HighSchoolEnum.ACTIVE.ToString();
                _unitOfWork.HighSchool.Add(createHighSchool);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfHighSchool>(createHighSchool);
                response.Message = "Create Successfully.";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task DeleteHighSchool(int id)
        {
            var highSchool = _unitOfWork.HighSchool.GetById(id);
            if (highSchool is null)
            {
                throw new Exception("Can not found by" + id);
            }
            highSchool.Status = HighSchoolEnum.INACTIVE.ToString();
            _unitOfWork.HighSchool.Update(highSchool);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<List<ResponseOfHighSchool>>> GetAllHighSchools(string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfHighSchool>>();

            try
            {
                var highSchool = await _unitOfWork.HighSchool.GetAllHighSchools();
                if (highSchool is null || !highSchool.Any())
                {
                    response.Message = "The HighSchool list is empty";
                    response.Success = true;
                    return response;
                }
                // Sắp xếp danh sách Violation Group theo yêu cầu
                var highSchoolDTO = _mapper.Map<List<ResponseOfHighSchool>>(highSchool);
                if (sortOrder == "desc")
                {
                    highSchoolDTO = highSchoolDTO.OrderByDescending(r => r.Code).ToList();
                }
                else
                {
                    highSchoolDTO = highSchoolDTO.OrderBy(r => r.Code).ToList();
                }
                response.Data = highSchoolDTO;
                response.Message = "List HighSchools";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ResponseOfHighSchool>> GetHighSchoolById(int id)
        {
            var response = new DataResponse<ResponseOfHighSchool>();

            try
            {
                var highSchool = await _unitOfWork.HighSchool.GetHighSchoolById(id);
                if (highSchool is null)
                {
                    throw new Exception("The highschool does not exist");
                }
                response.Data = _mapper.Map<ResponseOfHighSchool>(highSchool);
                response.Message = $"SchoolId {highSchool.SchoolId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ResponseOfHighSchool>>> SearchHighSchools(string? code, string? name, string? city, string? address, string? phone, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfHighSchool>>();

            try
            {
                var highSchools = await _unitOfWork.HighSchool.SearchHighSchools(code, name, city, address, phone);
                if (highSchools is null || highSchools.Count == 0)
                {
                    response.Message = "No HighSchools found matching the criteria";
                    response.Success = true;
                }
                else
                {
                    var highSchoolDTO = _mapper.Map<List<ResponseOfHighSchool>>(highSchools);

                    // Thực hiện sắp xếp
                    if (sortOrder == "desc")
                    {
                        highSchoolDTO = highSchoolDTO.OrderByDescending(p => p.Code).ToList();
                    }
                    else
                    {
                        highSchoolDTO = highSchoolDTO.OrderBy(p => p.Code).ToList();
                    }

                    response.Data = highSchoolDTO;
                    response.Message = "HighSchool found";
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

        public async Task<DataResponse<ResponseOfHighSchool>> UpdateHighSchool(int id, RequestOfHighSchool request)
        {
            var response = new DataResponse<ResponseOfHighSchool>();

            try
            {
                var highSchool = _unitOfWork.HighSchool.GetById(id);
                if (highSchool is null)
                {
                    response.Message = "Can not found HighSchool";
                    response.Success = false;
                    return response;
                }
                highSchool.Code = request.Code;
                highSchool.Name = request.Name;
                highSchool.City = request.City;
                highSchool.Address = request.Address;
                highSchool.Phone = request.Phone;
                highSchool.WebUrl = request.WebUrl;
                _unitOfWork.HighSchool.Update(highSchool);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfHighSchool>(highSchool);
                response.Success = true;
                response.Message = "Update Successfully.";
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
