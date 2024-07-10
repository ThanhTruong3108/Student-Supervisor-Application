using AutoMapper;
using CloudinaryDotNet.Core;
using Domain.Entity;
using Domain.Enums.Role;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.TeacherRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.TeacherResponse;


namespace StudentSupervisorService.Service.Implement
{
    public class TeacherImplement : TeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TeacherImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TeacherResponse> CreateAccountSupervisor(RequestOfTeacher request)
        {
            var isExist = await _unitOfWork.User.GetAccountByPhone(request.Phone);
            if (isExist != null)
            {
                throw new Exception("Phone already in use!");
            }

            var teacher = _mapper.Map<Teacher>(request);

            teacher.User = new User
            {
                SchoolId = request.SchoolId,
                Code = request.Code,
                Name = request.TeacherName,
                // Prepend "84" if not already present
                Phone = request.Phone.StartsWith("84") ? request.Phone : "84" + request.Phone,
                Password = request.Password,
                Address = request.Address,
                RoleId = (byte)RoleAccountEnum.SUPERVISOR,
                Status = UserStatusEnums.ACTIVE.ToString()
            };

            _unitOfWork.Teacher.Add(teacher);
            _unitOfWork.Save();

            return _mapper.Map<TeacherResponse>(teacher);
        }

        public async Task<TeacherResponse> CreateAccountTeacher(RequestOfTeacher request)
        {
            var isExist = await _unitOfWork.User.GetAccountByPhone(request.Phone);
            if (isExist != null)
            {
                throw new Exception("Phone already in use!");
            }

            var teacher = _mapper.Map<Teacher>(request);


            teacher.User = new User
            {
                SchoolId = request.SchoolId,
                Code = request.Code,
                Name = request.TeacherName,
                // Prepend "84" if not already present
                Phone = request.Phone.StartsWith("84") ? request.Phone : "84" + request.Phone,
                Password = request.Password,
                Address = request.Address,
                RoleId = (byte)RoleAccountEnum.TEACHER, 
                Status = UserStatusEnums.ACTIVE.ToString() 
            };

            _unitOfWork.Teacher.Add(teacher);
            _unitOfWork.Save();

            return _mapper.Map<TeacherResponse>(teacher);
        }

        public async Task DeleteTeacher(int id)
        {
            var teacher = await _unitOfWork.Teacher.GetTeacherByIdWithUser(id);
            if (teacher == null)
            {
                throw new Exception("Cannot find Teacher by id " + id);
            }

            if (teacher.User == null)
            {
                throw new Exception("Associated User not found for Teacher id " + id);
            }

            teacher.User.Status = UserStatusEnums.INACTIVE.ToString();

            _unitOfWork.User.Update(teacher.User);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<List<TeacherResponse>>> GetAllTeachers(string sortOrder)
        {
            var response = new DataResponse<List<TeacherResponse>>();

            try
            {
                var teachers = await _unitOfWork.Teacher.GetAllTeachers();
                if (teachers is null || !teachers.Any())
                {
                    response.Message = "The Teacher list is empty";
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
                response.Message = "List Teachers";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
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
                    throw new Exception("The Teacher does not exist");
                }
                response.Data = _mapper.Map<TeacherResponse>(teacher);
                response.Message = $"TeacherId {teacher.TeacherId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<TeacherResponse>>> SearchTeachers(int? schoolId, int? userId, bool sex, string sortOrder)
        {
            var response = new DataResponse<List<TeacherResponse>>();

            try
            {
                var teachers = await _unitOfWork.Teacher.SearchTeachers(schoolId, userId, sex);
                if (teachers is null || teachers.Count == 0)
                {
                    response.Message = "No Teacher found matching the criteria";
                    response.Success = true;
                }
                else
                {
                    var teacherDTO = _mapper.Map<List<TeacherResponse>>(teachers);

                    // Thực hiện sắp xếp
                    if (sortOrder == "desc")
                    {
                        teacherDTO = teacherDTO.OrderByDescending(p => p.TeacherId).ToList();
                    }
                    else
                    {
                        teacherDTO = teacherDTO.OrderBy(p => p.TeacherId).ToList();
                    }

                    response.Data = teacherDTO;
                    response.Message = "Teachers found";
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

        public async Task<DataResponse<TeacherResponse>> UpdateTeacher(int id, RequestOfTeacher request)
        {
            var response = new DataResponse<TeacherResponse>();

            try
            {
                var teacher = await _unitOfWork.Teacher.GetTeacherByIdWithUser(id);
                if (teacher == null)
                {
                    response.Message = "Cannot find Teacher";
                    response.Success = false;
                    return response;
                }

                // Check if Code already exists for another teacher
                var isExistCode =  _unitOfWork.User.Find(u => u.Code == request.Code && u.UserId != teacher.UserId).FirstOrDefault();
                if (isExistCode != null)
                {
                    response.Message = "Code already in use!";
                    response.Success = false;
                    return response;
                }

                // Check if Phone already exists for another teacher
                var isExistPhone =  _unitOfWork.User.Find(u => u.Phone == request.Phone && u.UserId != teacher.UserId).FirstOrDefault();
                if (isExistPhone != null)
                {
                    response.Message = "Phone already in use!";
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
                response.Message = "Update Successfully.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating the teacher: " + ex.Message);
                response.Message = "Oops! Something went wrong.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }
    }
}
