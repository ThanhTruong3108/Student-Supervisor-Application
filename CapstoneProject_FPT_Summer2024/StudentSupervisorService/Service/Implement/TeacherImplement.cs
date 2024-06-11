using AutoMapper;
using Domain.Entity;
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
        public async Task<DataResponse<TeacherResponse>> CreateTeacher(RequestOfTeacher request)
        {
            var response = new DataResponse<TeacherResponse>();

            try
            {
                var createTeacher = _mapper.Map<Teacher>(request);
                _unitOfWork.Teacher.Add(createTeacher);
                _unitOfWork.Save();
                response.Data = _mapper.Map<TeacherResponse>(createTeacher);
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

        public async Task DeleteTeacher(int id)
        {
            var teacher = _unitOfWork.Teacher.GetById(id);
            if (teacher is null)
            {
                throw new Exception("Can not found by" + id);
            }

            _unitOfWork.Teacher.Remove(teacher);
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
                    teacherDTO = teacherDTO.OrderByDescending(r => r.Code).ToList();
                }
                else
                {
                    teacherDTO = teacherDTO.OrderBy(r => r.Code).ToList();
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
                    var yearPackageDTO = _mapper.Map<List<TeacherResponse>>(teachers);

                    // Thực hiện sắp xếp
                    if (sortOrder == "desc")
                    {
                        yearPackageDTO = yearPackageDTO.OrderByDescending(p => p.Code).ToList();
                    }
                    else
                    {
                        yearPackageDTO = yearPackageDTO.OrderBy(p => p.Code).ToList();
                    }

                    response.Data = yearPackageDTO;
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
                var teacher = _unitOfWork.Teacher.GetById(id);
                if (teacher is null)
                {
                    response.Message = "Can not found Teacher";
                    response.Success = false;
                    return response;
                }

                teacher.SchoolId = request.SchoolId;
                teacher.UserId = request.UserId;
                teacher.Sex = request.Sex;


                _unitOfWork.Teacher.Update(teacher);
                _unitOfWork.Save();

                response.Data = _mapper.Map<TeacherResponse>(teacher);
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
