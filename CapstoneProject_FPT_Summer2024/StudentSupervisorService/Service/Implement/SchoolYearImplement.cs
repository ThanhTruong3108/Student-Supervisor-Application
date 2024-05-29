using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Authentication;
using StudentSupervisorService.Authentication.Implement;
using StudentSupervisorService.Models.Request.SchoolYearRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.SchoolYearResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service.Implement
{
    public class SchoolYearImplement : SchoolYearService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SchoolYearImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<ResponseOfSchoolYear>> CreateSchoolYear(RequestCreateSchoolYear request)
        {
            var response = new DataResponse<ResponseOfSchoolYear>();

            try
            {
                var createSchoolYear = _mapper.Map<SchoolYear>(request);
                //createSchoolYear.Status = SchoolYearEnum.ACTIVE.ToString();
                createSchoolYear.Status = 1;
                _unitOfWork.SchoolYear.Add(createSchoolYear);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfSchoolYear>(createSchoolYear);
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

        public async Task DeleteSchoolYear(int tennantId)
        {
            var schoolYear = _unitOfWork.SchoolYear.GetById(tennantId);
            if (schoolYear is null)
            {
                throw new Exception("Can not found by" + tennantId);
            }
            //schoolYear.Status = SchoolYearEnum.INACTIVE.ToString();
            schoolYear.Status = 0;
            _unitOfWork.SchoolYear.Update(schoolYear);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<List<ResponseOfSchoolYear>>> GetAllSchoolYears()
        {
            var response = new DataResponse<List<ResponseOfSchoolYear>>();

            try
            {
                var schoolYears = _unitOfWork.SchoolYear.GetAll().ToList();
                if (schoolYears is null)
                {
                    throw new Exception("The schoolyears list is empty");
                }
                response.Data = _mapper.Map<List<ResponseOfSchoolYear>>(schoolYears);
                response.Message = "List schoolYears";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ResponseOfSchoolYear>> GetSchoolYearById(int id)
        {
            var response = new DataResponse<ResponseOfSchoolYear>();

            try
            {
                var schoolYear = await _unitOfWork.SchoolYear.GetSchoolYearById(id);
                if (schoolYear is null)
                {
                    throw new Exception("The schoolyear does not exist");
                }
                response.Data = _mapper.Map<ResponseOfSchoolYear>(schoolYear);
                response.Message = $"SchoolYearId {schoolYear.SchoolYearId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ResponseOfSchoolYear>> UpdateSchoolYear(int id, RequestCreateSchoolYear request)
        {
            var response = new DataResponse<ResponseOfSchoolYear>();

            try
            {
                var schoolYear = _unitOfWork.SchoolYear.GetById(id);
                if (schoolYear is null)
                {
                    response.Message = "Can not found schoolYear";
                    response.Success = false;
                    return response;
                }
                schoolYear.SchoolId = request.SchoolId;
                schoolYear.Year = request.Year;
                schoolYear.StartDate = request.StartDate;
                schoolYear.EndDate = request.EndDate;
                _unitOfWork.SchoolYear.Update(schoolYear);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfSchoolYear>(schoolYear);
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
