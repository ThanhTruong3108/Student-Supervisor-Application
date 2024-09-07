using AutoMapper;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Response.SemesterResponse;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Request.SemesterRequest;

namespace StudentSupervisorService.Service.Implement
{
    public class SemesterImplement : SemesterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SemesterImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DataResponse<List<ResponseOfSemester>>> GetSemestersBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<ResponseOfSemester>>();

            try
            {
                var semesters = await _unitOfWork.Semester.GetSemestersBySchoolId(schoolId);
                if (!semesters.Any())
                {
                    throw new Exception("Không tìm thấy học kỳ nào cho trường này.");
                }

                response.Data = _mapper.Map<List<ResponseOfSemester>>(semesters);
                response.Message = $"Số lượng học kỳ: {semesters.Count}";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<ResponseOfSemester>> UpdateSemester(int id, RequestOfSemester request)
        {
            var response = new DataResponse<ResponseOfSemester>();

            try
            {
                // Lấy học kỳ cần cập nhật
                var semester = await _unitOfWork.Semester.GetSemesterById(id);
                if (semester is null)
                {
                    response.Message = "Không thể tìm thấy Học kỳ";
                    response.Success = false;
                    return response;
                }

                // Lấy năm học liên quan đến học kỳ hiện tại
                var schoolYear = _unitOfWork.SchoolYear.GetById(semester.SchoolYearId);
                if (schoolYear is null)
                {
                    response.Message = "Không thể tìm thấy Năm học liên quan";
                    response.Success = false;
                    return response;
                }

                // Lấy danh sách các học kỳ thuộc năm học hiện tại, không cập nhật học kỳ khác
                var semesters = await _unitOfWork.Semester.GetSemestersBySchoolYearId(schoolYear.SchoolYearId);
                var semester1 = semesters.FirstOrDefault(s => s.Name == "Học kỳ 1" && s.SchoolYearId != id);
                var semester2 = semesters.FirstOrDefault(s => s.Name == "Học kỳ 2" && s.SchoolYearId != id);

                // Validate riêng cho học kỳ hiện tại
                if (request.StartDate >= request.EndDate)
                {
                    response.Message = $"Ngày bắt đầu của {semester.Name} phải trước ngày kết thúc.";
                    response.Success = false;
                    return response;
                }

                // Nếu đang cập nhật Học kỳ 1, kiểm tra ngày kết thúc phải trước ngày bắt đầu của Học kỳ 2 (nếu Học kỳ 2 đã tồn tại)
                if (semester.Name == "Học kỳ 1" && semester2 != null && request.EndDate >= semester2.StartDate)
                {
                    response.Message = "Ngày kết thúc của Học kỳ 1 phải trước ngày bắt đầu của Học kỳ 2.";
                    response.Success = false;
                    return response;
                }

                // Nếu đang cập nhật Học kỳ 2, kiểm tra ngày bắt đầu phải sau ngày kết thúc của Học kỳ 1 (nếu Học kỳ 1 đã tồn tại)
                if (semester.Name == "Học kỳ 2" && semester1 != null && semester1.EndDate >= request.StartDate)
                {
                    response.Message = "Ngày bắt đầu của Học kỳ 2 phải sau ngày kết thúc của Học kỳ 1.";
                    response.Success = false;
                    return response;
                }

                // Cập nhật thông tin học kỳ
                semester.SchoolYearId = request.SchoolYearId;
                semester.Name = request.Name;
                semester.StartDate = request.StartDate;
                semester.EndDate = request.EndDate;

                _unitOfWork.Semester.Update(semester);
                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfSemester>(semester);
                response.Success = true;
                response.Message = "Cập nhật thành công";
            }
            catch (Exception ex)
            {
                response.Message = "Cập nhật thất bại.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }

            return response;
        }
    }
}
