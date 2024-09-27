using AutoMapper;
using CloudinaryDotNet.Core;
using Domain.Entity;
using Domain.Enums.Role;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.TeacherRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.TeacherResponse;
using StudentSupervisorService.Authentication;
using Microsoft.AspNetCore.Http;
using Infrastructures.Repository.UnitOfWork;
using OfficeOpenXml;
using Azure.Core;
using System.Globalization;


namespace StudentSupervisorService.Service.Implement
{
    public class TeacherImplement : TeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        public TeacherImplement(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<DataResponse<string>> ImportTeachersFromExcel(int userId, IFormFile file)
        {
            var response = new DataResponse<string>();
            try
            {
                if (file == null || file.Length == 0)
                {
                    response.Data = "Empty";
                    response.Message = "File không tồn tại hoặc rỗng";
                    response.Success = false;
                    return response;
                }

                if (file.FileName.EndsWith(".xls") || file.FileName.EndsWith(".xlsx"))
                {
                    // lấy user từ userId của JWT
                    var user = await _unitOfWork.User.GetUserById(userId);

                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            int rowCount = worksheet.Dimension.Rows;
                            List<Teacher> teachers = new List<Teacher>();

                            for (int row = 2; row <= rowCount; row++)
                            {
                                // đã tồn tại Teacher theo Code hoặc Phone 
                                var code = worksheet.Cells[row, 1].Value.ToString().Trim();
                                var phone = worksheet.Cells[row, 6].Value.ToString().Trim();
                                var normalizedPhone = phone.StartsWith("84") ? phone : "84" + phone;

                                // nếu đã tồn tại Teacher theo Code hoặc Phone trong teachers List => bỏ qua
                                if (teachers.Any(t => t.User.Code.Equals(code) || t.User.Phone.Equals(normalizedPhone)))
                                {
                                    continue;
                                }

                                // nếu đã tồn tại Teacher theo Code hoặc Phone thuộc 1 Schoold trong DB => bỏ qua
                                var existedUser = _unitOfWork.User
                                    .Find(s => (s.Code.Equals(code) || s.Phone.Equals(normalizedPhone)) && s.SchoolId == user.SchoolId)
                                    .FirstOrDefault();
                                if (existedUser != null)
                                {
                                    continue;
                                }
                                
                                teachers.Add(new Teacher
                                {
                                    SchoolId = (int)user.SchoolId,
                                    Sex = !worksheet.Cells[row, 3].Value.ToString().Trim().Equals("Nam"), // cột Sex
                                    User = new User
                                    {
                                        SchoolId = (int)user.SchoolId,
                                        Code = code, // cột Code
                                        Phone = normalizedPhone, // cột Phone đã chuẩn hóa
                                        Name = worksheet.Cells[row, 2].Value.ToString().Trim(), // cột Name
                                        Password = _passwordHasher.HashPassword(worksheet.Cells[row, 4].Value.ToString().Trim()), // cột Password đã mã hóa
                                        Address = worksheet.Cells[row, 5].Value.ToString().Trim(), // cột Address
                                        RoleId = (byte)RoleAccountEnum.TEACHER,
                                        Status = UserStatusEnums.ACTIVE.ToString()
                                    }
                                });
                            }

                            _unitOfWork.Teacher.AddRange(teachers);
                            _unitOfWork.Save();

                            response.Data = "Empty";
                            response.Message = "Import thành công " + teachers.Count + " giáo viên";
                            response.Success = true;
                            return response;
                        }
                    }
                }
                else
                {
                    response.Data = "Empty";
                    response.Message = "File không đúng định dạng";
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Import thất bại.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<TeacherResponse>> CreateAccountSupervisor(RequestOfTeacher request)
        {
            var response = new DataResponse<TeacherResponse>();
            try
            {
                var normalizedPhone = request.Phone.StartsWith("84") ? request.Phone : "84" + request.Phone;

                var isExist = _unitOfWork.User.Find(u => u.Phone == normalizedPhone).FirstOrDefault();
                if (isExist != null)
                {
                    response.Message = "Số điện thoại đã được sử dụng!";
                    response.Success = false;
                    return response;
                }

                var isExistCode = _unitOfWork.User.Find(s => s.Code == request.Code).FirstOrDefault();
                if (isExistCode != null)
                {
                    response.Message = "Mã tài khoản đã được sử dụng!!";
                    response.Success = false;
                    return response;
                }

                // Tạo đối tượng Teacher và ánh xạ từ request
                var teacher = _mapper.Map<Teacher>(request);

                // Mã hóa mật khẩu
                var hashedPassword = _passwordHasher.HashPassword(request.Password);

                teacher.User = new User
                {
                    SchoolId = request.SchoolId,
                    Code = request.Code,
                    Name = request.TeacherName,
                    Phone = normalizedPhone, // Sử dụng số điện thoại đã được chuẩn hóa
                    Password = hashedPassword, // Sử dụng mật khẩu đã mã hóa
                    Address = request.Address,
                    RoleId = (byte)RoleAccountEnum.SUPERVISOR,
                    Status = UserStatusEnums.ACTIVE.ToString()
                };

                _unitOfWork.Teacher.Add(teacher);
                _unitOfWork.Save();

                response.Data = _mapper.Map<TeacherResponse>(teacher);
                response.Message = "Tạo thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Tạo thất bại." + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<TeacherResponse>> CreateAccountTeacher(RequestOfTeacher request)
        {
            var response = new DataResponse<TeacherResponse>();
            try
            {
                // Chuẩn hóa số điện thoại (thêm "84" vào đầu nếu chưa có)
                var normalizedPhone = request.Phone.StartsWith("84") ? request.Phone : "84" + request.Phone;

                // Kiểm tra trùng lặp số điện thoại
                var isExist = _unitOfWork.User.Find(u => u.Phone == normalizedPhone).FirstOrDefault();
                if (isExist != null)
                {
                    response.Message = "Số điện thoại đã được sử dụng!";
                    response.Success = false;
                    return response;
                }

                // Kiểm tra trùng lặp mã tài khoản
                var isExistCode =  _unitOfWork.User.Find(s => s.Code == request.Code).FirstOrDefault();
                if (isExistCode != null)
                {
                    response.Message = "Mã tài khoản đã được sử dụng!!";
                    response.Success = false;
                    return response;
                }

                // Tạo đối tượng Teacher và ánh xạ từ request
                var teacher = _mapper.Map<Teacher>(request);

                // Mã hóa mật khẩu
                var hashedPassword = _passwordHasher.HashPassword(request.Password);

                // Tạo đối tượng User
                teacher.User = new User
                {
                    SchoolId = request.SchoolId,
                    Code = request.Code,
                    Name = request.TeacherName,
                    Phone = normalizedPhone, // Lưu số điện thoại đã được chuẩn hóa
                    Password = hashedPassword, // Sử dụng mật khẩu đã mã hóa
                    Address = request.Address,
                    RoleId = (byte)RoleAccountEnum.TEACHER,
                    Status = UserStatusEnums.ACTIVE.ToString()
                };

                // Thêm đối tượng Teacher vào cơ sở dữ liệu
                _unitOfWork.Teacher.Add(teacher);
                _unitOfWork.Save();

                // Ánh xạ dữ liệu phản hồi
                response.Data = _mapper.Map<TeacherResponse>(teacher);
                response.Message = "Tạo thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ và phản hồi lỗi
                response.Message = "Tạo thất bại.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<TeacherResponse>> DeleteTeacher(int id)
        {
            var response = new DataResponse<TeacherResponse>();
            try
            {
                var teacher = await _unitOfWork.Teacher.GetTeacherByIdWithUser(id);
                if (teacher == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không thể tìm thấy Giáo viên có ID: " + id;
                    response.Success = false;
                    return response;
                }

                if (teacher.User == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy Tài khoản được liên kết cho Giáo viên có ID: " + id;
                    response.Success = false;
                    return response;
                }

                if(teacher.User.Status == UserStatusEnums.INACTIVE.ToString())
                {
                    response.Data = "Empty";
                    response.Message = "Tài khoản liên kết với Giáo viên đã bị xóa.";
                    response.Success = false;
                    return response;
                }

                teacher.User.Status = UserStatusEnums.INACTIVE.ToString();
                _unitOfWork.User.Update(teacher.User);
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

        public async Task<DataResponse<List<TeacherResponse>>> GetAllTeachers(string sortOrder)
        {
            var response = new DataResponse<List<TeacherResponse>>();

            try
            {
                var teachers = await _unitOfWork.Teacher.GetAllTeachers();
                if (teachers is null || !teachers.Any())
                {
                    response.Message = "Danh sách Giáo viên trống";
                    response.Success = true;
                    return response;
                }
                // Sắp xếp danh sách sản phẩm theo yêu cầu
                var teacherDTO = _mapper.Map<List<TeacherResponse>>(teachers);
                if (sortOrder == "desc")
                {
                    teacherDTO = teacherDTO.OrderByDescending(r => r.TeacherId).ToList();
                }
                else
                {
                    teacherDTO = teacherDTO.OrderBy(r => r.TeacherId).ToList();
                }
                response.Data = teacherDTO;
                response.Message = "Danh sách các giáo viên";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<TeacherResponse>> GetTeacherById(int id)
        {
            var response = new DataResponse<TeacherResponse>();

            try
            {
                var teacher = await _unitOfWork.Teacher.GetTeacherById(id);
                if (teacher is null)
                {
                    throw new Exception("Giáo viên không tồn tại");
                }
                response.Data = _mapper.Map<TeacherResponse>(teacher);
                response.Message = $"TeacherId {teacher.TeacherId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<TeacherResponse>>> GetTeachersBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<TeacherResponse>>();

            try
            {
                var teachers = await _unitOfWork.Teacher.GetTeachersBySchoolId(schoolId);
                if (teachers == null || !teachers.Any())
                {
                    response.Message = "Không tìm thấy Giáo viên nào cho SchoolId được chỉ định!!";
                    response.Success = false;
                }
                else
                {
                    var teacherDTOs = _mapper.Map<List<TeacherResponse>>(teachers);
                    response.Data = teacherDTOs;
                    response.Message = "Tìm thấy Giáo viên";
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

        public async Task<DataResponse<TeacherResponse>> UpdateTeacher(int id, RequestOfTeacher request)
        {
            var response = new DataResponse<TeacherResponse>();

            try
            {
                var teacher = await _unitOfWork.Teacher.GetTeacherByIdWithUser(id);
                if (teacher == null)
                {
                    response.Message = "Không thể tìm thấy Giáo viên";
                    response.Success = false;
                    return response;
                }

                // Check if Code already exists for another teacher
                var isExistCode =  _unitOfWork.User.Find(u => u.Code == request.Code && u.UserId != teacher.UserId).FirstOrDefault();
                if (isExistCode != null)
                {
                    response.Message = "Mã tài khoản đã được sử dụng";
                    response.Success = false;
                    return response;
                }

                // Check if Phone already exists for another teacher
                var isExistPhone =  _unitOfWork.User.Find(u => u.Phone == request.Phone && u.UserId != teacher.UserId).FirstOrDefault();
                if (isExistPhone != null)
                {
                    response.Message = "Số điện thoại đã được sử dụng";
                    response.Success = false;
                    return response;
                }

                // Update Teacher entity
                _mapper.Map(request, teacher);

                // Update User entity
                var user = teacher.User;
                user.Name = request.TeacherName;
                // Prepend "84" if not already present
                user.Phone = request.Phone.StartsWith("84") ? request.Phone : "84" + request.Phone;
                user.Password = request.Password;
                user.Address = request.Address;
                user.Status = UserStatusEnums.ACTIVE.ToString(); 

                _unitOfWork.Teacher.Update(teacher);
                _unitOfWork.User.Update(user);
                _unitOfWork.Save();

                response.Data = _mapper.Map<TeacherResponse>(teacher);
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

        public async Task<DataResponse<List<TeacherResponse>>> GetAllTeachersWithRoleTeacher(int schoolId)
        {
            var response = new DataResponse<List<TeacherResponse>>();

            try
            {
                var teachers = await _unitOfWork.Teacher.GetAllTeachersWithRoleTeacher(schoolId);
                if (teachers == null || !teachers.Any())
                {
                    response.Message = "Không tìm thấy giáo viên của trường học này.";
                    response.Success = false;
                }
                else
                {
                    var teacherDTOs = _mapper.Map<List<TeacherResponse>>(teachers);
                    response.Data = teacherDTOs;
                    response.Message = "Danh sách giáo viên của trường";
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

        public async Task<DataResponse<List<TeacherResponse>>> GetAllTeachersWithRoleSupervisor(int schoolId)
        {
            var response = new DataResponse<List<TeacherResponse>>();

            try
            {
                var teachers = await _unitOfWork.Teacher.GetAllTeachersWithRoleSupervisor(schoolId);
                if (teachers == null || !teachers.Any())
                {
                    response.Message = "Không tìm thấy giám thị của trường học này.";
                    response.Success = false;
                }
                else
                {
                    var teacherDTOs = _mapper.Map<List<TeacherResponse>>(teachers);
                    response.Data = teacherDTOs;
                    response.Message = "Danh sách giám thị của trường";
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

        public async Task<DataResponse<List<TeacherResponse>>> GetTeachersWithoutClass(int schoolId, short year)
        {
            var response = new DataResponse<List<TeacherResponse>>();

            try
            {
                var teachers = await _unitOfWork.Teacher.GetTeachersWithoutClass(schoolId, year);
                if (teachers == null || !teachers.Any())
                {
                    response.Message = "Không tìm thấy Giáo viên nào chưa thuộc lớp trong năm học và trường được chỉ định!";
                    response.Success = false;
                }
                else
                {
                    var teacherDTOs = _mapper.Map<List<TeacherResponse>>(teachers);
                    response.Data = teacherDTOs;
                    response.Message = "Tìm thấy Giáo viên";
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
