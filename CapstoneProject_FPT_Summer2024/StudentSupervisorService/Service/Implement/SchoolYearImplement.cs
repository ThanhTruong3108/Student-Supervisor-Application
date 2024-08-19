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
                var existed = await _unitOfWork.SchoolYear.GetOngoingSchoolYearBySchoolIdAndYear(request.SchoolId, request.Year);
                // schoolyear đã tồn tại với trạng thái ONGOING
                if (existed != null)
                {
                    response.Data = "Empty";
                    response.Message = "Năm học đã tồn tại và đang hoạt động.";
                    response.Success = false;
                    return response;
                }

                // if request.Year is > DateTime.Now.Year + 1, throw exception
                if (request.Year > DateTime.Now.Year + 1)
                {
                    response.Data = "Empty";
                    response.Message = "Chỉ được tạo niên khóa trước 1 năm";
                    response.Success = false;
                    return response;
                }
                var createSchoolYear = _mapper.Map<SchoolYear>(request);
                createSchoolYear.Status = SchoolYearStatusEnums.ONGOING.ToString();

                _unitOfWork.SchoolYear.Add(createSchoolYear);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfSchoolYear>(createSchoolYear);
                response.Message = "Tạo thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Tạo thất bại.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
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
                    response.Message = "Không thể tìm thấy Năm học có ID: " + id;
                    response.Success = false;
                    return response;
                }

                if (schoolYear.Status == SchoolYearStatusEnums.INACTIVE.ToString())
                {
                    response.Data = "Empty";
                    response.Message = "Năm học đã bị xóa.";
                    response.Success = false;
                    return response;
                }

                schoolYear.Status = SchoolYearStatusEnums.INACTIVE.ToString();
                _unitOfWork.SchoolYear.Update(schoolYear);
                _unitOfWork.Save();

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

        public async Task<DataResponse<List<ResponseOfSchoolYear>>> GetAllSchoolYears(string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfSchoolYear>>();

            try
            {
                var schoolYears = await _unitOfWork.SchoolYear.GetAllSchoolYears();
                if (schoolYears is null || !schoolYears.Any())
                {
                    response.Message = "Danh sách Năm học trống";
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
                response.Message = "Danh sách các Năm học";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
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
                    throw new Exception("Năm học không tồn tại!!");
                }
                response.Data = _mapper.Map<ResponseOfSchoolYear>(schoolYear);
                response.Message = $"SchoolYearId {schoolYear.SchoolYearId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
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
                    response.Message = "Không tìm thấy Năm học nào cho SchoolId được chỉ định!!";
                    response.Success = false;

                }
                else
                {
                    var schoolYearDTO = _mapper.Map<List<ResponseOfSchoolYear>>(schoolYears);
                    response.Data = schoolYearDTO;
                    response.Message = "Năm học được tìm thấy";
                    response.Success = true;
                }
            }
            catch(Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }
            return  response;
        }

        public async Task<DataResponse<ResponseOfSchoolYear>> UpdateSchoolYear(int id, RequestCreateSchoolYear request)
        {
            var response = new DataResponse<ResponseOfSchoolYear>();

            try
            {
                var schoolYear = _unitOfWork.SchoolYear.GetById(id);
                if (schoolYear is null)
                {
                    response.Message = "Không tìm thấy năm học!!";
                    response.Success = false;
                    return response;
                }

                if (schoolYear.Status.Equals(SchoolYearStatusEnums.INACTIVE.ToString()))
                {
                    response.Data = "Empty";
                    response.Message = "Năm học đã bị xóa. Không thể cập nhật";
                    response.Success = false;
                    return response;
                }

                var existed = await _unitOfWork.SchoolYear.GetOngoingSchoolYearBySchoolIdAndYear(request.SchoolId, request.Year);
                // schoolyear đã tồn tại với trạng thái ONGOING
                if (existed != null && existed.SchoolYearId != id)
                {
                    response.Data = "Empty";
                    response.Message = "Năm học đã tồn tại và đang hoạt động.";
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
                response.Message = "Cập nhật thành công";
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
    }
}
