using AutoMapper;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.SchoolAdminResponse;


namespace StudentSupervisorService.Service.Implement
{
    public class SchoolAdminImplement : SchoolAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SchoolAdminImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<List<SchoolAdminResponse>>> GetAllSchoolAdmins(string sortOrder)
        {
            var response = new DataResponse<List<SchoolAdminResponse>>();

            try
            {
                var users = await _unitOfWork.SchoolAdmin.GetAllSchoolAdmins();
                if (users is null || !users.Any())
                {
                    response.Message = "The SchoolAdmin list is empty";
                    response.Success = true;
                    return response;
                }
                // Sắp xếp danh sách User theo yêu cầu
                var schoolAdminDTO = _mapper.Map<List<SchoolAdminResponse>>(users);
                if (sortOrder == "desc")
                {
                    schoolAdminDTO = schoolAdminDTO.OrderByDescending(r => r.AdminId).ToList();
                }
                else
                {
                    schoolAdminDTO = schoolAdminDTO.OrderBy(r => r.AdminId).ToList();
                }
                response.Data = schoolAdminDTO;
                response.Message = "List SchoolAdmins";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<SchoolAdminResponse>> GetBySchoolAdminId(int id)
        {
            var response = new DataResponse<SchoolAdminResponse>();

            try
            {
                var schoolAdmin = await _unitOfWork.SchoolAdmin.GetBySchoolAdminId(id);
                if (schoolAdmin is null)
                {
                    throw new Exception("The SchoolAdmin does not exist");
                }
                response.Data = _mapper.Map<SchoolAdminResponse>(schoolAdmin);
                response.Message = $"SchoolAdminId {schoolAdmin.SchoolAdminId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<SchoolAdminResponse>>> SearchSchoolAdmins(int? schoolId, int? adminId, string sortOrder)
        {
            var response = new DataResponse<List<SchoolAdminResponse>>();

            try
            {
                var schoolAdmins = await _unitOfWork.SchoolAdmin.SearchSchoolAdmins(schoolId, adminId);
                if (schoolAdmins is null || schoolAdmins.Count == 0)
                {
                    response.Message = "No SchoolAdmins found matching the criteria";
                    response.Success = true;
                }
                else
                {
                    var schoolAdminDTO = _mapper.Map<List<SchoolAdminResponse>>(schoolAdmins);

                    // Thực hiện sắp xếp
                    if (sortOrder == "desc")
                    {
                        schoolAdminDTO = schoolAdminDTO.OrderByDescending(p => p.AdminId).ToList();
                    }
                    else
                    {
                        schoolAdminDTO = schoolAdminDTO.OrderBy(p => p.AdminId).ToList();
                    }

                    response.Data = schoolAdminDTO;
                    response.Message = "SchoolAdmins found";
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
    }
}
