using AutoMapper;
using Azure;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
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
                createSchoolYear.Status = SchoolYearEnum.ACTIVE.ToString();
                //createSchoolYear.Status = 1;
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

        public async Task DeleteSchoolYear(int id)
        {
            var schoolYear = _unitOfWork.SchoolYear.GetById(id);
            if (schoolYear is null)
            {
                throw new Exception("Can not found by" + id);
            }
            schoolYear.Status = SchoolYearEnum.INACTIVE.ToString();

            _unitOfWork.SchoolYear.Update(schoolYear);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<List<ResponseOfSchoolYear>>> GetAllSchoolYears(string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfSchoolYear>>();

            try
            {
                var schoolYears = await _unitOfWork.SchoolYear.GetAllSchoolYears();
                if (schoolYears is null || !schoolYears.Any())
                {
                    response.Message = "The SchoolYear list is empty";
                    response.Success = true;
                    return response;
                }
                // Sắp xếp danh sách sản phẩm theo yêu cầu
                var schoolYearDTO = _mapper.Map<List<ResponseOfSchoolYear>>(schoolYears);
                if (sortOrder == "desc")
                {
                    schoolYearDTO = schoolYearDTO.OrderByDescending(r => r.Year).ToList();
                }
                else
                {
                    schoolYearDTO = schoolYearDTO.OrderBy(r => r.Year).ToList();
                }
                response.Data = schoolYearDTO;
                response.Message = "List SchoolYears";
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

        public async Task<DataResponse<List<ResponseOfSchoolYear>>> SearchSchoolYears(short? year, DateTime? startDate, DateTime? endDate, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfSchoolYear>>();

            try
            {
                var schoolYears = await _unitOfWork.SchoolYear.SearchSchoolYears(year, startDate, endDate);
                if (schoolYears is null || schoolYears.Count == 0)
                {   
                    response.Message = "No SchoolYears found matching the criteria";
                    response.Success = true;
                }
                else
                {
                    var schoolYearDTO = _mapper.Map<List<ResponseOfSchoolYear>>(schoolYears);

                    // Thực hiện sắp xếp
                    if (sortOrder == "desc")
                    {
                        schoolYearDTO = schoolYearDTO.OrderByDescending(p => p.Year).ToList();
                    }
                    else
                    {
                        schoolYearDTO = schoolYearDTO.OrderBy(p => p.Year).ToList();
                    }

                    response.Data = schoolYearDTO;
                    response.Message = "SchoolYears found";
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
