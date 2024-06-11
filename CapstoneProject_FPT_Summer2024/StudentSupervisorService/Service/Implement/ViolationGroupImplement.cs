using AutoMapper;
using Domain.Entity;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.ViolationGroupRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.HighschoolResponse;
using StudentSupervisorService.Models.Response.ViolationGroupResponse;


namespace StudentSupervisorService.Service.Implement
{
    public class ViolationGroupImplement : ViolationGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ViolationGroupImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DataResponse<ResponseOfVioGroup>> CreateVioGroup(RequestOfVioGroup request)
        {
            var response = new DataResponse<ResponseOfVioGroup>();

            try
            {
                var createvioGroup = _mapper.Map<ViolationGroup>(request);
                _unitOfWork.ViolationGroup.Add(createvioGroup);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfVioGroup>(createvioGroup);
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

        public async Task DeleteVioGroup(int id)
        {
            var vioGroup = _unitOfWork.ViolationGroup.GetById(id);
            if (vioGroup is null)
            {
                throw new Exception("Can not found by" + id);
            }

            _unitOfWork.ViolationGroup.Remove(vioGroup);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<List<ResponseOfVioGroup>>> GetAllVioGroups(string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfVioGroup>>();

            try
            {
                var vioGroup = await _unitOfWork.ViolationGroup.GetAllViolationGroups();
                if (vioGroup is null || !vioGroup.Any())
                {
                    response.Message = "The ViolationGroup list is empty";
                    response.Success = true;
                    return response;
                }
                // Sắp xếp danh sách Violation Group theo yêu cầu
                var vioGroupDTO = _mapper.Map<List<ResponseOfVioGroup>>(vioGroup);
                if (sortOrder == "desc")
                {
                    vioGroupDTO = vioGroupDTO.OrderByDescending(r => r.Code).ToList();
                }
                else
                {
                    vioGroupDTO = vioGroupDTO.OrderBy(r => r.Code).ToList();
                }
                response.Data = vioGroupDTO;
                response.Message = "List ViolationGroups";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ResponseOfVioGroup>> GetVioGroupById(int id)
        {
            var response = new DataResponse<ResponseOfVioGroup>();

            try
            {
                var vioGroup = await _unitOfWork.ViolationGroup.GetViolationGroupById(id);
                if (vioGroup is null)
                {
                    throw new Exception("The ViolationGroup does not exist");
                }
                response.Data = _mapper.Map<ResponseOfVioGroup>(vioGroup);
                response.Message = $"ViolationGroupId {vioGroup.ViolationGroupId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ResponseOfVioGroup>>> SearchVioGroups(string? code, string? name, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfVioGroup>>();

            try
            {
                var vioGroups = await _unitOfWork.ViolationGroup.SearchViolationGroups( code, name);
                if (vioGroups is null || vioGroups.Count == 0)
                {
                    response.Message = "No ViolationGroup found matching the criteria";
                    response.Success = true;
                }
                else
                {
                    var vioGroupDTO = _mapper.Map<List<ResponseOfVioGroup>>(vioGroups);

                    // Thực hiện sắp xếp
                    if (sortOrder == "desc")
                    {
                        vioGroupDTO = vioGroupDTO.OrderByDescending(p => p.Code).ToList();
                    }
                    else
                    {
                        vioGroupDTO = vioGroupDTO.OrderBy(p => p.Code).ToList();
                    }

                    response.Data = vioGroupDTO;
                    response.Message = "ViolationGroups found";
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

        public async Task<DataResponse<ResponseOfVioGroup>> UpdateVioGroup(int id, RequestOfVioGroup request)
        {
            var response = new DataResponse<ResponseOfVioGroup>();

            try
            {
                var vioGroup = _unitOfWork.ViolationGroup.GetById(id);
                if (vioGroup is null)
                {
                    response.Message = "Can not found ViolationGroup";
                    response.Success = false;
                    return response;
                }

                vioGroup.Code = request.Code;
                vioGroup.Name = request.VioGroupName;
                vioGroup.Description = request.Description;

                _unitOfWork.ViolationGroup.Update(vioGroup);
                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfVioGroup>(vioGroup);
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
