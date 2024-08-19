using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.StudentInClassRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.StudentInClassResponse;


namespace StudentSupervisorService.Service.Implement
{
    public class StudentInClassImplement : StudentInClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ValidationService _validationService;

        public StudentInClassImplement(IUnitOfWork unitOfWork, IMapper mapper, ValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<DataResponse<List<StudentInClassResponse>>> GetAllStudentInClass(string sortOrder)
        {
            var response = new DataResponse<List<StudentInClassResponse>>();
            try
            {

                var studentInClassEntities = await _unitOfWork.StudentInClass.GetAllStudentInClass();
                if (studentInClassEntities is null || !studentInClassEntities.Any())
                {
                    response.Message = "Danh sách StudentInClass trống!!";
                    response.Success = true;
                    return response;
                }

                studentInClassEntities = sortOrder == "desc"
                    ? studentInClassEntities.OrderByDescending(r => r.StudentInClassId).ToList()
                    : studentInClassEntities.OrderBy(r => r.StudentInClassId).ToList();

                response.Data = _mapper.Map<List<StudentInClassResponse>>(studentInClassEntities);
                response.Message = "Danh sách các StudentInClass";
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

        public async Task<DataResponse<StudentInClassResponse>> GetStudentInClassById(int id)
        {
            var response = new DataResponse<StudentInClassResponse>();
            try
            {
                var studentInClassEntity = await _unitOfWork.StudentInClass.GetStudentInClassById(id);
                if (studentInClassEntity == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy StudentInClass !!";
                    response.Success = false;
                    return response;
                }
                response.Data = _mapper.Map<StudentInClassResponse>(studentInClassEntity);
                response.Message = "Tìm thấy StudentInClass";
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

        public async Task<DataResponse<StudentInClassResponse>> CreateStudentInClass(StudentInClassCreateRequest request)
        {
            var response = new DataResponse<StudentInClassResponse>();
            try
            {
                // 

                // Student trùng mã Code trước đó => ko cho create
                var existedStudentByCode = await _unitOfWork.Student.GetStudentByCode(request.Code);
                if (existedStudentByCode != null)
                {
                    response.Data = "Empty";
                    response.Message = "Mã Code học sinh đã tồn tại";
                    response.Success = false;
                    return response;
                }
                // tạo Student trước
                var createdStudent = await _unitOfWork.Student.CreateStudent(new Student
                {
                    SchoolId = request.SchoolId,
                    Code = request.Code,
                    Name = request.Name,
                    Sex = request.Sex,
                    Birthday = request.Birthday,
                    Address = request.Address,
                    Phone = request.Phone
                });
                // tạo StudentInClass sau
                var createdStudentInClass = await _unitOfWork.StudentInClass.CreateStudentInClass(
                    new StudentInClass
                    {
                        ClassId = request.ClassId,
                        StudentId = createdStudent.StudentId,
                        EnrollDate = request.EnrollDate,
                        IsSupervisor = request.IsSupervisor,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        NumberOfViolation = 0, // mới tạo nên số vi phạm = 0
                        Status = StudentInClassStatusEnums.ENROLLED.ToString()
                    });

                response.Data = _mapper.Map<StudentInClassResponse>(createdStudentInClass);
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
        public async Task<DataResponse<StudentInClassResponse>> UpdateStudentInClass(StudentInClassUpdateRequest request)
        {
            var response = new DataResponse<StudentInClassResponse>();
            try
            {
                // Kiểm tra xem StudentId có tồn tại không
                var existedStudent = await _unitOfWork.Student.GetStudentById(request.StudentId);
                if (existedStudent == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy học sinh";
                    response.Success = false;
                    return response;
                }
                // Student trùng mã Code trước đó => ko cho update
                var existedStudentByCode = await _unitOfWork.Student.GetStudentByCode(request.Code);
                if (existedStudentByCode != null)
                {
                    response.Data = "Empty";
                    response.Message = "Mã Code học sinh đã tồn tại";
                    response.Success = false;
                    return response;
                }
                // Kiểm tra xem ClassId có tồn tại không
                if (request.ClassId != null)
                {
                    var existedClass = await _unitOfWork.Class.GetClassById(request.ClassId.Value);
                    if (existedClass == null)
                    {
                        response.Data = "Empty";
                        response.Message = "Không tìm thấy lớp học";
                        response.Success = false;
                        return response;
                    }
                }
                // nếu ko tồn tại StudentInClassId => ko cho update
                var existingSIC = await _unitOfWork.StudentInClass.GetStudentInClassById(request.StudentInClassId);
                if (existingSIC == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy StudentInClass";
                    response.Success = false;
                    return response;
                }
                // update StudentInClass trước
                existingSIC.Class.ClassId = request.ClassId ?? existingSIC.ClassId; // nếu hsinh chuyển lớp
                existingSIC.EnrollDate = request.EnrollDate ?? existingSIC.EnrollDate;
                existingSIC.IsSupervisor = request.IsSupervisor ?? existingSIC.IsSupervisor;
                existingSIC.StartDate = request.StartDate ?? existingSIC.StartDate;
                existingSIC.EndDate = request.EndDate ?? existingSIC.EndDate;
                existingSIC.NumberOfViolation = request.NumberOfViolation ?? existingSIC.NumberOfViolation;
                existingSIC.Student.Code = request.Code ?? existingSIC.Student.Code;
                existingSIC.Student.Name = request.Name ?? existingSIC.Student.Name;
                existingSIC.Student.Sex = request.Sex ?? existingSIC.Student.Sex;
                existingSIC.Student.Birthday = request.Birthday ?? existingSIC.Student.Birthday;
                existingSIC.Student.Address = request.Address ?? existingSIC.Student.Address;
                existingSIC.Student.Phone = request.Phone ?? existingSIC.Student.Phone;
                var updated = await _unitOfWork.StudentInClass.UpdateStudentInClass(existingSIC);               

                response.Data = _mapper.Map<StudentInClassResponse>(updated);
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

        public async Task<DataResponse<StudentInClassResponse>> DeleteStudentInClass(int id)
        {
            var response = new DataResponse<StudentInClassResponse>();
            try
            {
                var existingStudentInClass = await _unitOfWork.StudentInClass.GetStudentInClassById(id);
                if (existingStudentInClass == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy StudentInClass !!";
                    response.Success = false;
                    return response;
                }

                if (existingStudentInClass.Status == StudentInClassStatusEnums.UNENROLLED.ToString())
                {
                    response.Data = null;
                    response.Message = "StudentInClass đã bị xóa!!";
                    response.Success = false;
                    return response;
                }

                await _unitOfWork.StudentInClass.DeleteStudentInClass(id);
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

        public async Task<DataResponse<StudentInClassResponse>> ChangeStudentToAnotherClass(int studentInClassId, int newClassId)
        {
            var response = new DataResponse<StudentInClassResponse>();
            try
            {
                var existingStudentInClass = await _unitOfWork.StudentInClass.GetStudentInClassById(studentInClassId);
                if (existingStudentInClass == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy StudentInClass !!";
                    response.Success = false;
                    return response;
                }

                // Tạo mới một bản StudentInClass cho lớp mới mà học sinh chuyển vào
                var newStudentInClass = new StudentInClass
                {
                    ClassId = newClassId,
                    StudentId = existingStudentInClass.StudentId,
                    EnrollDate = DateTime.Now,
                    IsSupervisor = existingStudentInClass.IsSupervisor,
                    StartDate = DateTime.Now,
                    EndDate = null,
                    NumberOfViolation = existingStudentInClass.NumberOfViolation,
                    Status = StudentInClassStatusEnums.ENROLLED.ToString()
                };

                // Cập nhật lại thông tin học sinh trong lớp cũ để biết được rằng học sinh đó đã không còn trong lớp đó
                existingStudentInClass.Status = StudentInClassStatusEnums.UNENROLLED.ToString();
                existingStudentInClass.EndDate = DateTime.Now;

                await _unitOfWork.StudentInClass.UpdateStudentInClass(existingStudentInClass);
                await _unitOfWork.StudentInClass.CreateStudentInClass(newStudentInClass);

                response.Data = _mapper.Map<StudentInClassResponse>(newStudentInClass);
                response.Message = "Học sinh thay đổi lớp thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Thay đổi lớp thất bại.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<StudentInClassResponse>>> GetStudentInClassesBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<StudentInClassResponse>>();
            try
            {
                var studentInClasses = await _unitOfWork.StudentInClass.GetStudentInClassesBySchoolId(schoolId);
                if (studentInClasses == null || !studentInClasses.Any())
                {
                    response.Message = "Không tìm thấy StudentInClasses nào cho SchoolId được chỉ định!!";
                    response.Success = false;
                }
                else
                {
                    var studentInClassDTOs = _mapper.Map<List<StudentInClassResponse>>(studentInClasses);
                    response.Data = studentInClassDTOs;
                    response.Message = "Tìm thấy StudentInClasses";
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
