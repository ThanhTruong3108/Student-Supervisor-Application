using AutoMapper;
using Domain.Entity;
using Domain.Enums.Role;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;
using StudentSupervisorService.Models.Request.StudentSupervisorRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.StudentSupervisorResponse;
using StudentSupervisorService.Authentication;


namespace StudentSupervisorService.Service.Implement
{
    public class StudentSupervisorImplement : StudentSupervisorServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        public StudentSupervisorImplement(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<DataResponse<StudentSupervisorResponse>> CreateAccountStudentSupervisor(StudentSupervisorRequest request)
        {
            var response = new DataResponse<StudentSupervisorResponse>();
            try
            {
                // Kiểm tra xem số điện thoại đã tồn tại chưa
                var isExist = await _unitOfWork.User.GetAccountByPhone(request.Phone);
                if (isExist != null)
                {
                    throw new Exception("Số điện thoại đã được sử dụng !!");
                }

                // Kiểm tra thông tin học sinh và lớp học tương ứng
                var studentInClass = _unitOfWork.StudentInClass.GetById(request.StudentInClassId);
                if (studentInClass == null)
                {
                    throw new Exception("Học sinh không tồn tại trong lớp.");
                }

                // Kiểm tra xem lớp này đã có bao nhiêu Sao đỏ
                var supervisorsInClass = _unitOfWork.StudentSupervisor.Find(s => s.StudentInClass.ClassId == studentInClass.ClassId);

                var supervisorCount = supervisorsInClass.Count();

                if (supervisorCount >= 2)
                {
                    throw new Exception("Lớp này đã có 2 Sao đỏ");
                }

                // Kiểm tra xem học sinh này đã là Sao đỏ chưa
                var existingSupervisor = _unitOfWork.StudentSupervisor.SingleOrDefault(s => s.StudentInClassId == request.StudentInClassId);

                if (existingSupervisor != null)
                {
                    throw new Exception("Học sinh này đã là sao đỏ");
                }

                // Mã hóa mật khẩu
                var hashedPassword = _passwordHasher.HashPassword(request.Password);

                var studentSupervisor = new StudentSupervisor
                {
                    StudentInClassId = request.StudentInClassId,
                    Description = request.Description,
                    User = new User
                    {
                        SchoolId = request.SchoolId,
                        Code = request.Code,
                        Name = request.SupervisorName,
                        // Thêm tiền tố "84" nếu không có
                        Phone = request.Phone.StartsWith("84") ? request.Phone : "84" + request.Phone,
                        Password = hashedPassword, // Sử dụng mật khẩu đã mã hóa
                        Address = request.Address,
                        RoleId = (byte)RoleAccountEnum.STUDENT_SUPERVISOR,
                        Status = UserStatusEnums.ACTIVE.ToString()
                    }
                };

                _unitOfWork.StudentSupervisor.Add(studentSupervisor);

                // Cập nhật IsSupervisor cho StudentInClass tương ứng
                studentInClass.IsSupervisor = true;
                _unitOfWork.StudentInClass.Update(studentInClass);

                _unitOfWork.Save();

                response.Data = _mapper.Map<StudentSupervisorResponse>(studentSupervisor);
                response.Message = "Tạo thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Tạo thất bại. " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<StudentSupervisorResponse>> DeleteStudentSupervisor(int id)
        {
            var response = new DataResponse<StudentSupervisorResponse>();
            try
            {
                var stuSupervisor = await _unitOfWork.StudentSupervisor.GetStudentSupervisorById(id);
                if (stuSupervisor == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không thể tìm thấy Sao đỏ có ID: " + id;
                    response.Success = false;
                    return response;
                }

                if (stuSupervisor.User == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy tài khoản được liên kết cho Sao đỏ có ID: " + id;
                    response.Success = false;
                    return response;
                }

                if (stuSupervisor.User.Status == UserStatusEnums.INACTIVE.ToString())
                {
                    response.Data = "Empty";
                    response.Message = "Tài khoản liên kết với Sao đỏ đã bị xóa.";
                    response.Success = false;
                    return response;
                }

                stuSupervisor.User.Status = UserStatusEnums.INACTIVE.ToString();
                _unitOfWork.User.Update(stuSupervisor.User);

                // Cập nhật Supervisor cho StudentInClass tương ứng
                var studentInClass = _unitOfWork.StudentInClass.GetById(stuSupervisor.StudentInClassId.Value);
                if (studentInClass != null)
                {
                    studentInClass.IsSupervisor = false;
                    _unitOfWork.StudentInClass.Update(studentInClass);
                }

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

        public async Task<DataResponse<List<StudentSupervisorResponse>>> GetAllStudentSupervisors(string sortOrder)
        {
            var response = new DataResponse<List<StudentSupervisorResponse>>();

            try
            {
                var stuSupervisors = await _unitOfWork.StudentSupervisor.GetAllStudentSupervisors();
                if (stuSupervisors is null || !stuSupervisors.Any())
                {
                    response.Message = "Danh sách Sao đỏ trống!!";
                    response.Success = true;
                    return response;
                }
                // Sắp xếp danh sách sản phẩm theo yêu cầu
                var stuSuperDTO = _mapper.Map<List<StudentSupervisorResponse>>(stuSupervisors);
                if (sortOrder == "desc")
                {
                    stuSuperDTO = stuSuperDTO.OrderByDescending(r => r.StudentSupervisorId).ToList();
                }
                else
                {
                    stuSuperDTO = stuSuperDTO.OrderBy(r => r.StudentSupervisorId).ToList();
                }
                response.Data = stuSuperDTO;
                response.Message = "Danh sách các Sao đỏ";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<StudentSupervisorResponse>> GetStudentSupervisorById(int id)
        {
            var response = new DataResponse<StudentSupervisorResponse>();

            try
            {
                var stuSupervisor = await _unitOfWork.StudentSupervisor.GetStudentSupervisorById(id);
                if (stuSupervisor is null)
                {
                    throw new Exception("Sao đỏ không tồn tại");
                }
                response.Data = _mapper.Map<StudentSupervisorResponse>(stuSupervisor);
                response.Message = $"StudentSupervisorId {stuSupervisor.StudentSupervisorId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<StudentSupervisorResponse>>> GetStudentSupervisorsBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<StudentSupervisorResponse>>();
            try
            {
                var studentSupervisors = await _unitOfWork.StudentSupervisor.GetStudentSupervisorsBySchoolId(schoolId);
                if (studentSupervisors == null || !studentSupervisors.Any())
                {
                    response.Message = "Không tìm thấy Sao đỏ nào cho SchoolId được chỉ định";
                    response.Success = false;
                }
                else
                {
                    var stuSupervisorDTOs = _mapper.Map<List<StudentSupervisorResponse>>(studentSupervisors);
                    response.Data = stuSupervisorDTOs;
                    response.Message = "Tìm thấy Sao đỏ";
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

        public async Task<DataResponse<StudentSupervisorResponse>> UpdateStudentSupervisor(int id, StudentSupervisorRequest request)
        {
            var response = new DataResponse<StudentSupervisorResponse>();

            try
            {
                var studentSupervisor = await _unitOfWork.StudentSupervisor.GetStudentSupervisorById(id);
                if (studentSupervisor == null)
                {
                    response.Message = "Không thể tìm thấy Sao đỏ";
                    response.Success = false;
                    return response;
                }

                // Check if Code already exists for another StudentSupervisor
                var isExistCode = _unitOfWork.User.Find(u => u.Code == request.Code && u.UserId != studentSupervisor.UserId).FirstOrDefault();
                if (isExistCode != null)
                {
                    response.Message = "Mã tài khoản đã được sử dụng!!";
                    response.Success = false;
                    return response;
                }

                // Check if Phone already exists for another StudentSupervisor
                var isExistPhone = _unitOfWork.User.Find(u => u.Phone == request.Phone && u.UserId != studentSupervisor.UserId).FirstOrDefault();
                if (isExistPhone != null)
                {
                    response.Message = "Số điện thoại tài khoản đã được sử dụng !!";
                    response.Success = false;
                    return response;
                }

                studentSupervisor.StudentInClassId = request.StudentInClassId;
                studentSupervisor.Description = request.Description;

                var user = studentSupervisor.User;
                user.SchoolId = request.SchoolId;
                user.Code = request.Code;
                user.Name = request.SupervisorName;
                // Prepend "84" if not already present
                user.Phone = request.Phone.StartsWith("84") ? request.Phone : "84" + request.Phone;
                user.Password = request.Password; 
                user.Address = request.Address;
                user.Status = UserStatusEnums.ACTIVE.ToString();

                _unitOfWork.StudentSupervisor.Update(studentSupervisor);
                _unitOfWork.User.Update(user);
                _unitOfWork.Save();

                response.Data = _mapper.Map<StudentSupervisorResponse>(studentSupervisor);
                response.Success = true;
                response.Message = "Cập nhật thành công";
            }
            catch (Exception ex)
            {
                response.Message = "Cập nhật thất bại.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<StudentSupervisorResponse>>> GetActiveStudentSupervisorsWithLessThanTwoSchedules(int schoolId)
        {
            var response = new DataResponse<List<StudentSupervisorResponse>>();

            try
            {
                var supervisors = await _unitOfWork.StudentSupervisor.GetActiveStudentSupervisorsWithLessThanTwoSchedules(schoolId);
                if (supervisors == null || !supervisors.Any())
                {
                    response.Message = "Không tìm thấy Sao đỏ nào chưa đủ 2 lịch trực trong trường này!";
                    response.Success = false;
                }
                else
                {
                    var supervisorDTOs = _mapper.Map<List<StudentSupervisorResponse>>(supervisors);
                    response.Data = supervisorDTOs;
                    response.Message = "Danh sách Sao đỏ chưa đủ 2 lịch trực trong trường";
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
