using AutoMapper;
using Domain.Entity;
using Domain.Enums.Role;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.StudentSupervisorRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.StudentSupervisorResponse;


namespace StudentSupervisorService.Service.Implement
{
    public class StudentSupervisorImplement : StudentSupervisorServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StudentSupervisorImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StudentSupervisorResponse> CreateAccountStudentSupervisor(StudentSupervisorRequest request)
        {
            // Check if the phone number already exists
            var isExist = await _unitOfWork.User.GetAccountByPhone(request.Phone);
            if (isExist != null)
            {
                throw new Exception("Phone already in use!");
            }

            var studentSupervisor = new StudentSupervisor
            {
                StudentInClassId = request.StudentInClassId,
                Description = request.Description,
                User = new User
                {
                    SchoolId = request.SchoolId,
                    Code = request.Code,
                    Name = request.SupervisorName,
                    Phone = "84" + request.Phone, // Assuming the phone number needs to be prefixed with "84"
                    Password = request.Password,
                    Address = request.Address,
                    RoleId = (byte)RoleAccountEnum.STUDENTSUPERVISOR,
                    Status = UserStatusEnums.ACTIVE.ToString()
                }
            };

            _unitOfWork.StudentSupervisor.Add(studentSupervisor);
            _unitOfWork.Save();

            return _mapper.Map<StudentSupervisorResponse>(studentSupervisor);
        }

        public async Task DeleteStudentSupervisor(int id)
        {
            var stuSupervisor = await _unitOfWork.StudentSupervisor.GetStudentSupervisorById(id);
            if (stuSupervisor == null)
            {
                throw new Exception("Cannot find StudentSupervisor by id " + id);
            }

            if (stuSupervisor.User == null)
            {
                throw new Exception("Associated User not found for StudentSupervisor id " + id);
            }

            stuSupervisor.User.Status = UserStatusEnums.INACTIVE.ToString();

            _unitOfWork.User.Update(stuSupervisor.User);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<List<StudentSupervisorResponse>>> GetAllStudentSupervisors(string sortOrder)
        {
            var response = new DataResponse<List<StudentSupervisorResponse>>();

            try
            {
                var stuSupervisors = await _unitOfWork.StudentSupervisor.GetAllStudentSupervisors();
                if (stuSupervisors is null || !stuSupervisors.Any())
                {
                    response.Message = "The StudentSupervisor list is empty";
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
                response.Message = "List StudentSupervisors";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
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
                    throw new Exception("The StudentSupervisor does not exist");
                }
                response.Data = _mapper.Map<StudentSupervisorResponse>(stuSupervisor);
                response.Message = $"StudentSupervisorId {stuSupervisor.StudentSupervisorId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<StudentSupervisorResponse>>> SearchStudentSupervisors(int? userId, int? studentInClassId, string sortOrder)
        {
            var response = new DataResponse<List<StudentSupervisorResponse>>();

            try
            {
                var stuSupervisors = await _unitOfWork.StudentSupervisor.SearchStudentSupervisors(userId, studentInClassId);
                if (stuSupervisors is null || stuSupervisors.Count == 0)
                {
                    response.Message = "No StudentSupervisor found matching the criteria";
                    response.Success = true;
                }
                else
                {
                    var stuSupervisorDTO = _mapper.Map<List<StudentSupervisorResponse>>(stuSupervisors);

                    // Thực hiện sắp xếp
                    if (sortOrder == "desc")
                    {
                        stuSupervisorDTO = stuSupervisorDTO.OrderByDescending(p => p.StudentSupervisorId).ToList();
                    }
                    else
                    {
                        stuSupervisorDTO = stuSupervisorDTO.OrderBy(p => p.StudentSupervisorId).ToList();
                    }

                    response.Data = stuSupervisorDTO;
                    response.Message = "StudentSupervisors found";
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

        public async Task<DataResponse<StudentSupervisorResponse>> UpdateStudentSupervisor(int id, StudentSupervisorRequest request)
        {
            var response = new DataResponse<StudentSupervisorResponse>();

            try
            {
                // Retrieve the existing StudentSupervisor entity by ID
                var studentSupervisor = await _unitOfWork.StudentSupervisor.GetStudentSupervisorById(id);
                if (studentSupervisor == null)
                {
                    response.Message = "Cannot find StudentSupervisor";
                    response.Success = false;
                    return response;
                }

                studentSupervisor.StudentInClassId = request.StudentInClassId;
                studentSupervisor.Description = request.Description;

                var user = studentSupervisor.User;
                user.SchoolId = request.SchoolId;
                user.Code = request.Code;
                user.Name = request.SupervisorName;
                user.Phone = request.Phone;
                user.Password = request.Password; 
                user.Address = request.Address;
                user.Status = UserStatusEnums.ACTIVE.ToString();

                _unitOfWork.StudentSupervisor.Update(studentSupervisor);
                _unitOfWork.User.Update(user);
                _unitOfWork.Save();

                response.Data = _mapper.Map<StudentSupervisorResponse>(studentSupervisor);
                response.Success = true;
                response.Message = "Update Successfully.";
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
