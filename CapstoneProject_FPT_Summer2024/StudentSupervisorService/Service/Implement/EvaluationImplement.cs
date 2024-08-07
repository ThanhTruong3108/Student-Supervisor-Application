using AutoMapper;
using Domain.Entity;
using Domain.Entity.DTO;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.EvaluationRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.EvaluationResponse;


namespace StudentSupervisorService.Service.Implement
{
    public class EvaluationImplement : EvaluationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EvaluationImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DataResponse<List<EvaluationResponse>>> GetAllEvaluations(string sortOrder)
        {
            var response = new DataResponse<List<EvaluationResponse>>();
            try
            {

                var evaluationEntities = await _unitOfWork.Evaluation.GetAllEvaluations();
                if (evaluationEntities is null || !evaluationEntities.Any())
                {
                    response.Message = "Danh sách bảng Đánh giá trống";
                    response.Success = true;
                    return response;
                }

                evaluationEntities = sortOrder == "desc"
                    ? evaluationEntities.OrderByDescending(r => r.EvaluationId).ToList()
                    : evaluationEntities.OrderBy(r => r.EvaluationId).ToList();

                response.Data = _mapper.Map<List<EvaluationResponse>>(evaluationEntities);
                response.Message = "Danh sách các bảng Đánh giá";
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

        public async Task<DataResponse<EvaluationResponse>> GetEvaluationById(int id)
        {
            var response = new DataResponse<EvaluationResponse>();
            try
            {
                var evaluationEntity = await _unitOfWork.Evaluation.GetEvaluationById(id);
                if (evaluationEntity == null)
                {
                    response.Message = "Danh sách bảng Đánh giá trống";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<EvaluationResponse>(evaluationEntity);
                response.Message = "Bảng Đánh giá được tìm thấy";
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

        public async Task<DataResponse<EvaluationResponse>> CreateEvaluation(EvaluationCreateRequest request)
        {
            var response = new DataResponse<EvaluationResponse>();
            try
            {
                // Lấy Lớp học liên quan đến đánh giá
                var classEntity =  _unitOfWork.Class.GetById(request.ClassId ?? 0);
                if (classEntity == null)
                {
                    response.Message = "Không tìm thấy Lớp";
                    response.Success = false;
                    return response;
                }

                // Lấy Niên khóa liên quan đến lớp học
                var schoolYear = _unitOfWork.SchoolYear.GetById(classEntity.SchoolYearId);
                if (schoolYear == null)
                {
                    response.Message = "Không tìm thấy niên khóa tương ứng với lớp";
                    response.Success = false;
                    return response;
                }

                // Validate phạm vi ngày đánh giá
                if (request.From < schoolYear.StartDate || request.To > schoolYear.EndDate)
                {
                    response.Message = "Ngày đánh giá nằm ngoài thời gian của Niên khóa";
                    response.Success = false;
                    return response;
                }

                // Kiểm tra xem đánh giá đã tồn tại trong phạm vi ngày đã chỉ định chưa
                var existingEvaluation = await _unitOfWork.Evaluation.GetEvaluationsByClassIdAndDateRange(
                    request.ClassId ?? 0, request.From, request.To);
                if (existingEvaluation.Any())
                {
                    response.Message = "Đã có đánh giá trong khoảng thời gian được chỉ định.";
                    response.Success = false;
                    return response;
                }

                // Create một Đánh giá mới
                var evaluationEntity = new Evaluation
                {
                    ClassId = request.ClassId,
                    Description = request.Description,
                    From = request.From,
                    To = request.To,
                    Points = classEntity.TotalPoint,
                    Status = EvaluationStatusEnums.ACTIVE.ToString(),
                };

                var createdEvaluation = await _unitOfWork.Evaluation.CreateEvaluation(evaluationEntity);

                // Update TotalPoint của lớp
                classEntity.TotalPoint = 100;
                _unitOfWork.Class.Update(classEntity);
                _unitOfWork.Save();

                response.Data = _mapper.Map<EvaluationResponse>(createdEvaluation);
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


        public async Task<DataResponse<EvaluationResponse>> UpdateEvaluation(EvaluationUpdateRequest request)
        {
            var response = new DataResponse<EvaluationResponse>();
            try
            {
                var existingEvaluation = await _unitOfWork.Evaluation.GetEvaluationById(request.EvaluationId);
                if (existingEvaluation == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy bảng Đánh giá";
                    response.Success = false;
                    return response;
                }

                existingEvaluation.ClassId = request.ClassId ?? existingEvaluation.ClassId;
                existingEvaluation.Description = request.Description ?? existingEvaluation.Description;
                existingEvaluation.From = request.From ?? existingEvaluation.From;
                existingEvaluation.To = request.To ?? existingEvaluation.To;
                existingEvaluation.Points = request.Points ?? existingEvaluation.Points;

                await _unitOfWork.Evaluation.UpdateEvaluation(existingEvaluation);

                response.Data = _mapper.Map<EvaluationResponse>(existingEvaluation);
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

        public async Task<DataResponse<EvaluationResponse>> DeleteEvaluation(int id)
        {
            var response = new DataResponse<EvaluationResponse>();
            try
            {
                var evaluation = _unitOfWork.Evaluation.GetById(id);
                if (evaluation is null)
                {
                    response.Data = "Empty";
                    response.Message = "Không thể tìm thấy bảng Đánh giá !!";
                    response.Success = false;
                    return response;
                }

                _unitOfWork.Evaluation.Remove(evaluation);
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

        public async Task<DataResponse<List<EvaluationResponse>>> GetEvaluationsBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<EvaluationResponse>>();
            try
            {
                var evaluations = await _unitOfWork.Evaluation.GetEvaluationsBySchoolId(schoolId);
                if (evaluations == null || !evaluations.Any())
                {
                    response.Message = "Không tìm thấy bảng Đánh giá nào cho SchoolId được chỉ định!!";
                    response.Success = false;
                }
                else
                {
                    var evaluationDTOS = _mapper.Map<List<EvaluationResponse>>(evaluations);
                    response.Data = evaluationDTOS;
                    response.Message = "Bảng Đánh giá được tìm thấy";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Oops! Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<ClassRankResponse>>> GetEvaluationRanks(int schoolId, DateTime fromDate, DateTime toDate)
        {
            var response = new DataResponse<List<ClassRankResponse>>();
            try
            {
                var evaluations = await _unitOfWork.Evaluation.GetEvaluationRanks(schoolId, fromDate, toDate);
                if (evaluations == null || !evaluations.Any())
                {
                    response.Message = "Không có dữ liệu xếp hạng cho khoảng thời gian được chỉ định!";
                    response.Success = false;
                    return response;
                }

                // Calculate ranks
                int rank = 1;
                foreach (var eval in evaluations)
                {
                    eval.Rank = rank++;
                }

                response.Data = evaluations;
                response.Message = "Danh sách xếp hạng theo đánh giá";
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
