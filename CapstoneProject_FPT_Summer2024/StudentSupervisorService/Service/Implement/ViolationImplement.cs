﻿using AutoMapper;
using Azure.Core;
using Domain.Entity;
using Domain.Entity.DTO;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentSupervisorService.Models.Request.ViolationRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ViolationResponse;
using System.Net;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;


namespace StudentSupervisorService.Service.Implement
{
    public class ViolationImplement : ViolationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ImageUrlService _imageUrlService;
        private readonly ValidationService _validationService;
        public ViolationImplement(IUnitOfWork unitOfWork, IMapper mapper, ImageUrlService imageUrlService, ValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageUrlService = imageUrlService;
            _validationService = validationService;
        }
        public async Task<DataResponse<List<ResponseOfViolation>>> GetAllViolations(string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfViolation>>();

            try
            {
                var violations = await _unitOfWork.Violation.GetAllViolations();
                if (violations is null || !violations.Any())
                {
                    response.Data = "Empty";
                    response.Message = "Danh sách vi phạm trống";
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
                response.Message = "Danh sách các vi phạm";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByClassId(int classId, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfViolation>>();

            try
            {
                var violations = await _unitOfWork.Violation.GetViolationsByClassId(classId);
                if (violations == null || !violations.Any())
                {
                    response.Data = "Empty";
                    response.Message = "Không có vi phạm nào cho lớp này";
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
                response.Message = "Danh sách các vi phạm cho lớp";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
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
                    response.Data = "Empty";
                    response.Message = "Vi phạm không tồn tại";
                    response.Success = false;
                    throw new Exception("Vi phạm không tồn tại");
                }
                response.Data = _mapper.Map<ResponseOfViolation>(violation);
                response.Message = $"ViolationId {violation.ViolationId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ResponseOfViolation>> CreateViolationForStudentSupervisor(int userId, RequestOfStuSupervisorCreateViolation request)
        {
            var response = new DataResponse<ResponseOfViolation>();
            try
            {
                // validate trong năm đó trường có đăng ký gói VALID nào ko
                if (!await _validationService.IsAnyValidPackageInSpecificYear(request.SchoolId, request.Year))
                {
                    response.Data = "Empty";
                    response.Message = "Trường chưa đăng ký gói nào hoặc không có gói nào còn hạn";
                    response.Success = false;
                    return response;
                }

                var schoolYear = await _unitOfWork.SchoolYear.GetYearBySchoolYearId(request.SchoolId, request.Year);
                if (schoolYear == null)
                {
                    response.Message = "Niên khóa không tồn tại hoặc không thuộc về trường được đăng ký.";
                    response.Success = false;
                    return response;
                }

                // Validate the violation date
                if (request.Date < schoolYear.StartDate || request.Date > schoolYear.EndDate)
                {
                    response.Message = "Thời gian vi phạm không nằm trong khoảng thời gian của niên khóa.";
                    response.Success = false;
                    return response;
                }

                // Validate the class belongs to the school and school year
                var classEntity = await _unitOfWork.Class.GetClassById(request.ClassId);
                if (classEntity == null || classEntity.SchoolYearId != schoolYear.SchoolYearId || classEntity.SchoolYear.SchoolId != request.SchoolId)
                {
                    response.Message = "Lớp học không thuộc niên khóa hoặc trường được đăng ký.";
                    response.Success = false;
                    return response;
                }

                // Validate the ViolationType belongs to the school
                var violationType = await _unitOfWork.ViolationType.GetVioTypeById(request.ViolationTypeId);
                if (violationType == null || violationType.ViolationGroup.SchoolId != request.SchoolId)
                {
                    response.Message = "Loại vi phạm không thuộc trường được chỉ định.";
                    response.Success = false;
                    return response;
                }

                // Validate the ViolationType status
                if (violationType.Status != ViolationTypeStatusEnums.ACTIVE.ToString())
                {
                    response.Message = "Loại vi phạm không còn được thực thi.";
                    response.Success = false;
                    return response;
                }

                // Validate patrol schedule for StudentSupervisor
                var patrolSchedule = await _unitOfWork.PatrolSchedule.GetPatrolScheduleById(request.ScheduleId);
                if (patrolSchedule == null)
                {
                    response.Message = "Lịch trực không hợp lệ.";
                    response.Success = false;
                    return response;
                }

                if (patrolSchedule.ClassId != request.ClassId)
                {
                    response.Message = "Lịch trực không thuộc về lớp được chỉ định.";
                    response.Success = false;
                    return response;
                }

                if (request.Date < patrolSchedule.From || request.Date > patrolSchedule.To)
                {
                    response.Message = "Thời gian vi phạm không nằm trong lịch trực.";
                    response.Success = false;
                    return response;
                }

                // Ko cho tạo violation nếu quá 1 giờ so với thời gian trong lịch trực
                var isOver1Hour = await _validationService.IsViolationCreatedOver1HourFromTimeInPatrolSchedule(request.ScheduleId);
                if (isOver1Hour)
                {
                    response.Data = "Empty";
                    response.Message = "Không thể tạo vi phạm sau 1 giờ từ lúc bắt đầu lịch trực";
                    response.Success = false;
                    return response;
                }

                // Validate Chỉ Sao đỏ được phân công trong lịch trực mới có quyền ghi nhận vi phạm cho lịch trực đó
                var studentSupervisor = await _unitOfWork.StudentSupervisor.GetStudentSupervisorByUserId(userId);
                if (studentSupervisor == null || !studentSupervisor.PatrolSchedules.Any(s => s.ScheduleId == request.ScheduleId))
                {
                    response.Message = "Sao đỏ không có quyền tạo vi phạm cho lịch trực này.";
                    response.Success = false;
                    return response;
                }

                // Mapping request to Violation entity
                var violationEntity = new Violation
                {
                    UserId = userId,
                    ClassId = request.ClassId,
                    ViolationTypeId = request.ViolationTypeId,
                    StudentInClassId = request.StudentInClassId,
                    ScheduleId = request.ScheduleId,
                    Name = request.ViolationName,
                    Description = request.Description,
                    Date = request.Date,
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
                                PublicId = uploadResult.PublicId,
                                Url = uploadResult.SecureUrl.AbsoluteUri,
                                Name = uploadResult.PublicId,
                                Description = "Hình ảnh của " + violationEntity.ViolationId + " vi phạm"
                            });
                        }
                    }
                }

                // Save Violation to database
                await _unitOfWork.Violation.CreateViolation(violationEntity);

                response.Data = _mapper.Map<ResponseOfViolation>(violationEntity);
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

        public async Task<DataResponse<ResponseOfViolation>> CreateViolationForSupervisor(int userId, RequestOfSupervisorCreateViolation request)
        {
            var response = new DataResponse<ResponseOfViolation>();
            try
            {
                // validate trong năm đó trường có đăng ký gói VALID nào ko
                if (!await _validationService.IsAnyValidPackageInSpecificYear(request.SchoolId, request.Year))
                {
                    response.Data = "Empty";
                    response.Message = "Trường chưa đăng ký gói nào hoặc không có gói nào còn hạn";
                    response.Success = false;
                    return response;
                }

                var schoolYear = await _unitOfWork.SchoolYear.GetYearBySchoolYearId(request.SchoolId, request.Year);
                if (schoolYear == null)
                {
                    response.Message = "Niên khóa không tồn tại hoặc không thuộc về trường được đăng ký.";
                    response.Success = false;
                    return response;
                }

                // Validate chỉ ghi nhận vi phạm trong niên khóa 
                if (request.Date < schoolYear.StartDate || request.Date > schoolYear.EndDate)
                {
                    response.Message = "Thời gian vi phạm không nằm trong khoảng thời gian của niên khóa.";
                    response.Success = false;
                    return response;
                }

                // Validate vi phạm thuộc về lớp có trong niên khóa
                var classEntity = await _unitOfWork.Class.GetClassById(request.ClassId);
                if (classEntity == null || classEntity.SchoolYearId != schoolYear.SchoolYearId || classEntity.SchoolYear.SchoolId != request.SchoolId)
                {
                    response.Message = "Lớp học không thuộc niên khóa hoặc trường được đăng ký.";
                    response.Success = false;
                    return response;
                }

                // Validate coi loại vi phạm có nằm trong nội quy trường không
                var violationType = await _unitOfWork.ViolationType.GetVioTypeById(request.ViolationTypeId);
                if (violationType == null || violationType.ViolationGroup.SchoolId != request.SchoolId)
                {
                    response.Message = "Loại vi phạm không thuộc trường được chỉ định.";
                    response.Success = false;
                    return response;
                }

                // Validate the ViolationType status
                if (violationType.Status != ViolationTypeStatusEnums.ACTIVE.ToString())
                {
                    response.Message = "Loại vi phạm không còn được thực thi.";
                    response.Success = false;
                    return response;
                }

                // Lấy VioConfig tương ứng với Viotype và kiểm tra VioConfig có hợp lệ không
                var violationConfig = await _unitOfWork.ViolationConfig.GetConfigByViolationTypeId(request.ViolationTypeId);
                if (violationConfig == null || violationConfig.Status == ViolationConfigStatusEnums.INACTIVE.ToString())
                {
                    response.Message = "Cấu hình vi phạm không hợp lệ.";
                    response.Success = false;
                    return response;
                }

                // Mapping request to Violation entity
                var violationEntity = new Violation
                {
                    UserId = userId,
                    ClassId = request.ClassId,
                    ViolationTypeId = request.ViolationTypeId,
                    StudentInClassId = request.StudentInClassId,
                    Name = request.ViolationName,
                    Description = request.Description,
                    Date = request.Date,
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
                                PublicId = uploadResult.PublicId,
                                Url = uploadResult.SecureUrl.AbsoluteUri,
                                Name = uploadResult.PublicId,
                                Description = "Hình ảnh của " + violationEntity.ViolationId + " vi phạm"
                            });
                        }
                    }
                }

                // Save Violation to database
                await _unitOfWork.Violation.CreateViolation(violationEntity);

                // Tạo Discipline cho Violation tương ứng
                var disciplineEntity = new Discipline
                {
                    ViolationId = violationEntity.ViolationId,
                    PennaltyId = null,
                    Description = violationEntity.Name,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7), // Set default EndDate
                    Status = DisciplineStatusEnums.PENDING.ToString()
                };

