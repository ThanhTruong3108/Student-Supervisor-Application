using AutoMapper;
using Domain.Entity;
using Domain.Enums.Role;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.UserRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.UserResponse;
using System.Security.Principal;


namespace StudentSupervisorService.Service.Implement
{
    public class UserImplement : UserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DataResponse<ResponseOfUser>> CreatePrincipal(RequestOfUser request)
        {
            var response = new DataResponse<ResponseOfUser>();

            try
            {
                var isExist = await _unitOfWork.User.GetAccountByPhone(request.Phone);
                if (isExist != null)
                {
                    throw new Exception("Phone already in use!");
                }

                var newPrincipal = _mapper.Map<User>(request);
                newPrincipal.RoleId = (byte)RoleAccountEnum.PRINCIPAL;
                newPrincipal.Status = UserStatusEnums.ACTIVE.ToString();
                newPrincipal.Phone = "84" + request.Phone;

                _unitOfWork.User.Add(newPrincipal);
                _unitOfWork.Save();

                var userResponse = _mapper.Map<ResponseOfUser>(newPrincipal);

                response.Data = userResponse;
                response.Message = "User created successfully.";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ResponseOfUser>> CreateSchoolAdmin(RequestOfUser request)
        {
            var response = new DataResponse<ResponseOfUser>();

            try
            {
                var isExist = await _unitOfWork.User.GetAccountByPhone(request.Phone);
                if (isExist != null)
                {
                    throw new Exception("Phone already in use!");
                }

                var newSchoolAdmin = _mapper.Map<User>(request);
                newSchoolAdmin.RoleId = (byte)RoleAccountEnum.SCHOOL_ADMIN;
                newSchoolAdmin.Status = UserStatusEnums.ACTIVE.ToString();
                newSchoolAdmin.Phone = "84" + request.Phone;

                _unitOfWork.User.Add(newSchoolAdmin);
                _unitOfWork.Save();

                var userResponse = _mapper.Map<ResponseOfUser>(newSchoolAdmin);

                response.Data = userResponse;
                response.Message = "User created successfully.";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task DeleteUser(int userId)
        {
            var user = _unitOfWork.User.GetById(userId);
            if (user is null)
            {
                throw new Exception("Can not found by" + userId);
            }
            user.Status = UserStatusEnums.INACTIVE.ToString();
            _unitOfWork.User.Update(user);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<List<ResponseOfUser>>> GetAllUsers(string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfUser>>();

            try
            {
                var users = await _unitOfWork.User.GetAllUsers();
                if (users is null || !users.Any())
                {
                    response.Message = "The User list is empty";
                    response.Success = true;
                    return response;
                }
                // Sắp xếp danh sách User theo yêu cầu
                var userDTO = _mapper.Map<List<ResponseOfUser>>(users);
                if (sortOrder == "desc")
                {
                    userDTO = userDTO.OrderByDescending(r => r.Code).ToList();
                }
                else
                {
                    userDTO = userDTO.OrderBy(r => r.Code).ToList();
                }
                response.Data = userDTO;
                response.Message = "List Users";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ResponseOfUser>> GetUserById(int id)
        {
            var response = new DataResponse<ResponseOfUser>();

            try
            {
                var user = await _unitOfWork.User.GetUserById(id);
                if (user is null)
                {
                    throw new Exception("The User does not exist");
                }
                response.Data = _mapper.Map<ResponseOfUser>(user);
                response.Message = $"UserId {user.UserId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ResponseOfUser>>> GetUsersBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<ResponseOfUser>>();

            try
            {
                var users = await _unitOfWork.User.GetUsersBySchoolId(schoolId);
                if (users == null || !users.Any())
                {
                    response.Message = "No users found for the specified SchoolId.";
                    response.Success = false;
                }
                else
                {
                    var userDTOs = _mapper.Map<List<ResponseOfUser>>(users);
                    response.Data = userDTOs;
                    response.Message = "Users found";
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

        public async Task<DataResponse<List<ResponseOfUser>>> SearchUsers(int? schoolId, int? role, string? code, string? name, string? phone, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfUser>>();

            try
            {
                var users = await _unitOfWork.User.SearchUsers( schoolId,role, code, name, phone);
                if (users is null || users.Count == 0)
                {
                    response.Message = "No Users found matching the criteria";
                    response.Success = true;
                }
                else
                {
                    var userDTO = _mapper.Map<List<ResponseOfUser>>(users);

                    // Thực hiện sắp xếp
                    if (sortOrder == "desc")
                    {
                        userDTO = userDTO.OrderByDescending(p => p.Code).ToList();
                    }
                    else
                    {
                        userDTO = userDTO.OrderBy(p => p.Code).ToList();
                    }

                    response.Data = userDTO;
                    response.Message = "Users found";
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

        public async Task<DataResponse<ResponseOfUser>> UpdateUser(int id, RequestOfUser request)
        {
            var response = new DataResponse<ResponseOfUser>();

            try
            {
                var user = _unitOfWork.User.GetById(id);
                if (user is null)
                {
                    response.Message = "Can not found User";
                    response.Success = false;
                    return response;
                }
                user.SchoolId = request.SchoolId;
                //user.RoleId = request.RoleId;
                user.Code = request.Code;
                user.Name = request.Name;
                user.Phone = "84" + request.Phone;
                user.Password = request.Password;
                user.Address = request.Address;
                _unitOfWork.User.Update(user);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfUser>(user);
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
