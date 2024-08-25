using AutoMapper;
using Domain.Entity;
using Domain.Enums.Role;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Authentication;
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
        private readonly IPasswordHasher _passwordHasher;
        public UserImplement(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        public async Task<DataResponse<ResponseOfUser>> CreatePrincipal(RequestOfUser request)
        {
            var response = new DataResponse<ResponseOfUser>();

            try
            {
                var formattedPhone = request.Phone.StartsWith("84") ? request.Phone : "84" + request.Phone;

                var isExistPhone = await _unitOfWork.User.GetAccountByPhone(formattedPhone);
                if (isExistPhone != null)
                {
                    throw new Exception("Số điện thoại đã được sử dụng!!");
                }

                var isExistCode = _unitOfWork.User.Find(u => u.Code == request.Code).FirstOrDefault();
                if (isExistCode != null)
                {
                    throw new Exception("Mã tài khoản đã được sử dụng!!");
                }

                var newPrincipal = _mapper.Map<User>(request);
                newPrincipal.RoleId = (byte)RoleAccountEnum.PRINCIPAL;
                newPrincipal.Status = UserStatusEnums.ACTIVE.ToString();
                newPrincipal.Phone = formattedPhone;
                newPrincipal.Password = _passwordHasher.HashPassword(request.Password);

                _unitOfWork.User.Add(newPrincipal);
                _unitOfWork.Save();

                var userResponse = _mapper.Map<ResponseOfUser>(newPrincipal);

                response.Data = userResponse;
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

        public async Task<DataResponse<ResponseOfUser>> CreateSchoolAdmin(RequestOfUser request)
        {
            var response = new DataResponse<ResponseOfUser>();

            try
            {
                if (request.SchoolId.HasValue)
                {
                    var existedSchoolAdmin = await _unitOfWork.User.GetActiveSchoolAdminBySchoolId(request.SchoolId.Value);
                    if (existedSchoolAdmin != null)
                    {
                        throw new Exception("Trường đã tồn tại một tài khoản SchoolAdmin!");
                    }
                }

                var formattedPhone = request.Phone.StartsWith("84") ? request.Phone : "84" + request.Phone;

                var isExistPhone = await _unitOfWork.User.GetAccountByPhone(formattedPhone);
                if (isExistPhone != null)
                {
                    throw new Exception("Số điện thoại đã được sử dụng!!");
                }

                var isExistCode = _unitOfWork.User.Find(u => u.Code == request.Code).FirstOrDefault();
                if (isExistCode != null)
                {
                    throw new Exception("Mã tài khoản đã được sử dụng!!");
                }

                var newSchoolAdmin = _mapper.Map<User>(request);
                newSchoolAdmin.RoleId = (byte)RoleAccountEnum.SCHOOL_ADMIN;
                newSchoolAdmin.Status = UserStatusEnums.ACTIVE.ToString();
                newSchoolAdmin.Phone = formattedPhone;
                newSchoolAdmin.Password = _passwordHasher.HashPassword(request.Password);

                _unitOfWork.User.Add(newSchoolAdmin);
                _unitOfWork.Save();

                var userResponse = _mapper.Map<ResponseOfUser>(newSchoolAdmin);

                response.Data = userResponse;
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

        public async Task<DataResponse<ResponseOfUser>> DeleteUser(int userId)
        {
            var response = new DataResponse<ResponseOfUser>();
            try
            {
                var user = _unitOfWork.User.GetById(userId);
                if (user is null)
                {
                    response.Data = "Empty";
                    response.Message = "Không thể tìm thấy tài khoản có ID: " + userId;
                    response.Success = false;
                    return response;
                }

                if (user.Status == UserStatusEnums.INACTIVE.ToString())
                {
                    response.Data = "Empty";
                    response.Message = "Tài khoản đã bị xóa!!";
                    response.Success = false;
                    return response;
                }

                user.Status = UserStatusEnums.INACTIVE.ToString();
                _unitOfWork.User.Update(user);
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
            return response; ;
        }

        public async Task<DataResponse<List<ResponseOfUser>>> GetAllUsers(string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfUser>>();

            try
            {
                var users = await _unitOfWork.User.GetAllUsers();
                if (users is null || !users.Any())
                {
                    response.Message = "Danh sách Tài khoản trống";
                    response.Success = true;
                    return response;
                }
                // Sắp xếp danh sách User theo yêu cầu
                var userDTO = _mapper.Map<List<ResponseOfUser>>(users);
                if (sortOrder == "desc")
                {
                    userDTO = userDTO.OrderByDescending(r => r.UserId).ToList();
                }
                else
                {
                    userDTO = userDTO.OrderBy(r => r.UserId).ToList();
                }
                response.Data = userDTO;
                response.Message = "Danh sách các tài khoản";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
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
                    throw new Exception("Tài khoản không tồn tại");
                }
                response.Data = _mapper.Map<ResponseOfUser>(user);
                response.Message = $"UserId {user.UserId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
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
                    response.Message = "Không tìm thấy tài khoản người dùng nào cho SchoolId được chỉ định.";
                    response.Success = false;
                }
                else
                {
                    var userDTOs = _mapper.Map<List<ResponseOfUser>>(users);
                    response.Data = userDTOs;
                    response.Message = "Tìm thấy tài khoản người dùng";
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

        public async Task<DataResponse<ResponseOfUser>> UpdateUser(int id, RequestOfUser request)
        {
            var response = new DataResponse<ResponseOfUser>();

            try
            {
                var user = _unitOfWork.User.GetById(id);
                if (user is null)
                {
                    response.Message = "Không thể tìm thấy tài khoản";
                    response.Success = false;
                    return response;
                }

                if (user.Status != UserStatusEnums.ACTIVE.ToString())
                {
                    response.Message = "Tài khoản đã bị xóa, không thể cập nhật";
                    response.Success = false;
                    return response;
                }

                // Prepend "84" if not already present
                var formattedPhone = request.Phone.StartsWith("84") ? request.Phone : "84" + request.Phone;

                var isExistCode = _unitOfWork.User.Find(u => u.Code == request.Code && u.UserId != id).FirstOrDefault();
                if (isExistCode != null)
                {
                    response.Message = "Mã tài khoản đã được sử dụng";
                    response.Success = false;
                    return response;
                }

                var isExistPhone = _unitOfWork.User.Find(u => u.Phone == formattedPhone && u.UserId != id).FirstOrDefault();
                if (isExistPhone != null)
                {
                    response.Message = "Số điện thoại đã được sử dụng";
                    response.Success = false;
                    return response;
                }

                user.SchoolId = request.SchoolId;
                user.Code = request.Code;
                user.Name = request.Name;
                user.Phone = formattedPhone;
                user.Address = request.Address;

                if (!string.IsNullOrWhiteSpace(request.Password))
                {
                    user.Password = _passwordHasher.HashPassword(request.Password);
                }

                _unitOfWork.User.Update(user);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfUser>(user);
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

    }
}
