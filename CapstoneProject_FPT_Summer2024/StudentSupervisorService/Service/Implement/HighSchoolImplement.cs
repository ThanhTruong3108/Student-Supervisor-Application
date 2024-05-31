using AutoMapper;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.HighschoolResponse;
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

        public async Task<DataResponse<List<ResponseOfHighSchool>>> GetAllHighSchools(int page, int pageSize, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfHighSchool>>();

            try
            {
                var highSchools = await _unitOfWork.HighSchool.GetAllHighSchools();
                if (highSchools is null)
                {
                    response.Message = "The HighSchool list is empty";
                    response.Success = true;
                }

                // Sắp xếp danh sách trường học theo yêu cầu
                var highSchoolDTO = _mapper.Map<List<ResponseOfHighSchool>>(highSchools);
                if (sortOrder == "desc")
                {
                    highSchoolDTO = highSchoolDTO.OrderByDescending(r => r.Code).ToList();
                }
                else
                {
                    highSchoolDTO = highSchoolDTO.OrderBy(r => r.Code).ToList();
                }

                // Thực hiện phân trang
                var startIndex = (page - 1) * pageSize;
                var pagedHighSchools = highSchoolDTO.Skip(startIndex).Take(pageSize).ToList();


                response.Data = pagedHighSchools;
                response.Message = "List highSchools";
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

        public async Task<DataResponse<List<ResponseOfHighSchool>>> SearchHighSchools(string? code, string? name, string? address, string? phone, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfHighSchool>>();

            try
            {
                var highSchools = await _unitOfWork.HighSchool.SearchHighSchools(code, name, address, phone);
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
    }
}