                await _unitOfWork.Discipline.CreateDiscipline(disciplineEntity);

                // Increase NumberOfViolation in StudentInClass
                var studentInClass = _unitOfWork.StudentInClass.GetById(violationEntity.StudentInClassId.Value);
                if (studentInClass != null)
                {
                    studentInClass.NumberOfViolation = (studentInClass.NumberOfViolation ?? 0) + 1;
                    _unitOfWork.StudentInClass.Update(studentInClass);
                    _unitOfWork.Save();
                }

                // Trừ điểm của lớp đó dựa trên VioConfig tương ứng
                if (violationConfig.MinusPoints.HasValue)
                {
                    classEntity.TotalPoint -= violationConfig.MinusPoints.Value;
                    _unitOfWork.Class.Update(classEntity);
                }

                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfViolation>(violationEntity);
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

        public async Task<DataResponse<ResponseOfViolation>> UpdateViolationForStudentSupervisor(int id, RequestOfUpdateViolationForStudentSupervisor request)
        {
            var response = new DataResponse<ResponseOfViolation>();
            try
            {
                var violation = await _unitOfWork.Violation.GetByIdWithImages(id);
                if (violation == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy vi phạm!!";
                    response.Success = false;
                    return response;
                }

                if (violation.Status.Equals(ViolationStatusEnums.INACTIVE.ToString()))
                {
                    response.Data = "Empty";
                    response.Message = "Vi phạm đã bị xóa, không thể cập nhật";
                    response.Success = false;
                    return response;
                }

                if (violation.Status != ViolationStatusEnums.PENDING.ToString())
                {
                    response.Data = "Empty";
                    response.Message = "Vi phạm đã được chấp thuận, không thể cập nhật";
                    response.Success = false;
                    return response;
                }

                // Validate patrol schedule for StudentSupervisor
                var patrolSchedule = await _unitOfWork.PatrolSchedule.GetPatrolScheduleById(request.ScheduleId);
                if (patrolSchedule == null)
                {
                    response.Message = "Lịch trực không hợp lệ.";
                    response.Success = false;
                    return response;
                }

                if (patrolSchedule.ClassId != request.ClassId)
                {
                    response.Message = "Lịch trực không thuộc về lớp được chỉ định.";
                    response.Success = false;
                    return response;
                }

                if (request.Date < patrolSchedule.From || request.Date > patrolSchedule.To)
                {
                    response.Message = "Thời gian vi phạm không nằm trong lịch trực.";
                    response.Success = false;
                    return response;
                }

                // Update the violation details
                violation.UserId = request.UserId;
                violation.ClassId = request.ClassId;
                violation.ViolationTypeId = request.ViolationTypeId;
                violation.StudentInClassId = request.StudentInClassId;
                violation.ScheduleId = request.ScheduleId;
                violation.Name = request.ViolationName;
                violation.Description = request.Description;
                violation.Date = request.Date;
                violation.UpdatedAt = DateTime.Now;
                // update hình ảnh nếu có
                if (request.Images != null)
                {
                    // xóa hình ảnh cũ
                    foreach (var imageUrl in violation.ImageUrls)
                    {
                        if (imageUrl.PublicId != null)
                        {
                            var deleteResult = await _imageUrlService.DeleteImage(imageUrl.PublicId);
                            if (deleteResult.StatusCode != HttpStatusCode.OK)
                            {
                                await Console.Out.WriteLineAsync("Lỗi khi xóa hình ảnh ở UpdateViolation");
                                throw new Exception($"Failed to delete image with public ID {imageUrl.Name}");
                            }
                        }
                    }
                    // xóa ảnh cũ của violation
                    await _unitOfWork.ImageUrl.DeleteImagesByViolationId(violation.ViolationId);
                    violation.ImageUrls.Clear();

                    // upload ảnh mới
                    var first2Images = request.Images.Take(2).ToList(); // just take the first 2 images to upload
                    foreach (var image in first2Images)
                    {
                        // upload ảnh lên Cloudinary
                        var uploadResult = await _imageUrlService.UploadImage(image);
                        if (uploadResult.StatusCode == HttpStatusCode.OK)
                        {
                            violation.ImageUrls.Add(new ImageUrl
                            {
                                ViolationId = violation.ViolationId,
                                PublicId = uploadResult.PublicId,
                                Url = uploadResult.SecureUrl.AbsoluteUri,
                                Name = uploadResult.PublicId,
                                Description = "Hình ảnh của " + violation.ViolationId + " vi phạm"
                            });
                        }
                        else
                        {
                            throw new Exception($"Failed to upload image {image.FileName}");
                        }
                    }
                }

                _unitOfWork.Violation.Update(violation);
                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfViolation>(violation);
                response.Message = "Cập nhật thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Cập nhật thất bại.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<ResponseOfViolation>> UpdateViolationForSupervisor(int id, RequestOfUpdateViolationForSupervisor request)
        {
            var response = new DataResponse<ResponseOfViolation>();
            try
            {
                var violation = _unitOfWork.Violation.GetById(id);
                if (violation == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy vi phạm!!";
                    response.Success = false;
                    return response;
                }

                if (violation.Status.Equals(ViolationStatusEnums.INACTIVE.ToString()))
                {
                    response.Data = "Empty";
                    response.Message = "Vi phạm đã bị xóa, không thể cập nhật";
                    response.Success = false;
                    return response;
                }

                // Store old ViolationTypeId and get old ViolationConfig
                var oldViolationTypeId = violation.ViolationTypeId;
                var oldViolationConfig = await _unitOfWork.ViolationConfig.GetConfigByViolationTypeId(oldViolationTypeId);

                // Update the violation details
                violation.UserId = request.UserId;
                violation.ClassId = request.ClassId;
                violation.ViolationTypeId = request.ViolationTypeId;
                violation.StudentInClassId = request.StudentInClassId;
                violation.Name = request.ViolationName;
                violation.Description = request.Description;
                violation.Date = request.Date;
                violation.UpdatedAt = DateTime.Now;
                // update hình ảnh nếu có
                if (request.Images != null)
                {
                    // xóa hình ảnh cũ
                    foreach (var imageUrl in violation.ImageUrls)
                    {
                        if (imageUrl.PublicId != null)
                        {
                            var deleteResult = await _imageUrlService.DeleteImage(imageUrl.PublicId);
                            if (deleteResult.StatusCode != HttpStatusCode.OK)
                            {
                                await Console.Out.WriteLineAsync("Lỗi khi xóa hình ảnh ở UpdateViolation");
                                throw new Exception($"Failed to delete image with public ID {imageUrl.Name}");
                            }
                        }
                    }
                    // xóa ảnh cũ của violation
                    await _unitOfWork.ImageUrl.DeleteImagesByViolationId(violation.ViolationId);
                    violation.ImageUrls.Clear();

                    // upload ảnh mới
                    var first2Images = request.Images.Take(2).ToList(); // just take the first 2 images to upload
                    foreach (var image in first2Images)
                    {
                        // upload ảnh lên Cloudinary
                        var uploadResult = await _imageUrlService.UploadImage(image);
                        if (uploadResult.StatusCode == HttpStatusCode.OK)
                        {
                            violation.ImageUrls.Add(new ImageUrl
                            {
                                ViolationId = violation.ViolationId,
                                PublicId = uploadResult.PublicId,
                                Url = uploadResult.SecureUrl.AbsoluteUri,
                                Name = uploadResult.PublicId,
                                Description = "Hình ảnh của " + violation.ViolationId + " vi phạm"
                            });
                        }
                        else
                        {
                            throw new Exception($"Failed to upload image {image.FileName}");
                        }
                    }
                }

                _unitOfWork.Violation.Update(violation);

                // Cập nhật TotalPoint của Lớp dựa trên sự thay đổi trong ViolationType
                var newViolationConfig = await _unitOfWork.ViolationConfig.GetConfigByViolationTypeId(violation.ViolationTypeId);
                var classEntity = await _unitOfWork.Class.GetClassById(violation.ClassId);

                if (classEntity != null)
                {
                    // Điều chỉnh TotalPoint dựa trên sự khác biệt trong MinusPoints
                    var oldMinusPoints = oldViolationConfig?.MinusPoints ?? 0;
                    var newMinusPoints = newViolationConfig?.MinusPoints ?? 0;

                    classEntity.TotalPoint += oldMinusPoints - newMinusPoints;

                    _unitOfWork.Class.Update(classEntity);
                }

                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfViolation>(violation);
                response.Message = "Cập nhật thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Cập nhật thất bại.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<ResponseOfViolation>> DeleteViolation(int id)
        {
            var response = new DataResponse<ResponseOfViolation>();
            try
            {
                var violation = _unitOfWork.Violation.GetById(id);
                if (violation is null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy vi phạm!!";
                    response.Success = false;
                    return response;
                }

                if (violation.Status == ViolationStatusEnums.INACTIVE.ToString())
                {
                    response.Data = "Empty";
                    response.Message = "Vi phạm đã bị xóa";
                    response.Success = false;
                    return response;
                }

                // kiểm tra xem Vi phạm đó có ở status pending không
                if (violation.Status != ViolationStatusEnums.PENDING.ToString())
                {
                    response.Data = "Empty";
                    response.Message = "Chỉ những vi phạm đang Chờ xử lý mới được xóa";
                    response.Success = false;
                    return response;
                }

                violation.UpdatedAt = DateTime.Now;
                violation.Status = ViolationStatusEnums.INACTIVE.ToString();
                _unitOfWork.Violation.Update(violation);
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

        public async Task<DataResponse<ResponseOfViolation>> ApproveViolation(int violationId)
        {
            var response = new DataResponse<ResponseOfViolation>();

            try
            {
                var violation = await _unitOfWork.Violation.GetViolationById(violationId);
                if (violation == null)
                {
                    response.Message = "Không thể tìm thấy vi phạm";
                    response.Success = false;
                    return response;
                }

                if (violation.Status == ViolationStatusEnums.APPROVED.ToString())
                {
                    response.Message = "Vi phạm đã được Duyệt";
                    response.Success = false;
                    return response;
                }

                if (violation.Status != ViolationStatusEnums.PENDING.ToString())
                {
                    response.Message = "Chỉ những vi phạm đang Chờ xử lý mới có thể được Duyệt";
                    response.Success = false;
                    return response;
                }

                // Kiểm tra xem Discipline tương ứng với Vioation đó đã được tạo chưa
                var discipline = await _unitOfWork.Discipline.GetDisciplineByViolationId(violation.ViolationId);

                if (violation.Status == ViolationStatusEnums.PENDING.ToString())
                {
                    if (discipline == null)
                    {
                        // Nếu chưa có thì tạo mới một Discipline tương ứng với Violation đó
                        var disciplineEntity = new Discipline
                        {
                            ViolationId = violation.ViolationId,
                            PennaltyId = null,
                            Description = violation.Name,
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(7),
                            Status = DisciplineStatusEnums.PENDING.ToString()
                        };
                        await _unitOfWork.Discipline.CreateDiscipline(disciplineEntity);
                    }
                }
                //else if (violation.Status == ViolationStatusEnums.REJECTED.ToString())
                //{
                //    if (discipline != null)
                //    {
                //        // Nếu Discipline tương ứng với Violation đó đã được tạo nhưng Violation bị Rejected dẫn đến Status Discipline = INACTIVE
                //        // => Update lại Status của Discipline đó thành PENDING
                //        _unitOfWork.Discipline.DetachLocal(discipline, discipline.DisciplineId);
                //        discipline.Status = DisciplineStatusEnums.PENDING.ToString();
                //        await _unitOfWork.Discipline.UpdateDiscipline(discipline);
                //    }
                //}

                // Detach the existing tracked instance of the Violation entity
                _unitOfWork.Violation.DetachLocal(violation, violation.ViolationId);

                // Đồng thời cập nhật lại Status của Violation thành APPROVED
                violation.UpdatedAt = DateTime.Now;
                violation.Status = ViolationStatusEnums.APPROVED.ToString();
                _unitOfWork.Violation.Update(violation);

                // Increase NumberOfViolation in StudentInClass
                var studentInClass = await _unitOfWork.StudentInClass.GetStudentInClassById(violation.StudentInClassId.Value);
                if (studentInClass != null)
                {
                    _unitOfWork.StudentInClass.DetachLocal(studentInClass, studentInClass.StudentInClassId);
                    studentInClass.NumberOfViolation = (studentInClass.NumberOfViolation ?? 0) + 1;
                    _unitOfWork.StudentInClass.Update(studentInClass);
                }

                // Lấy VioConfig tương ứng với Viotype và kiểm tra VioConfig có hợp lệ không
                var violationConfig = await _unitOfWork.ViolationConfig.GetConfigByViolationTypeId(violation.ViolationTypeId);
                if (violationConfig == null || violationConfig.Status == ViolationConfigStatusEnums.INACTIVE.ToString())
                {
                    response.Message = "Cấu hình vi phạm không hợp lệ.";
                    response.Success = false;
                    return response;
                }

                // Trừ điểm của lớp đó dựa trên VioConfig tương ứng
                if (violationConfig.MinusPoints.HasValue)
                {
                    var classEntity = await _unitOfWork.Class.GetClassById(violation.ClassId);
                    if (classEntity != null)
                    {
                        _unitOfWork.Class.DetachLocal(classEntity, classEntity.ClassId);
                        classEntity.TotalPoint -= violationConfig.MinusPoints.Value;
                        _unitOfWork.Class.Update(classEntity);
                    }
                }

                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfViolation>(violation);
                response.Success = true;
                response.Message = "Đã phê duyệt vi phạm thành công";
            }
            catch (Exception ex)
            {
                response.Message = "Phê duyệt vi phạm thất bại.\n" + ex.Message
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
                    response.Message = "Không thể tìm thấy Vi phạm!!";
                    response.Success = false;
                    return response;
                }

                if (violation.Status == ViolationStatusEnums.REJECTED.ToString())
                {
                    response.Message = "Vi phạm đã bị Từ chối";
                    response.Success = false;
                    return response;
                }

                // Kiểm tra xem vi phạm hiện có ở trạng thái DISCUSSING hay không
                if (violation.Status == ViolationStatusEnums.DISCUSSING.ToString())
                {
                    // Detach the existing violation entity to avoid tracking issues
                    _unitOfWork.Violation.DetachLocal(violation, violation.ViolationId);

                    // Cập nhật Status vi phạm thành REJECTED
                    violation.Status = ViolationStatusEnums.REJECTED.ToString();
                    violation.UpdatedAt = DateTime.Now;
                    await _unitOfWork.Violation.UpdateViolation(violation);

                    // Cập nhật Discipline status thành INACTIVE
                    var discipline = await _unitOfWork.Discipline.GetDisciplineByViolationId(violation.ViolationId);
                    if (discipline != null)
                    {
                        _unitOfWork.Discipline.DetachLocal(discipline, discipline.DisciplineId);
                        discipline.Status = DisciplineStatusEnums.INACTIVE.ToString();
                        await _unitOfWork.Discipline.UpdateDiscipline(discipline);
                    }

                    // Decrease NumberOfViolation in StudentInClass
                    var studentInClass = _unitOfWork.StudentInClass.GetById(violation.StudentInClassId.Value);
                    if (studentInClass != null && studentInClass.NumberOfViolation > 0)
                    {
                        _unitOfWork.StudentInClass.DetachLocal(studentInClass, studentInClass.StudentInClassId);
                        studentInClass.NumberOfViolation -= 1;
                        _unitOfWork.StudentInClass.Update(studentInClass);
                    }

                    // Lấy ViolationConfig tương ứng với Viotype và kiểm tra ViolationConfig có hợp lệ không
                    var violationConfig = await _unitOfWork.ViolationConfig.GetConfigByViolationTypeId(violation.ViolationTypeId);
                    if (violationConfig != null && violationConfig.Status == ViolationConfigStatusEnums.ACTIVE.ToString() && violationConfig.MinusPoints.HasValue)
                    {
                        // Khôi phục điểm cho Class dựa trên ViolationConfig
                        var classEntity = await _unitOfWork.Class.GetClassById(violation.ClassId);
                        if (classEntity != null)
                        {
                            _unitOfWork.Class.DetachLocal(classEntity, classEntity.ClassId);
                            classEntity.TotalPoint += violationConfig.MinusPoints.Value;
                            _unitOfWork.Class.Update(classEntity);
                        }
                    }

                    _unitOfWork.Save();

                    response.Data = _mapper.Map<ResponseOfViolation>(violation);
                    response.Success = true;
                    response.Message = "Từ chối vi phạm thành công";
                }
                else
                {
                    response.Message = "Trạng thái vi phạm đang không phải Phản đối, Không thể Từ chối";
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Từ chối vi phạm thất bại.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<ResponseOfViolation>> CompleteViolation(int violationId)
        {
            var response = new DataResponse<ResponseOfViolation>();

            try
            {
                var violation = await _unitOfWork.Violation.GetViolationById(violationId);
                if (violation == null)
                {
                    response.Message = "Không thể tìm thấy vi phạm";
                    response.Success = false;
                    return response;
                }

                if (violation.Status == ViolationStatusEnums.COMPLETED.ToString())
                {
                    response.Message = "Vi phạm đã được Chấp nhận";
                    response.Success = false;
                    return response;
                }

                if (violation.Status != ViolationStatusEnums.DISCUSSING.ToString())
                {
                    response.Message = "Chỉ những vi phạm có trạng thái Phản đối mới có thể được Chấp nhận";
                    response.Success = false;
                    return response;
                }

                // Detach the existing tracked instance of the Violation entity
                _unitOfWork.Violation.DetachLocal(violation, violation.ViolationId);

                // Cập nhật trạng thái của Violation thành COMPLETED
                violation.Status = ViolationStatusEnums.COMPLETED.ToString();
                _unitOfWork.Violation.Update(violation);

                // Cập nhật trạng thái của Discipline tương ứng thành FINALIZED
                var discipline = await _unitOfWork.Discipline.GetDisciplineByViolationId(violation.ViolationId);
                if (discipline != null)
                {
                    _unitOfWork.Discipline.DetachLocal(discipline, discipline.DisciplineId);
                    discipline.Status = DisciplineStatusEnums.FINALIZED.ToString();
                    await _unitOfWork.Discipline.UpdateDiscipline(discipline);
                }

                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfViolation>(violation);
                response.Success = true;
                response.Message = "Chấp nhận thành công, Vi phạm đã được GVCN đồng ý";
            }
            catch (Exception ex)
            {
                response.Message = "Complete vi phạm thất bại.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<ResponseOfViolation>>> GetViolationsBySchoolId(int schoolId, string sortOrder, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var response = new DataResponse<List<ResponseOfViolation>>();
            try
            {
                var violations = await _unitOfWork.Violation.GetViolationsBySchoolId(schoolId, year, semesterName, month, weekNumber);
                if (violations == null || !violations.Any())
                {
                    response.Data = new List<ResponseOfViolation>();
                    response.Message = "Danh sách vi phạm trống";
                    response.Success = true;
                    return response;
                }

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
                response.Message = "Đã tìm thấy vi phạm";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<ResponseOfViolation>();
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByUserId(int userId, string sortOrder, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var response = new DataResponse<List<ResponseOfViolation>>();

            try
            {
                var violations = await _unitOfWork.Violation.GetViolationsByUserId(userId, year, semesterName, month, weekNumber);
                if (violations == null || !violations.Any())
                {
                    response.Data = new List<ResponseOfViolation>();
                    response.Message = "Danh sách vi phạm trống";
                    response.Success = true;
                    return response;
                }

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
                response.Message = "Danh sách các vi phạm của lớp";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<ResponseOfViolation>();
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<ResponseOfViolation>> GetViolationByDisciplineId(int disciplineId)
        {
            var response = new DataResponse<ResponseOfViolation>();

            try
            {
                var violation = await _unitOfWork.Violation.GetViolationByDisciplineId(disciplineId);
                if (violation == null)
                {
                    response.Data = "Empty";
                    response.Message = "Vi phạm không tồn tại";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<ResponseOfViolation>(violation);
                response.Message = $"ViolationId {violation.ViolationId}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByUserRoleStudentSupervisor(int userId, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfViolation>>();

            try
            {
                var violations = await _unitOfWork.Violation.GetViolationsByUserRoleStudentSupervisor(userId);
                if (violations is null || !violations.Any())
                {
                    response.Data = "Empty";
                    response.Message = "Danh sách vi phạm trống";
                    response.Success = true;
                    return response;
                }

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
                response.Message = "Danh sách các vi phạm của sao đỏ";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByUserRoleSupervisor(int userId, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfViolation>>();

            try
            {
                var violations = await _unitOfWork.Violation.GetViolationsByUserRoleSupervisor(userId);
                if (violations is null || !violations.Any())
                {
                    response.Data = "Empty";
                    response.Message = "Danh sách vi phạm trống";
                    response.Success = true;
                    return response;
                }

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
                response.Message = "Danh sách các vi phạm của giám thị";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ResponseOfViolation>>> GetViolationsBySupervisorUserId(int userId, string sortOrder, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var response = new DataResponse<List<ResponseOfViolation>>();
            try
            {
                var violations = await _unitOfWork.Violation.GetViolationsBySupervisorUserId(userId, year, semesterName, month, weekNumber);
                if (violations == null || !violations.Any())
                {
                    response.Data = new List<ResponseOfViolation>();
                    response.Message = "Danh sách vi phạm trống";
                    response.Success = false;
                    return response;
                }

                var violationDTOS = _mapper.Map<List<ResponseOfViolation>>(violations);

                if (sortOrder == "desc")
                {
                    violationDTOS = violationDTOS.OrderByDescending(r => r.ViolationId).ToList();
                }
                else
                {
                    violationDTOS = violationDTOS.OrderBy(r => r.ViolationId).ToList();
                }

                response.Data = violationDTOS;
                response.Message = "Đã tìm thấy vi phạm";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }
    }
}
