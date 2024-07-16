using AutoMapper;
using Azure;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.SchoolYearRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.SchoolYearResponse;


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
                createSchoolYear.Status = SchoolYearStatusEnums.ONGOING.ToString();
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

        public async Task<DataResponse<ResponseOfSchoolYear>> DeleteSchoolYear(int id)
        {
            var response = new DataResponse<ResponseOfSchoolYear>();
            try
            {
                var schoolYear = _unitOfWork.SchoolYear.GetById(id);
                if (schoolYear is null)
                {
                    response.Data = "Empty";
                    response.Message = "Cannot find SchoolYear with ID: " + id;
                    response.Success = false;
                    return response;
                }

                schoolYear.Status = SchoolYearStatusEnums.INACTIVE.ToString();
                _unitOfWork.SchoolYear.Update(schoolYear);
                _unitOfWork.Save();

                response.Data = "Empty";
                response.Message = "SchoolYear deleted successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Delete SchoolYear failed: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
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
                    schoolYearDTO = schoolYearDTO.OrderByDescending(r => r.SchoolYearId).ToList();
                }
                else
                {
                    schoolYearDTO = schoolYearDTO.OrderBy(r => r.SchoolYearId).ToList();
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

        public async Task<DataResponse<List<ResponseOfSchoolYear>>> GetSchoolYearBySchoolId(int schoolId)
        {
           var response = new DataResponse<List<ResponseOfSchoolYear>>();

            try
            {
                var schoolYears = await _unitOfWork.SchoolYear.GetSchoolYearBySchoolId(schoolId);
                if(schoolYears == null || !schoolYears.Any())
                {
                    response.Message = "No SchoolYears found for the specified SchoolId";
                    response.Success = false;

                }
                else
                {
                    var schoolYearDTO = _mapper.Map<List<ResponseOfSchoolYear>>(schoolYears);
                    response.Data = schoolYearDTO;
                    response.Message = "SchoolYears found";
                    response.Success = true;
                }
            }
            catch(Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message;
                response.Success = false;
            }
            return  response;
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
                        schoolYearDTO = schoolYearDTO.OrderByDescending(p => p.SchoolYearId).ToList();
                    }
                    else
                    {
                        schoolYearDTO = schoolYearDTO.OrderBy(p => p.SchoolYearId).ToList();
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
