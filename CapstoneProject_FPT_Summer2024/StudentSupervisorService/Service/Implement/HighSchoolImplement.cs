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

        public async Task<DataResponse<List<ResponseOfHighSchool>>> GetAllHighSchools()
        {
            var response = new DataResponse<List<ResponseOfHighSchool>>();

            try
            {
                var highSchools = _unitOfWork.HighSchool.GetAll().ToList();
                if (highSchools is null)
                {
                    throw new Exception("The highSchool list is empty");
                }
                response.Data = _mapper.Map<List<ResponseOfHighSchool>>(highSchools);
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
    }
}
