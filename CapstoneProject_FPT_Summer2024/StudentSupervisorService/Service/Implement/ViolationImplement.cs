using AutoMapper;
using Azure.Core;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.CloudinaryConfig;
using StudentSupervisorService.Models.Request.ViolationRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ClassGroupResponse;
using StudentSupervisorService.Models.Response.DisciplineResponse;
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
                    vioDTO = vioDTO.OrderByDescending(r => r.ViolationId).ToList();
                }
                else
                {
                    vioDTO = vioDTO.OrderBy(r => r.ViolationId).ToList();
                }
                response.Data = vioDTO;
                response.Message = "List Violations";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
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
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ResponseOfViolation>>> SearchViolations(
                int? classId,
                int? violationTypeId,
                int? studentInClassId,
                int? teacherId,
                string? name,
                string? description,
                DateTime? date,
                string? status, 
                string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfViolation>>();

            try
            {
                var violations = await _unitOfWork.Violation.SearchViolations(classId, violationTypeId, studentInClassId, teacherId, name, description, date, status);
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
                        violationDTO = violationDTO.OrderByDescending(p => p.ViolationId).ToList();
                    }
                    else
                    {
                        violationDTO = violationDTO.OrderBy(p => p.ViolationId).ToList();
                    }

                    response.Data = violationDTO;
                    response.Message = "Violations found";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Something went wrong.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
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
                var violationEntity = new Violation
                {
                    ClassId = request.ClassId,
                    ViolationTypeId = request.ViolationTypeId,
                    StudentInClassId = request.StudentInClassId,
                    TeacherId = request.TeacherId,
                    Name = request.ViolationName,
                    Description = request.Description,
                    Date = request.Date,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = ViolationStatusEnums.PENDING.ToString()
                };

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
                await _unitOfWork.Violation.CreateViolation(violationEntity);

                response.Data = _mapper.Map<ResponseOfViolation>(violationEntity);
                response.Message = "Create Successfully.";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
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
                var violationEntity = new Violation
                {
                    ClassId = request.ClassId,
                    ViolationTypeId = request.ViolationTypeId,
                    StudentInClassId = request.StudentInClassId,
                    TeacherId = request.TeacherId,
                    Name = request.ViolationName,
                    Description = request.Description,
                    Date = request.Date,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = ViolationStatusEnums.APPROVED.ToString()
                };

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
                await _unitOfWork.Violation.CreateViolation(violationEntity);

                response.Data = _mapper.Map<ResponseOfViolation>(violationEntity);
                response.Message = "Create Successfully.";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
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
                if (violation == null)
                {
                    response.Data = "Empty";
                    response.Message = "Violation not found";
                    response.Success = false;
                    return response;
                }

                //existingViolation.Class.ClassId = request.ClassId ?? existingViolation.ClassId;
                violation.ViolationTypeId = request.ViolationTypeId;
                violation.StudentInClassId = request.StudentInClassId;
                violation.TeacherId = request.TeacherId;
                violation.Name = request.ViolationName;
                violation.Description = request.Description;
                violation.Date = request.Date;
                violation.UpdatedAt = DateTime.Now;

                _unitOfWork.Violation.Update(violation);
                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfViolation>(violation);
                response.Message = "Violation updated successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Violation Discipline failed: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task DeleteViolation(int id)
        {
            var violation = _unitOfWork.Violation.GetById(id);
            if (violation is null)
            {
                throw new Exception("Can not found by" + id);
            }
            violation.Status = ViolationStatusEnums.INACTIVE.ToString();

            _unitOfWork.Violation.Update(violation);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<ResponseOfViolation>> ApproveViolation(int violationId)
        {
            var response = new DataResponse<ResponseOfViolation>();

            try
            {
                var violation = await _unitOfWork.Violation.GetViolationById(violationId);
                if (violation is null)
                {
                    response.Message = "Can not found Violation";
                    response.Success = false;
                    return response;
                }

                violation.Status = ViolationStatusEnums.APPROVED.ToString();
                await _unitOfWork.Violation.UpdateViolation(violation);

                response.Data = _mapper.Map<ResponseOfViolation>(violation);
                response.Success = true;
                response.Message = "Approve Violation Successfully.";
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<ResponseOfViolation>> RejectViolation(int violationId)
        {
            var response = new DataResponse<ResponseOfViolation>();

            try
            {
                var violation = await _unitOfWork.Violation.GetViolationById(violationId);
                if (violation is null)
                {
                    response.Message = "Can not found Violation";
                    response.Success = false;
                    return response;
                }

                violation.Status = ViolationStatusEnums.REJECTED.ToString();
                await _unitOfWork.Violation.UpdateViolation(violation);

                response.Data = _mapper.Map<ResponseOfViolation>(violation);
                response.Success = true;
                response.Message = "Approve Violation Successfully.";
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }
    }
}
