using AutoMapper;
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
        public async Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByMonthAndWeek(int schoolId, short year, int month, int? weekNumber = null)
        {
            var response = new DataResponse<List<ResponseOfViolation>>();

            try
            {
                var violations = await _unitOfWork.Violation.GetViolationsByMonthAndWeek(schoolId, year, month, weekNumber);
                if (violations == null || !violations.Any())
                {
                    response.Data = "Empty";
                    response.Message = weekNumber.HasValue ? "Không có vi phạm nào trong tuần này" : "Không có vi phạm nào trong tháng này";
                    response.Success = true;
                    return response;
                }

                var vioDTO = _mapper.Map<List<ResponseOfViolation>>(violations);
                response.Data = vioDTO;
                response.Message = weekNumber.HasValue ? "Danh sách vi phạm trong tuần" : "Danh sách vi phạm trong tháng";
                response.Success = true;
            }
            catch (ArgumentException ex)
            {
                response.Data = "Empty";
                response.Message = ex.Message;
                response.Success = false;
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

        public async Task<DataResponse<List<ResponseOfViolation>>> GetViolationsByYearAndClassName(short year, string className, int schoolId)
        {
            var response = new DataResponse<List<ResponseOfViolation>>();

            try
            {
                var violations = await _unitOfWork.Violation.GetViolationsByYearAndClassName(year, className, schoolId);
                if (violations == null || !violations.Any())
                {
                    response.Data = "Empty";
                    response.Message = "Không có vi phạm nào trong lớp này";
                    response.Success = true;
                    return response;
                }

                var vioDTO = _mapper.Map<List<ResponseOfViolation>>(violations);
                response.Data = vioDTO;
                response.Message = "Danh sách vi phạm trong lớp";
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
        public async Task<DataResponse<List<ViolationTypeSummary>>> GetTopFrequentViolations(short year, int schoolId)
        {
            var response = new DataResponse<List<ViolationTypeSummary>>();

            try
            {
                var violations = await _unitOfWork.Violation.GetTopFrequentViolations(year, schoolId);
                if (violations == null || !violations.Any())
                {
                    response.Data = new List<ViolationTypeSummary>();
                    response.Message = "Không có vi phạm thường xuyên trong năm học này";
                    response.Success = true;
                    return response;
                }

                response.Data = violations;
                response.Message = "Danh sách top 3 vi phạm thường xuyên trong năm học";
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

        public async Task<DataResponse<List<ClassViolationSummary>>> GetClassesWithMostViolations(short year, int schoolId)
        {
            var response = new DataResponse<List<ClassViolationSummary>>();

            try
            {
                var classViolations = await _unitOfWork.Violation.GetClassesWithMostViolations(year, schoolId);
                if (classViolations == null || !classViolations.Any())
                {
                    response.Data = "Empty";
                    response.Message = "Không có lớp nào với nhiều vi phạm trong năm học này";
                    response.Success = true;
                    return response;
                }

                response.Data = classViolations;
                response.Message = "Danh sách lớp có nhiều vi phạm trong năm học";
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

        public async Task<DataResponse<List<StudentViolationCount>>> GetTop5StudentsWithMostViolations(short year, int schoolId)
        {
            var response = new DataResponse<List<StudentViolationCount>>();

            try
            {
                var violations = await _unitOfWork.Violation.GetTop5StudentsWithMostViolations(year, schoolId);
                if (violations == null || !violations.Any())
                {
                    response.Data = "Empty";
                    response.Message = "Không có học sinh vi phạm trong năm học này";
                    response.Success = true;
                    return response;
                }

                response.Data = violations;
                response.Message = "Danh sách top 5 học sinh vi phạm nhiều nhất";
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

        public async Task<DataResponse<List<ClassViolationDetail>>> GetClassWithMostStudentViolations(short year, int schoolId)
        {
            var response = new DataResponse<List<ClassViolationDetail>>();

            try
            {
                var violations = await _unitOfWork.Violation.GetClassWithMostStudentViolations(year, schoolId);
                if (violations == null || !violations.Any())
                {
                    response.Data = "Empty";
                    response.Message = "Không có lớp nào có học sinh vi phạm trong năm học này";
                    response.Success = true;
                    return response;
                }

                response.Data = violations;
                response.Message = "Lớp có số lượng học sinh vi phạm nhiều nhất";
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
    }
}
