﻿using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.PackageRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.PackageResponse;


namespace StudentSupervisorService.Service.Implement
{
    public class PackageImplement : PackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PackageImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<ResponseOfPackage>> CreatePackage(PackageRequest request)
        {
            var response = new DataResponse<ResponseOfPackage>();

            try
            {
                var createPackage = _mapper.Map<Package>(request);
                createPackage.Status = PackageStatusEnums.ACTIVE.ToString();
                _unitOfWork.Package.Add(createPackage);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfPackage>(createPackage);
                response.Message = "Tạo thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Tạo thất bại.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<ResponseOfPackage>> DeletePackage(int id)
        {
            var response = new DataResponse<ResponseOfPackage>();
            try
            {
                var package = _unitOfWork.Package.GetById(id);
                if (package is null)
                {
                    response.Data = "Empty";
                    response.Message = "Không thể tìm thấy Gói có ID: " + id;
                    response.Success = false;
                    return response;
                }

                if (package.Status == PackageStatusEnums.INACTIVE.ToString())
                {
                    response.Data = "Empty";
                    response.Message = "Gói đã bị xóa !";
                    response.Success = false;
                    return response;
                }

                package.Status = PackageStatusEnums.INACTIVE.ToString();
                _unitOfWork.Package.Update(package);
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
            return response;
        }

        public async Task<DataResponse<List<ResponseOfPackage>>> GetAllPackages(string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfPackage>>();

            try
            {
                var package = await _unitOfWork.Package.GetAllPackages();
                if (package is null || !package.Any())
                {
                    response.Message = "Danh sách Gói trống!!";
                    response.Success = true;
                    return response;
                }
                // Sắp xếp danh sách Package theo yêu cầu
                var packageDTO = _mapper.Map<List<ResponseOfPackage>>(package);
                if (sortOrder == "desc")
                {
                    packageDTO = packageDTO.OrderByDescending(r => r.PackageId).ToList();
                }
                else
                {
                    packageDTO = packageDTO.OrderBy(r => r.PackageId).ToList();
                }
                response.Data = packageDTO;
                response.Message = "Danh sách các Gói";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ResponseOfPackage>> GetPackageById(int id)
        {
            var response = new DataResponse<ResponseOfPackage>();

            try
            {
                var package = await _unitOfWork.Package.GetPackageById(id);
                if (package is null)
                {
                    throw new Exception("Gói không tồn tại");
                }
                response.Data = _mapper.Map<ResponseOfPackage>(package);
                response.Message = $"PackageId {package.PackageId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ResponseOfPackage>> UpdatePackage(int id, PackageRequest request)
        {
            var response = new DataResponse<ResponseOfPackage>();

            try
            {
                var package = _unitOfWork.Package.GetById(id);
                if (package is null)
                {
                    response.Message = "Không tìm thấy Gói Package";
                    response.Success = false;
                    return response;
                }

                if (package.Status == PackageStatusEnums.INACTIVE.ToString())
                {
                    response.Data = "Empty";
                    response.Message = "Không thể cập nhật Gói đã bị xóa !";
                    response.Success = false;
                    return response;
                }

                package.Name = request.Name;
                package.Description = request.Description ?? package.Description;
                package.Price = request.Price ?? package.Price;

                _unitOfWork.Package.Update(package);
                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfPackage>(package);
                response.Success = true;
                response.Message = "Cập nhật thành công";
            }
            catch (Exception ex)
            {
                response.Message = "Cập nhật thất bại.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
