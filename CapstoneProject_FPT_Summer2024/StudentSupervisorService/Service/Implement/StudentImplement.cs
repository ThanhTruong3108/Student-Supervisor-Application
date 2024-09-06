using AutoMapper;
using Azure.Core;
using Domain.Entity;
using Infrastructures.Interfaces.IUnitOfWork;
using Infrastructures.Repository.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using StudentSupervisorService.Models.Request.StudentRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.StudentResponse;

namespace StudentSupervisorService.Service.Implement
{
    public class StudentImplement : StudentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public StudentImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<DataResponse<List<StudentResponse>>> GetAllStudents(string sortOrder)
        {
            var response = new DataResponse<List<StudentResponse>>();
            try
            {
                var studentEntities = await unitOfWork.Student.GetAllStudents();
                if (studentEntities is null || !studentEntities.Any())
                {
                    response.Message = "Danh sách học sinh trống!!";
                    response.Success = true;
                    return response;
                }

                studentEntities = sortOrder == "desc"
                    ? studentEntities.OrderByDescending(r => r.StudentId).ToList()
                    : studentEntities.OrderBy(r => r.StudentId).ToList();

                response.Data = mapper.Map<List<StudentResponse>>(studentEntities);
                response.Message = "Danh sách các học sinh";
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

        public async Task<DataResponse<StudentResponse>> GetStudentById(int id)
        {
            var response = new DataResponse<StudentResponse>();
            try
            {
                var studentEntity = await unitOfWork.Student.GetStudentById(id);
                if (studentEntity == null)
                {
                    response.Message = "Không tìm thấy học sinh!!";
                    response.Success = false;
                    return response;
                }

                response.Data = mapper.Map<StudentResponse>(studentEntity);
                response.Message = "Tìm thấy học sinh";
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

        public async Task<DataResponse<StudentResponse>> CreateStudent(StudentCreateRequest request)
        {
            var response = new DataResponse<StudentResponse>();
            try
            {
                var isExistCode = unitOfWork.Student.Find(s => s.Code == request.Code).FirstOrDefault();
                if (isExistCode != null)
                {
                    response.Message = "Mã học sinh đã được sử dụng";
                    response.Success = false;
                    return response;
                }

                var studentEntity = new Student
                {
                    SchoolId = request.SchoolId,
                    Code = request.Code,
                    Name = request.Name,
                    Sex = request.Sex,
                    Birthday = request.Birthday,
                    Address = request.Address,
                    Phone = request.Phone
                };

                var created = await unitOfWork.Student.CreateStudent(studentEntity);

                response.Data = mapper.Map<StudentResponse>(created);
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

        public async Task<DataResponse<StudentResponse>> UpdateStudent(StudentUpdateRequest request)
        {
            var response = new DataResponse<StudentResponse>();
            try
            {
                var existingStudent = await unitOfWork.Student.GetStudentById(request.StudentId);
                if (existingStudent == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy Học sinh";
                    response.Success = false;
                    return response;
                }

                var isExistCode = unitOfWork.Student.Find(s => s.Code == request.Code && s.StudentId != request.StudentId).FirstOrDefault();
                if (isExistCode != null)
                {
                    response.Message = "Mã học sinh đã được sử dụng !!";
                    response.Success = false;
                    return response;
                }

                existingStudent.SchoolId = request.SchoolId ?? existingStudent.SchoolId;
                existingStudent.Code = request.Code ?? existingStudent.Code;
                existingStudent.Name = request.Name ?? existingStudent.Name;
                existingStudent.Sex = request.Sex ?? existingStudent.Sex;
                existingStudent.Birthday = request.Birthday ?? existingStudent.Birthday;
                existingStudent.Address = request.Address ?? existingStudent.Address;
                existingStudent.Phone = request.Phone ?? existingStudent.Phone;

                await unitOfWork.Student.UpdateStudent(existingStudent);

                response.Data = mapper.Map<StudentResponse>(existingStudent);
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

        public async Task<DataResponse<StudentResponse>> DeleteStudent(int studentId)
        {
            var response = new DataResponse<StudentResponse>();
            try
            {
                var existingStudent = await unitOfWork.Student.GetStudentById(studentId);
                if (existingStudent == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy Học sinh";
                    response.Success = false;
                    return response;
                }
                await unitOfWork.Student.DeleteStudent(studentId);
                response.Data = "Empty";
                response.Message = "Xóa thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Xóa thất bại.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<StudentResponse>>> GetStudentsBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<StudentResponse>>();

            try
            {
                var students = await unitOfWork.Student.GetStudentsBySchoolId(schoolId);
                if (students == null || !students.Any())
                {
                    response.Message = "Không tìm thấy Học sinh nào cho SchoolId được chỉ định.";
                    response.Success = false;
                }
                else
                {
                    var studentDTO = mapper.Map<List<StudentResponse>>(students);
                    response.Data = studentDTO;
                    response.Message = "Tìm thấy Học sinh";
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

        public async Task<DataResponse<string>> ImportStudentsFromExcel(int userId, IFormFile file)
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
                    var user = await unitOfWork.User.GetUserById(userId);
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            int rowCount = worksheet.Dimension.Rows;
                            List<Student> students = new List<Student>();
                            for (int row = 2; row <= rowCount; row++)
                            {
                                // đã tồn tại Student theo Code hoặc Phone 
                                var code = worksheet.Cells[row, 1].Value.ToString().Trim();
                                var phone = worksheet.Cells[row, 6].Value.ToString().Trim();
                                var existedStudent = unitOfWork.Student.Find(s => s.Code.Equals(code) || s.Phone.Equals(phone)).FirstOrDefault();
                                if (existedStudent != null && existedStudent.SchoolId == (int)user.SchoolId)
                                {
                                    continue;
                                }
                                students.Add(new Student
                                {
                                    SchoolId = (int)user.SchoolId,
                                    Code = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                    Name = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                    Sex = Convert.ToBoolean(worksheet.Cells[row, 3].Value),
                                    Birthday = Convert.ToDateTime(worksheet.Cells[row, 4].Value),
                                    Address = worksheet.Cells[row, 5].Value.ToString().Trim(),
                                    Phone = worksheet.Cells[row, 6].Value.ToString().Trim()
                                });
                            }
                            await unitOfWork.Student.ImportExcel(students);
                            response.Data = "Empty";
                            response.Message = "Import thành công";
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
    }
}
