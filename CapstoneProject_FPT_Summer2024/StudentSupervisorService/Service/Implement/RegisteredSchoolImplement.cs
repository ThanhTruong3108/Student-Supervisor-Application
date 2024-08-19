using AutoMapper;
using Azure.Core;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.RegisteredSchoolRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.RegisteredSchoolResponse;


namespace StudentSupervisorService.Service.Implement
{
    public class RegisteredSchoolImplement : RegisteredSchoolService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ValidationService _validationService;

        public RegisteredSchoolImplement(IUnitOfWork unitOfWork, IMapper mapper, ValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<DataResponse<List<RegisteredSchoolResponse>>> GetAllRegisteredSchools(string sortOrder)
        {
            var response = new DataResponse<List<RegisteredSchoolResponse>>();
            try
            {

                var registeredSchoolEntities = await _unitOfWork.RegisteredSchool.GetAllRegisteredSchools();
                if (registeredSchoolEntities is null || !registeredSchoolEntities.Any())
                {
                    response.Message = "Danh sách Trường đã đăng ký trống!!";
                    response.Success = true;
                    return response;
                }

                registeredSchoolEntities = sortOrder == "desc"
                    ? registeredSchoolEntities.OrderByDescending(r => r.RegisteredDate).ToList()
                    : registeredSchoolEntities.OrderBy(r => r.RegisteredDate).ToList();

                response.Data = _mapper.Map<List<RegisteredSchoolResponse>>(registeredSchoolEntities);
                response.Message = "Danh sách các trường đã đăng ký";
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
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
                    response.Message = "Không tìm thấy Trường đã đăng ký!!";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<RegisteredSchoolResponse>(registeredSchoolEntity);
                response.Message = "Tìm thấy Trường đã đăng ký";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
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
                var existedActiveSchool = await _unitOfWork.RegisteredSchool.GetActiveSchoolsBySchoolCodeOrName(
                    request.SchoolCode, request.SchoolName);
                // trường học đã tồn tại và đang ACTIVE
                if (existedActiveSchool != null)
                {
                    response.Data = "Empty";
                    response.Message = "Trường học đã tồn tại và đang hoạt động";
                    response.Success = false;
                    return response;
                }
                
                var existedInactiveSchool = await _unitOfWork.RegisteredSchool.GetInactiveSchoolsBySchoolCodeOrName(
                    request.SchoolCode, request.SchoolName);
                // trường học chưa tồn tại trong registeredschool & highschool
                if (existedInactiveSchool == null)
                {
                    // insert highschool trước
                    var schoolEntity = new HighSchool
                    {
                        Code = request.SchoolCode,
                        Name = request.SchoolName,
                        City = request.City,
                        Address = request.Address,
                        Phone = request.Phone,
                        WebUrl = request.WebURL,
                    };
                    var createdSchool = await _unitOfWork.HighSchool.CreateHighSchool(schoolEntity);
                    // insert registeredschool sau
                    var registeredSchoolEntity = new RegisteredSchool
                    {
                        SchoolId = createdSchool.SchoolId,
                        RegisteredDate = request.RegisteredDate,
                        Description = request.Description,
                        Status = RegisteredSchoolStatusEnums.ACTIVE.ToString()
                    };
                    var createdRegisterSchool = await _unitOfWork.RegisteredSchool.CreateRegisteredSchool(registeredSchoolEntity);
                    response.Data = _mapper.Map<RegisteredSchoolResponse>(createdRegisterSchool);
                }
                // trường học đã tồn tại trong highschool nhưng INACTIVE trong registeredschool
                // ko cần insert highschool, chỉ cần insert registeredschool
                else
                {
                    var registeredSchoolEntity = new RegisteredSchool
                    {
                        SchoolId = existedInactiveSchool.SchoolId,
                        RegisteredDate = request.RegisteredDate,
                        Description = request.Description,
                        Status = RegisteredSchoolStatusEnums.ACTIVE.ToString()
                    };
                    var createdRegisterSchool = await _unitOfWork.RegisteredSchool.CreateRegisteredSchool(registeredSchoolEntity);
                    response.Data = _mapper.Map<RegisteredSchoolResponse>(createdRegisterSchool);
                }

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

        public async Task<DataResponse<RegisteredSchoolResponse>> UpdateRegisteredSchool(RegisteredSchoolUpdateRequest request)
        {
            var response = new DataResponse<RegisteredSchoolResponse>();
            try
            {
                var existing = await _unitOfWork.RegisteredSchool.GetRegisteredSchoolById(request.RegisteredId);
                if (existing == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy Trường đã đăng ký!!";
                    response.Success = false;
                    return response;
                }
                // đã tồn tại trường học trong bảng highschool (theo Code & Name)
                var isDuplicated = await _validationService.IsHighSchoolDuplicated(existing.RegisteredId, request.SchoolCode, request.SchoolName);
                if (isDuplicated)
                {
                    response.Data = "Empty";
                    response.Message = "Mã trường học hoặc tên trường học đã tồn tại";
                    response.Success = false;
                    return response;
                }

                existing.RegisteredDate = request.RegisteredDate ?? existing.RegisteredDate;
                existing.Description = request.Description ?? existing.Description;
                existing.Status = request.Status ?? existing.Status;
                existing.School.Code = request.SchoolCode ?? existing.School.Code;
                existing.School.Name = request.SchoolName ?? existing.School.Name;
                existing.School.City = request.City ?? existing.School.City;
                existing.School.Address = request.Address ?? existing.School.Address;
                existing.School.Phone = request.Phone ?? existing.School.Phone;
                existing.School.WebUrl = request.WebURL ?? existing.School.WebUrl;

                await _unitOfWork.RegisteredSchool.UpdateRegisteredSchool(existing);

                response.Data = _mapper.Map<RegisteredSchoolResponse>(existing);
                response.Message = "Cập nhật thành công";
                response.Success = true;
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

        public async Task<DataResponse<RegisteredSchoolResponse>> DeleteRegisteredSchool(int id)
        {
            var response = new DataResponse<RegisteredSchoolResponse>();
            try
            {
                var existingRegisteredSchool = await _unitOfWork.RegisteredSchool.GetRegisteredSchoolById(id);
                if (existingRegisteredSchool == null ||
                    existingRegisteredSchool.Status == RegisteredSchoolStatusEnums.INACTIVE.ToString())
                {
                    response.Data = "Empty";
                    response.Message = "Trường học không tồn tại hoặc đã bị xóa";
                    response.Success = false;
                    return response;
                }

                await _unitOfWork.RegisteredSchool.DeleteRegisteredSchool(id);
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

        public async Task<DataResponse<List<RegisteredSchoolResponse>>> GetRegisteredSchoolsBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<RegisteredSchoolResponse>>();

            try
            {
                var registeredSchools = await _unitOfWork.RegisteredSchool.GetRegisteredSchoolsBySchoolId(schoolId);
                if (registeredSchools == null || !registeredSchools.Any())
                {
                    response.Message = "Không tìm thấy Trường đã đăng ký nào cho SchoolId được chỉ định.";
                    response.Success = false;
                }
                else
                {
                    var registeredSchoolDTOs = _mapper.Map<List<RegisteredSchoolResponse>>(registeredSchools);
                    response.Data = registeredSchoolDTOs;
                    response.Message = "Các trường đã đăng ký được tìm thấy";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
