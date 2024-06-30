using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.ViolationTypeRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.HighschoolResponse;
using StudentSupervisorService.Models.Response.ViolationResponse;
using StudentSupervisorService.Models.Response.ViolationTypeResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentSupervisorService.Service.Implement
{
    public class ViolationTypeImplement : ViolationTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ViolationTypeImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DataResponse<ResponseOfVioType>> CreateVioType(RequestOfVioType request)
        {
            var response = new DataResponse<ResponseOfVioType>();

            try
            {
                var createVioType = _mapper.Map<ViolationType>(request);
                _unitOfWork.ViolationType.Add(createVioType);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfVioType>(createVioType);
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

        public async Task DeleteVioType(int id)
        {
            var vioType = _unitOfWork.ViolationType.GetById(id);
            if (vioType is null)
            {
                throw new Exception("Can not found by" + id);
            }

            _unitOfWork.ViolationType.Remove(vioType);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<List<ResponseOfVioType>>> GetAllVioTypes(string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfVioType>>();

            try
            {
                var vioTypes = await _unitOfWork.ViolationType.GetAllVioTypes();
                if (vioTypes is null || !vioTypes.Any())
                {
                    response.Message = "The ViolationType list is empty";
                    response.Success = true;
                    return response;
                }
                // Sắp xếp danh sách sản phẩm theo yêu cầu
                var vioTypeDTO = _mapper.Map<List<ResponseOfVioType>>(vioTypes);
                if (sortOrder == "desc")
                {
                    vioTypeDTO = vioTypeDTO.OrderByDescending(r => r.ViolationTypeId).ToList();
                }
                else
                {
                    vioTypeDTO = vioTypeDTO.OrderBy(r => r.ViolationTypeId).ToList();
                }
                response.Data = vioTypeDTO;
                response.Message = "List ViolationTypes";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ResponseOfVioType>> GetVioTypeById(int id)
        {
            var response = new DataResponse<ResponseOfVioType>();

            try
            {
                var vioType = await _unitOfWork.ViolationType.GetVioTypeById(id);
                if (vioType is null)
                {
                    throw new Exception("The ViolationType does not exist");
                }
                response.Data = _mapper.Map<ResponseOfVioType>(vioType);
                response.Message = $"ViolationTypeId {vioType.ViolationTypeId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ResponseOfVioType>>> SearchVioTypes(int? vioGroupId, string? name, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfVioType>>();

            try
            {
                var violations = await _unitOfWork.ViolationType.SearchVioTypes(vioGroupId, name);
                if (violations is null || violations.Count == 0)
                {
                    response.Message = "No ViolationType found matching the criteria";
                    response.Success = true;
                }
                else
                {
                    var vioTypeDTO = _mapper.Map<List<ResponseOfVioType>>(violations);

                    // Thực hiện sắp xếp
                    if (sortOrder == "desc")
                    {
                        vioTypeDTO = vioTypeDTO.OrderByDescending(p => p.ViolationTypeId).ToList();
                    }
                    else
                    {
                        vioTypeDTO = vioTypeDTO.OrderBy(p => p.ViolationTypeId).ToList();
                    }

                    response.Data = vioTypeDTO;
                    response.Message = "ViolationTypes found";
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

        public async Task<DataResponse<ResponseOfVioType>> UpdateVioType(int id, RequestOfVioType request)
        {
            var response = new DataResponse<ResponseOfVioType>();

            try
            {
                var vioType = _unitOfWork.ViolationType.GetById(id);
                if (vioType is null)
                {
                    response.Message = "Can not found ViolationType";
                    response.Success = false;
                    return response;
                }
                vioType.ViolationGroupId = request.ViolationGroupId;
                vioType.Name = request.VioTypeName;
                vioType.Description = request.Description;

                _unitOfWork.ViolationType.Update(vioType);
                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfVioType>(vioType);
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
