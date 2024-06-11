using AutoMapper;
using Azure.Core;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.CloudinaryConfig;
using StudentSupervisorService.Models.Request.ViolationRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ViolationResponse;
using System.Net;
using static System.Net.Mime.MediaTypeNames;


namespace StudentSupervisorService.Service.Implement
{
    public class ViolationImplement : ViolationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ImageUrlService _imageUrlService;
        public ViolationImplement(IUnitOfWork unitOfWork, IMapper mapper, ImageUrlService imageUrlService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageUrlService = imageUrlService;
        }
        public async Task<DataResponse<List<ResponseOfViolation>>> GetAllViolations(string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfViolation>>();

            try
            {
                var violations = await _unitOfWork.Violation.GetAllViolations();
                if (violations is null || !violations.Any())
                {
                    response.Message = "The Violation list is empty";
                    response.Success = true;
                    return response;
                }
                // Sắp xếp danh sách Violation theo yêu cầu
                var vioDTO = _mapper.Map<List<ResponseOfViolation>>(violations);
                if (sortOrder == "desc")
                {
                    vioDTO = vioDTO.OrderByDescending(r => r.Code).ToList();
                }
                else
                {
                    vioDTO = vioDTO.OrderBy(r => r.Code).ToList();
                }
                response.Data = vioDTO;
                response.Message = "List Violations";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ResponseOfViolation>> GetViolationById(int id)
        {
            var response = new DataResponse<ResponseOfViolation>();

            try
            {
                var violation = await _unitOfWork.Violation.GetViolationById(id);
                if (violation is null)
                {
                    throw new Exception("The Violation does not exist");
                }
                response.Data = _mapper.Map<ResponseOfViolation>(violation);
                response.Message = $"ViolationId {violation.ViolationId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ResponseOfViolation>>> SearchViolations(int? classId, int? teacherId, int? vioTypeId, string? code, string? name, DateTime? date, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfViolation>>();

            try
            {
                var violations = await _unitOfWork.Violation.SearchViolations(classId, teacherId, vioTypeId, code, name, date);
                if (violations is null || violations.Count == 0)
                {
                    response.Message = "No Violation found matching the criteria";
                    response.Success = true;
                }
                else
                {
                    var violationDTO = _mapper.Map<List<ResponseOfViolation>>(violations);

                    // Thực hiện sắp xếp
                    if (sortOrder == "desc")
                    {
                        violationDTO = violationDTO.OrderByDescending(p => p.Code).ToList();
                    }
                    else
                    {
                        violationDTO = violationDTO.OrderBy(p => p.Code).ToList();
                    }

                    response.Data = violationDTO;
                    response.Message = "Violations found";
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

        public async Task<DataResponse<ResponseOfViolation>> CreateViolationForStudentSupervisor(RequestOfCreateViolation request)
        {
            var response = new DataResponse<ResponseOfViolation>();
            try
            {
                // Mapping request to Violation entity
                var violationEntity = _mapper.Map<Violation>(request);
                violationEntity.CreatedAt = DateTime.Now;
                violationEntity.UpdatedAt = DateTime.Now;
                violationEntity.Status = ViolationEnum.PENDING.ToString();

                if (request.Images != null)
                {
                    var first2Images = request.Images.Take(2).ToList(); // just take first 2 images to upload
                    foreach (var image in first2Images)
                    {
                        // Upload image to cloudinary
                        var uploadResult = await _imageUrlService.UploadImage(image);
                        if (uploadResult.StatusCode == HttpStatusCode.OK)
                        {
                            violationEntity.ImageUrls.Add(new ImageUrl
                            {
                                ViolationId = violationEntity.ViolationId,
                                Url = uploadResult.SecureUrl.AbsoluteUri,
                                Name = uploadResult.PublicId,
                                Description = "Image of " + violationEntity.ViolationId + " Violation"
                            });
                        }
                    }
                }
                // Save Violation to database
                _unitOfWork.Violation.Add(violationEntity);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfViolation>(violationEntity);
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

        public async Task<DataResponse<ResponseOfViolation>> CreateViolationForSupervisor(RequestOfCreateViolation request)
        {
            var response = new DataResponse<ResponseOfViolation>();
            try
            {
                // Mapping request to Violation entity
                var violationEntity = _mapper.Map<Violation>(request);
                violationEntity.CreatedAt = DateTime.Now;
                violationEntity.UpdatedAt = DateTime.Now;
                violationEntity.Status = ViolationEnum.ACTIVE.ToString();

                if (request.Images != null)
                {
                    var first2Images = request.Images.Take(2).ToList(); // just take first 2 images to upload
                    foreach (var image in first2Images)
                    {
                        // Upload image to cloudinary
                        var uploadResult = await _imageUrlService.UploadImage(image);
                        if (uploadResult.StatusCode == HttpStatusCode.OK)
                        {
                            violationEntity.ImageUrls.Add(new ImageUrl
                            {
                                ViolationId = violationEntity.ViolationId,
                                Url = uploadResult.SecureUrl.AbsoluteUri,
                                Name = uploadResult.PublicId,
                                Description = "Image of " + violationEntity.ViolationId + " Violation"
                            });
                        }
                    }
                }
                // Save Violation to database
                _unitOfWork.Violation.Add(violationEntity);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfViolation>(violationEntity);
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

        public async Task<DataResponse<ResponseOfViolation>> UpdateViolation(int id, RequestOfUpdateViolation request)
        {
            var response = new DataResponse<ResponseOfViolation>();

            try
            {
                var violation = _unitOfWork.Violation.GetById(id);
                if (violation is null)
                {
                    response.Message = "Can not found Violation";
                    response.Success = false;
                    return response;
                }

                violation.ClassId = request.ClassId;
                violation.ViolationTypeId = request.ViolationTypeId;
                violation.TeacherId = request.TeacherId;
                violation.Code = request.Code;
                violation.Name = request.ViolationName;
                violation.Description = request.Description;
                violation.Date = request.Date;

                _unitOfWork.Violation.Update(violation);
                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfViolation>(violation);
                response.Success = true;
                response.Message = "Update Violation Successfully.";
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task DeleteViolation(int id)
        {
            var violation = _unitOfWork.Violation.GetById(id);
            if (violation is null)
            {
                throw new Exception("Can not found Violation by" + id);
            }
            violation.Status = SchoolYearEnum.INACTIVE.ToString();

            _unitOfWork.Violation.Update(violation);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<ResponseOfViolation>> ApproveViolation(int violationId)
        {
            var response = new DataResponse<ResponseOfViolation>();

            try
            {
                var violation = _unitOfWork.Violation.GetById(violationId);
                if (violation is null)
                {
                    response.Message = "Can not found Violation";
                    response.Success = false;
                    return response;
                }

                violation.Status = ViolationEnum.APPROVED.ToString();
                _unitOfWork.Violation.Update(violation);
                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfViolation>(violation);
                response.Success = true;
                response.Message = "Approve Violation Successfully.";
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<ResponseOfViolation>> RejectViolation(int violationId)
        {
            var response = new DataResponse<ResponseOfViolation>();

            try
            {
                var violation = _unitOfWork.Violation.GetById(violationId);
                if (violation is null)
                {
                    response.Message = "Can not found Violation";
                    response.Success = false;
                    return response;
                }

                violation.Status = ViolationEnum.REJECTED.ToString();
                _unitOfWork.Violation.Update(violation);
                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfViolation>(violation);
                response.Success = true;
                response.Message = "Reject Violation Successfully.";
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
