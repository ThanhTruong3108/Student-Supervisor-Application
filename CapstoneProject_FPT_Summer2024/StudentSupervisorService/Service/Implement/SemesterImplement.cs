using AutoMapper;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Response.SemesterResponse;
using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
