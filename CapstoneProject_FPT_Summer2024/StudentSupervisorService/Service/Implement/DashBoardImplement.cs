﻿using AutoMapper;
using Domain.Entity.DTO;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Response.ViolationResponse;
using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service.Implement
{
    public class DashBoardImplement : DashBoardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DashBoardImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<List<ViolationTypeSummary>>> GetTopFrequentViolations(int schoolId, short year, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var response = new DataResponse<List<ViolationTypeSummary>>();

            try
            {
                var violations = await _unitOfWork.Violation.GetTopFrequentViolations(schoolId, year, semesterName, month, weekNumber);
                if (violations == null || !violations.Any())
                {
                    response.Data = new List<ViolationTypeSummary>();
                    response.Message = "Không có vi phạm thường xuyên trong thời gian này";
                    response.Success = true;
                    return response;
                }

                response.Data = violations;
                response.Message = "Danh sách top vi phạm thường xuyên trong năm học";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<ViolationTypeSummary>();
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ClassViolationSummary>>> GetClassesWithMostViolations(int schoolId, short year, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var response = new DataResponse<List<ClassViolationSummary>>();

            try
            {
                if (year == 0)
                {
                    response.Data = new List<ClassViolationSummary>();
                    response.Message = "Năm là bắt buộc.";
                    response.Success = false;
                    return response;
                }

                var classViolations = await _unitOfWork.Violation.GetClassesWithMostViolations(schoolId, year, semesterName, month, weekNumber);
                if (classViolations == null || !classViolations.Any())
                {
                    response.Data = new List<ClassViolationSummary>();
                    response.Message = "Không có lớp nào với nhiều vi phạm trong khoảng thời gian này";
                    response.Success = true;
                    return response;
                }

                response.Data = classViolations;
                response.Message = "Danh sách lớp có nhiều vi phạm trong khoảng thời gian này";
                response.Success = true;
            }
            catch (ArgumentException ex)
            {
                response.Data = new List<ClassViolationSummary>();
                response.Message = ex.Message;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.Data = new List<ClassViolationSummary>();
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<StudentViolationCount>>> GetTop5StudentsWithMostViolations(int schoolId, short year, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var response = new DataResponse<List<StudentViolationCount>>();

            try
            {
                var violations = await _unitOfWork.Violation.GetTop5StudentsWithMostViolations(schoolId, year, semesterName, month, weekNumber);
                if (violations == null || !violations.Any())
                {
                    response.Data = new List<StudentViolationCount>();
                    response.Message = "Không có học sinh vi phạm trong khoảng thời gian này.";
                    response.Success = true;
                    return response;
                }

                response.Data = violations;
                response.Message = "Danh sách top 5 học sinh vi phạm nhiều nhất.";
                response.Success = true;
            }
            catch (ArgumentException ex)
            {
                response.Data = new List<StudentViolationCount>();
                response.Message = ex.Message;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.Data = new List<StudentViolationCount>();
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ClassViolationDetail>>> GetClassWithMostStudentViolations(int schoolId, short year, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var response = new DataResponse<List<ClassViolationDetail>>();

            try
            {
                var violations = await _unitOfWork.Violation.GetClassWithMostStudentViolations(schoolId, year, semesterName, month, weekNumber);
                if (violations == null || !violations.Any())
                {
                    response.Data = new List<ClassViolationDetail>();
                    response.Message = "Không có lớp nào có học sinh vi phạm trong khoảng thời gian này";
                    response.Success = true;
                    return response;
                }

                response.Data = violations;
                response.Message = "Lớp có số lượng học sinh vi phạm nhiều nhất trong khoảng thời gian này";
                response.Success = true;
            }
            catch (ArgumentException ex)
            {
                response.Data = new List<ClassViolationDetail>();
                response.Message = ex.Message;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.Data = new List<ClassViolationDetail>();
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByStudentCode(string studentCode)
        {
            var response = new DataResponse<List<ResponseOfViolation>>();
            try
            {
                var student = await _unitOfWork.Student.GetStudentByCode(studentCode);
                if (student == null)
                {
                    response.Message = "Học sinh không tìm thấy!!";
                    response.Success = false;
                    return response;
                }

                var violations = await _unitOfWork.Violation.GetViolationsByStudentId(student.StudentId);
                response.Data = _mapper.Map<List<ResponseOfViolation>>(violations);
                response.Message = "Danh sách vi phạm";
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

        public async Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByStudentCodeAndYear(string studentCode, short year)
        {
            var response = new DataResponse<List<ResponseOfViolation>>();
            try
            {
                var studentEntity = await _unitOfWork.Student.GetStudentByCode(studentCode);
                if (studentEntity == null)
                {
                    response.Message = "Học sinh không tìm thấy!!";
                    response.Success = false;
                    return response;
                }

                // Find the SchoolYearId based on the studentCode and Year
                var schoolYear = await _unitOfWork.SchoolYear.GetYearBySchoolYearId(studentEntity.SchoolId, year);
                if (schoolYear == null)
                {
                    response.Message = $"Năm học {year} của học sinh không tìm thấy!!";
                    response.Success = false;
                    return response;
                }

                var violations = await _unitOfWork.Violation.GetViolationsByStudentIdAndYear(studentEntity.StudentId, schoolYear.SchoolYearId);
                response.Data = _mapper.Map<List<ResponseOfViolation>>(violations);
                response.Message = "Danh sách vi phạm";
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

        public async Task<DataResponse<Dictionary<int, int>>> GetViolationCountByYear(string studentCode)
        {
            var response = new DataResponse<Dictionary<int, int>>();
            try
            {
                var studentEntity = await _unitOfWork.Student.GetStudentByCode(studentCode);
                if (studentEntity == null)
                {
                    response.Message = "Học sinh không tìm thấy!!";
                    response.Success = false;
                    return response;
                }

                var violations = await _unitOfWork.Violation.GetViolationCountByYear(studentEntity.StudentId);
                response.Data = violations;
                response.Message = "Số lượng vi phạm theo năm";
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

        public async Task<DataResponse<List<KeyValuePair<string, int>>>> CountViolations(int schoolId, short year, int? month = null, int? weekNumber = null)
        {
            var response = new DataResponse<List<KeyValuePair<string, int>>>();

            try
            {
                var violationCounts = await _unitOfWork.Violation.CountViolationsByDate(schoolId, year, month, weekNumber);
                response.Data = violationCounts;

                if (month.HasValue)
                {
                    if (weekNumber.HasValue)
                    {
                        response.Message = "Số lượng vi phạm trong tuần";
                    }
                    else
                    {
                        response.Message = "Số lượng vi phạm trong tháng";
                    }
                }
                else
                {
                    response.Message = "Số lượng vi phạm trong năm";
                }

                response.Success = true;
            }
            catch (ArgumentException ex)
            {
                response.Data = new List<KeyValuePair<string, int>>();
                response.Message = ex.Message;
                response.Success = false;
            }
            catch (Exception ex)
            {
                response.Data = new List<KeyValuePair<string, int>>();
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }

        public async Task<DataResponse<List<KeyValuePair<string, int>>>> GetMonthlyViolations(int schoolId, short year)
        {
            var response = new DataResponse<List<KeyValuePair<string, int>>>();

            try
            {
                var violationCounts = await _unitOfWork.Violation.GetMonthlyViolationCounts(schoolId, year);
                response.Data = violationCounts;
                response.Message = "Số lượng vi phạm theo từng tháng";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<KeyValuePair<string, int>>();
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }
    }
}
