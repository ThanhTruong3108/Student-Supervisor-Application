using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.PackageTypeRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.PackageTypeResponse;

namespace StudentSupervisorService.Service.Implement
{
    public class PackageTypeImplement : PackageTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PackageTypeImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DataResponse<PackageTypeResponse>> CreatePackageType(PackageTypeRequest request)
        {
            var response = new DataResponse<PackageTypeResponse>();

            try
            {
                var createPackageType = _mapper.Map<PackageType>(request);
                createPackageType.Status = PackageTypeStatusEnums.ACTIVE.ToString();
                _unitOfWork.PackageType.Add(createPackageType);
                _unitOfWork.Save();
                response.Data = _mapper.Map<PackageTypeResponse>(createPackageType);
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

        public async Task DeletePackageType(int id)
        {
            var packageType = _unitOfWork.PackageType.GetById(id);
            if (packageType is null)
            {
                throw new Exception("Can not found by" + id);
            }
            packageType.Status = PackageTypeStatusEnums.INACTIVE.ToString();
            _unitOfWork.PackageType.Update(packageType);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<List<PackageTypeResponse>>> GetAllPackageTypes(string sortOrder)
        {
            var response = new DataResponse<List<PackageTypeResponse>>();

            try
            {
                var packageTypes = await _unitOfWork.PackageType.GetAllPackageTypes();
                if (packageTypes is null || !packageTypes.Any())
                {
                    response.Message = "The PackageType list is empty";
                    response.Success = true;
                    return response;
                }
                // Sắp xếp danh sách PackageType theo yêu cầu
                var packageTypeDTO = _mapper.Map<List<PackageTypeResponse>>(packageTypes);
                if (sortOrder == "desc")
                {
                    packageTypeDTO = packageTypeDTO.OrderByDescending(r => r.PackageTypeId).ToList();
                }
                else
                {
                    packageTypeDTO = packageTypeDTO.OrderBy(r => r.PackageTypeId).ToList();
                }
                response.Data = packageTypeDTO;
                response.Message = "List PackageTypes";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<PackageTypeResponse>> GetPackageTypeById(int id)
        {
            var response = new DataResponse<PackageTypeResponse>();

            try
            {
                var packageType = await _unitOfWork.PackageType.GetPackageTypeById(id);
                if (packageType is null)
                {
                    throw new Exception("The PackageType does not exist");
                }
                response.Data = _mapper.Map<PackageTypeResponse>(packageType);
                response.Message = $"PackageTypeId {packageType.PackageTypeId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<PackageTypeResponse>>> SearchPackageTypes(string? name, string sortOrder)
        {
            var response = new DataResponse<List<PackageTypeResponse>>();

            try
            {
                var packageTypes = await _unitOfWork.PackageType.SearchPackageTypes(name);
                if (packageTypes is null || packageTypes.Count == 0)
                {
                    response.Message = "No PackageType found matching the criteria";
                    response.Success = true;
                }
                else
                {
                    var vioTypeDTO = _mapper.Map<List<PackageTypeResponse>>(packageTypes);

                    // Thực hiện sắp xếp
                    if (sortOrder == "desc")
                    {
                        vioTypeDTO = vioTypeDTO.OrderByDescending(p => p.PackageTypeId).ToList();
                    }
                    else
                    {
                        vioTypeDTO = vioTypeDTO.OrderBy(p => p.PackageTypeId).ToList();
                    }

                    response.Data = vioTypeDTO;
                    response.Message = "PackageTypes found";
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

        public async Task<DataResponse<PackageTypeResponse>> UpdatePackageType(int id, PackageTypeRequest request)
        {
            var response = new DataResponse<PackageTypeResponse>();

            try
            {
                var packageType = _unitOfWork.PackageType.GetById(id);
                if (packageType is null)
                {
                    response.Message = "Can not found PackageType";
                    response.Success = false;
                    return response;
                }
                packageType.Name = request.Name;
                packageType.Description = request.Description;

                _unitOfWork.PackageType.Update(packageType);
                _unitOfWork.Save();

                response.Data = _mapper.Map<PackageTypeResponse>(packageType);
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
