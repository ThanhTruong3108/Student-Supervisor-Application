using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.ViolationConfigRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ViolationConfigResponse;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentSupervisorService.Service.Implement
{
    public class ViolationConfigImplement : ViolationConfigService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ViolationConfigImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DataResponse<ViolationConfigResponse>> CreateViolationConfig(RequestOfViolationConfig request)
        {
            var response = new DataResponse<ViolationConfigResponse>();

            try
            {
                var createViolationConfig = _mapper.Map<ViolationConfig>(request);
                createViolationConfig.Status = ViolationConfigStatusEnums.ACTIVE.ToString();
                _unitOfWork.ViolationConfig.Add(createViolationConfig);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ViolationConfigResponse>(createViolationConfig);
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

        public async Task DeleteViolationConfig(int id)
        {
            var violationConfig = _unitOfWork.ViolationConfig.GetById(id);
            if (violationConfig is null)
            {
                throw new Exception("Can not found by" + id);
            }
            violationConfig.Status = ViolationConfigStatusEnums.INACTIVE.ToString();

            _unitOfWork.ViolationConfig.Update(violationConfig);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<List<ViolationConfigResponse>>> GetAllViolationConfigs(string sortOrder)
        {
            var response = new DataResponse<List<ViolationConfigResponse>>();

            try
            {
                var vioConfigs = await _unitOfWork.ViolationConfig.GetAllViolationConfigs();
                if (vioConfigs is null || !vioConfigs.Any())
                {
                    response.Message = "The ViolationConfig list is empty";
                    response.Success = true;
                    return response;
                }
                // Sắp xếp danh sách ViolationConfig theo yêu cầu
                var vioConfigDTO = _mapper.Map<List<ViolationConfigResponse>>(vioConfigs);
                if (sortOrder == "desc")
                {
                    vioConfigDTO = vioConfigDTO.OrderByDescending(r => r.ViolationConfigId).ToList();
                }
                else
                {
                    vioConfigDTO = vioConfigDTO.OrderBy(r => r.ViolationConfigId).ToList();
                }
                response.Data = vioConfigDTO;
                response.Message = "List ViolationConfig";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ViolationConfigResponse>> GetViolationConfigById(int id)
        { 
            var response = new DataResponse<ViolationConfigResponse>();

            try
            {
                var violationConfig = await _unitOfWork.ViolationConfig.GetViolationConfigById(id);
                if (violationConfig is null)
                {
                    throw new Exception("The ViolationConfig does not exist");
                }
                response.Data = _mapper.Map<ViolationConfigResponse>(violationConfig);
                response.Message = $"ViolationConfigId {violationConfig.ViolationConfigId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ViolationConfigResponse>>> SearchViolationConfigs(int? vioTypeId, int? minusPoints, string sortOrder)
        {
            var response = new DataResponse<List<ViolationConfigResponse>>();

            try
            {
                var violationConfigs = await _unitOfWork.ViolationConfig.SearchViolationConfigs(vioTypeId, minusPoints);
                if (violationConfigs is null || violationConfigs.Count == 0)
                {
                    response.Message = "No ViolationConfig found matching the criteria";
                    response.Success = true;
                }
                else
                {
                    var violationConfigDTO = _mapper.Map<List<ViolationConfigResponse>>(violationConfigs);

                    // Thực hiện sắp xếp
                    if (sortOrder == "desc")
                    {
                        violationConfigDTO = violationConfigDTO.OrderByDescending(p => p.ViolationConfigId).ToList();
                    }
                    else
                    {
                        violationConfigDTO = violationConfigDTO.OrderBy(p => p.ViolationConfigId).ToList();
                    }

                    response.Data = violationConfigDTO;
                    response.Message = "ViolationConfigs found";
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

        public async Task<DataResponse<ViolationConfigResponse>> UpdateViolationConfig(int id, RequestOfViolationConfig request)
        {
            var response = new DataResponse<ViolationConfigResponse>();

            try
            {
                var violation = _unitOfWork.ViolationConfig.GetById(id);
                if (violation is null)
                {
                    response.Message = "Can not found Violation";
                    response.Success = false;
                    return response;
                }

                violation.ViolationTypeId = request.ViolationTypeId;
                violation.MinusPoints = request.MinusPoints;
                violation.Description = request.Description;

                _unitOfWork.ViolationConfig.Update(violation);
                _unitOfWork.Save();

                response.Data = _mapper.Map<ViolationConfigResponse>(violation);
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
