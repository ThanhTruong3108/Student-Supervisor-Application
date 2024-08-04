using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.ClassRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ClassResponse;


namespace StudentSupervisorService.Service.Implement
{
    public class ClassImplement : ClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<List<ClassResponse>>> GetAllClasses(string sortOrder)
        {
            var response = new DataResponse<List<ClassResponse>>();
            try
            {
                var classEntities = await _unitOfWork.Class.GetAllClasses();
                if (classEntities is null || !classEntities.Any())
                {
                    response.Message = "Danh sách lớp học trống!";
                    response.Success = true;
                    return response;
                }

                classEntities = sortOrder == "desc"
                    ? classEntities.OrderByDescending(r => r.ClassId).ToList()
                    : classEntities.OrderBy(r => r.ClassId).ToList();

                response.Data = _mapper.Map<List<ClassResponse>>(classEntities);
                response.Message = "Danh sách các lớp học";
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

        public async Task<DataResponse<ClassResponse>> GetClassById(int id)
        {
            var response = new DataResponse<ClassResponse>();
            try
            {
                var classEntity = await _unitOfWork.Class.GetClassById(id);
                if (classEntity == null)
                {
                    response.Message = "Không tìm thấy lớp học !!";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<ClassResponse>(classEntity);
                response.Message = "Đã tìm thấy lớp học";
                response.Success = true;
            } catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<ClassResponse>> CreateClass(ClassCreateRequest request)
        {
            var response = new DataResponse<ClassResponse>();
            try
            {
                var isExistCode = _unitOfWork.Class.Find(s => s.Code == request.Code).FirstOrDefault();
                if (isExistCode != null)
                {
                    response.Message = "Mã lớp đã được sử dụng !!";
                    response.Success = false;
                    return response;
                }

                // Kiểm tra niên khóa có tồn tại hay không
                var schoolYear = _unitOfWork.SchoolYear.GetById(request.SchoolYearId);
                if (schoolYear == null)
                {
                    response.Message = "Niên khóa không tồn tại !!";
                    response.Success = false;
                    return response;
                }

                // Kiểm tra tên lớp đã tồn tại trong niên khóa hay chưa
                var isExistName = _unitOfWork.Class.Find(s => s.Name == request.Name && s.SchoolYearId == request.SchoolYearId).FirstOrDefault();
                if (isExistName != null)
                {
                    response.Message = "Tên lớp đã được sử dụng trong niên khóa này !!";
                    response.Success = false;
                    return response;
                }

                // Kiểm tra giáo viên đã thuộc một lớp trong cùng SchoolYear hay chưa
                var teacherClass = _unitOfWork.Class.Find(c => c.SchoolYearId == request.SchoolYearId && c.TeacherId == request.TeacherId).FirstOrDefault();
                if (teacherClass != null)
                {
                    response.Message = "Giáo viên đã thuộc một lớp trong niên khóa này rồi !!";
                    response.Success = false;
                    return response;
                }

                var classEntity = new Class
                {
                    SchoolYearId = request.SchoolYearId,
                    ClassGroupId = request.ClassGroupId,
                    TeacherId = request.TeacherId,
                    Code = request.Code,
                    Grade = request.Grade,
                    Name = request.Name,
                    TotalPoint = request.TotalPoint,
                    Status = ClassStatusEnums.ACTIVE.ToString(),
                };

                var created = await _unitOfWork.Class.CreateClass(classEntity);

                response.Data = _mapper.Map<ClassResponse>(created);
                response.Message = "Lớp được tạo thành công";
                response.Success = true;
            } catch (Exception ex)
            {
                response.Message = "Tạo lớp không thành công: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<ClassResponse>> UpdateClass(int id ,ClassUpdateRequest request)
        {
            var response = new DataResponse<ClassResponse>();
            try
            {
                var existingClass = _unitOfWork.Class.GetById(id);
                if (existingClass == null)
                {
                    response.Data = "Trống";
                    response.Message = "Không tìm thấy lớp học !!";
                    response.Success = false;
                    return response;
                }

                // Kiểm tra niên khóa có tồn tại hay không
                var schoolYear = _unitOfWork.SchoolYear.GetById(request.SchoolYearId);
                if (schoolYear == null)
                {
                    response.Message = "Niên khóa không tồn tại !!";
                    response.Success = false;
                    return response;
                }

                // Kiểm tra mã lớp đã tồn tại hay chưa
                var isExistCode = _unitOfWork.Class.Find(s => s.Code == request.Code && s.ClassId != id).FirstOrDefault();
                if (isExistCode != null)
                {
                    response.Message = "Mã lớp đã được sử dụng !!";
                    response.Success = false;
                    return response;
                }

                // Kiểm tra tên lớp đã tồn tại trong niên khóa hay chưa (ngoại trừ lớp đang cập nhật)
                var isExistName = _unitOfWork.Class.Find(s => s.Name == request.Name && s.SchoolYearId == request.SchoolYearId && s.ClassId != id).FirstOrDefault();
                if (isExistName != null)
                {
                    response.Message = "Tên lớp đã được sử dụng trong niên khóa này !!";
                    response.Success = false;
                    return response;
                }

                // Kiểm tra giáo viên đã thuộc một lớp trong cùng SchoolYear hay chưa (ngoại trừ lớp đang cập nhật)
                var teacherClass = _unitOfWork.Class.Find(c => c.SchoolYearId == request.SchoolYearId && c.TeacherId == request.TeacherId && c.ClassId != id).FirstOrDefault();
                if (teacherClass != null)
                {
                    response.Message = "Giáo viên đã thuộc một lớp trong niên khóa này rồi !!";
                    response.Success = false;
                    return response;
                }

                existingClass.SchoolYearId = request.SchoolYearId ?? existingClass.SchoolYearId;
                existingClass.ClassGroupId = request.ClassGroupId ?? existingClass.ClassGroupId;
                existingClass.TeacherId = request.TeacherId ?? existingClass.TeacherId;
                existingClass.Code = request.Code ?? existingClass.Code;
                existingClass.Grade = request.Grade ?? existingClass.Grade;
                existingClass.Name = request.Name ?? existingClass.Name;
                existingClass.TotalPoint = request.TotalPoint ?? existingClass.TotalPoint;

                await _unitOfWork.Class.UpdateClass(existingClass);

                response.Data = _mapper.Map<ClassResponse>(existingClass);
                response.Message = "Lớp được cập nhật thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Cập nhật lớp không thành công: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<ClassResponse>> DeleteClass(int id)
        {
            var response = new DataResponse<ClassResponse>();
            try
            {
                var existingClass = await _unitOfWork.Class.GetClassById(id);
                if (existingClass == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy lớp học !!";
                    response.Success = false;
                    return response;
                }
                await _unitOfWork.Class.DeleteClass(id);
                response.Data = "Empty";
                response.Message = "Lớp đã được xóa thành công !!";
                response.Success = true;
            } catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<ClassResponse>>> GetClassesBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<ClassResponse>>();
            try
            {
                var classes = await _unitOfWork.Class.GetClasssBySchoolId(schoolId);
                if (classes == null || !classes.Any())
                {
                    response.Message = "Không tìm thấy Lớp nào cho SchoolId được chỉ định";
                    response.Success = false;
                }
                else
                {
                    var classDTOs = _mapper.Map<List<ClassResponse>>(classes);
                    response.Data = classDTOs;
                    response.Message = "Đã tìm thấy lớp học";
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

        public async Task<DataResponse<ClassResponse>> GetClassByScheduleId(int scheduleId)
        {
            var response = new DataResponse<ClassResponse>();
            try
            {
                var classEntity = await _unitOfWork.Class.GetClassByScheduleId(scheduleId);
                if (classEntity == null)
                {
                    response.Message = "Không tìm thấy lớp học cho ScheduleId được chỉ định!";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<ClassResponse>(classEntity);
                response.Message = "Đã tìm thấy lớp học";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<ClassResponse>>> GetClassesByUserId(int userId)
        {
            var response = new DataResponse<List<ClassResponse>>();

            try
            {
                var classes = await _unitOfWork.Class.GetClassesByUserId(userId);
                if (classes == null || !classes.Any())
                {
                    response.Message = "Không tìm thấy lớp nào cho UserId được chỉ định !!";
                    response.Success = false;
                }
                else
                {
                    var classGroupDTO = _mapper.Map<List<ClassResponse>>(classes);
                    response.Data = classGroupDTO;
                    response.Message = "Lớp đã được tìm thấy";
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
