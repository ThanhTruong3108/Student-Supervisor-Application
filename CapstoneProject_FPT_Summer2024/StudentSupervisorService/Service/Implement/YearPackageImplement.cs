using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.YearPackageRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.YearPackageResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentSupervisorService.Service.Implement
{
    public class YearPackageImplement : YearPackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public YearPackageImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DataResponse<ResponseOfYearPackage>> CreateYearPackage(RequestOfYearPackage request)
        {
            var response = new DataResponse<ResponseOfYearPackage>();

            try
            {
                var createYearPackage = _mapper.Map<YearPackage>(request);
                createYearPackage.Status = YearPackageEnum.ACTIVE.ToString();
                _unitOfWork.YearPackage.Add(createYearPackage);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfYearPackage>(createYearPackage);
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

        public async Task DeleteYearPackage(int id)
        {
            var yearPackage = _unitOfWork.YearPackage.GetById(id);
            if (yearPackage is null)
            {
                throw new Exception("Can not found by" + id);
            }
            yearPackage.Status = YearPackageEnum.INACTIVE.ToString();

            _unitOfWork.YearPackage.Update(yearPackage);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<List<ResponseOfYearPackage>>> GetAllYearPackages(int page, int pageSize, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfYearPackage>>();

            try
            {
                var yearPackages = await _unitOfWork.YearPackage.GetAllYearPackages();
                if (yearPackages is null)
                {
                    response.Message = "The YearPackage list is empty";
                    response.Success = true;
                }

                // Sắp xếp danh sách Violation theo yêu cầu
                var yearPackageDTO = _mapper.Map<List<ResponseOfYearPackage>>(yearPackages);
                if (sortOrder == "desc")
                {
                    yearPackageDTO = yearPackageDTO.OrderByDescending(r => r.YearPackageId).ToList();
                }
                else
                {
                    yearPackageDTO = yearPackageDTO.OrderBy(r => r.YearPackageId).ToList();
                }

                // Thực hiện phân trang
                var startIndex = (page - 1) * pageSize;
                var pageYearPackages = yearPackageDTO.Skip(startIndex).Take(pageSize).ToList();

                response.Data = pageYearPackages;
                response.Message = "List YearPackages";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ResponseOfYearPackage>> GetYearPackageById(int id)
        {
            var response = new DataResponse<ResponseOfYearPackage>();

            try
            {
                var yearPackage = await _unitOfWork.YearPackage.GetYearPackageById(id);
                if (yearPackage is null)
                {
                    throw new Exception("The YearPackage does not exist");
                }
                response.Data = _mapper.Map<ResponseOfYearPackage>(yearPackage);
                response.Message = $"YearPackageId {yearPackage.YearPackageId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ResponseOfYearPackage>>> SearchYearPackages(int? schoolYearId, int? packageId, int? minNumberOfStudent, int? maxNumberOfStudent, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfYearPackage>>();

            try
            {
                var yearPackages = await _unitOfWork.YearPackage.SearchYearPackages(schoolYearId, packageId, minNumberOfStudent, maxNumberOfStudent);
                if (yearPackages is null || yearPackages.Count == 0)
                {
                    response.Message = "No YearPackage found matching the criteria";
                    response.Success = true;
                }
                else
                {
                    var yearPackageDTO = _mapper.Map<List<ResponseOfYearPackage>>(yearPackages);

                    // Thực hiện sắp xếp
                    if (sortOrder == "desc")
                    {
                        yearPackageDTO = yearPackageDTO.OrderByDescending(p => p.YearPackageId).ToList();
                    }
                    else
                    {
                        yearPackageDTO = yearPackageDTO.OrderBy(p => p.YearPackageId).ToList();
                    }

                    response.Data = yearPackageDTO;
                    response.Message = "YearPackages found";
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

        public async Task<DataResponse<ResponseOfYearPackage>> UpdateYearPackage(int id, RequestOfYearPackage request)
        {
            var response = new DataResponse<ResponseOfYearPackage>();

            try
            {
                var yearPackage = _unitOfWork.YearPackage.GetById(id);
                if (yearPackage is null)
                {
                    response.Message = "Can not found YearPackage";
                    response.Success = false;
                    return response;
                }

                yearPackage.SchoolYearId = request.SchoolYearId;
                yearPackage.PackageId = request.PackageId;
                yearPackage.NumberOfStudent = request.NumberOfStudent;
       

                _unitOfWork.YearPackage.Update(yearPackage);
                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfYearPackage>(yearPackage);
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
