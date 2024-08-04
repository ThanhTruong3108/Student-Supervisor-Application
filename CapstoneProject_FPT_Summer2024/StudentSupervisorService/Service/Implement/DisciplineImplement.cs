using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.DisciplineRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.DisciplineResponse;

namespace StudentSupervisorService.Service.Implement
{
    public class DisciplineImplement : DisciplineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DisciplineImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DataResponse<List<DisciplineResponse>>> GetAllDisciplines(string sortOrder)
        {
            var response = new DataResponse<List<DisciplineResponse>>();
            try
            {

                var disciplineEntities = await _unitOfWork.Discipline.GetAllDisciplines();
                if (disciplineEntities is null || !disciplineEntities.Any())
                {
                    response.Data = "Empty";
                    response.Message = "Danh sách Kỷ luật trống";
                    response.Success = true;
                    return response;
                }

                disciplineEntities = sortOrder == "desc"
                    ? disciplineEntities.OrderByDescending(r => r.DisciplineId).ToList()
                    : disciplineEntities.OrderBy(r => r.DisciplineId).ToList();

                response.Data = _mapper.Map<List<DisciplineResponse>>(disciplineEntities);
                response.Message = "Danh sách các Kỷ luật";
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Message = "Oops!  Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<DisciplineResponse>> GetDisciplineById(int id)
        {
            var response = new DataResponse<DisciplineResponse>();
            try
            {
                var disciplineEntity = await _unitOfWork.Discipline.GetDisciplineById(id);
                if (disciplineEntity == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy Kỷ luật";
                    response.Success = false;
                    return response;
                }

                response.Data = _mapper.Map<DisciplineResponse>(disciplineEntity);
                response.Message = "Tìm thấy Kỷ luật";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Oops!  Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<DisciplineResponse>> CreateDiscipline(DisciplineCreateRequest request)
        {
            var response = new DataResponse<DisciplineResponse>();
            try
            {
                // Check if StartDate is greater than EndDate
                if (request.StartDate > request.EndDate)
                {
                    response.Data = "Empty";
                    response.Message = "Ngày bắt đầu không được lớn hơn Ngày kết thúc";
                    response.Success = false;
                    return response;
                }

                var disciplineEntity = new Discipline
                {
                    ViolationId = request.ViolationId,
                    PennaltyId = request.PennaltyId,
                    Description = request.Description,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Status = DisciplineStatusEnums.PENDING.ToString()
                };

                var created = await _unitOfWork.Discipline.CreateDiscipline(disciplineEntity);

                response.Data = _mapper.Map<DisciplineResponse>(created);
                response.Message = "Kỷ luật được tạo thành công !!";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Tạo kỷ luật không thành công: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }
        public async Task<DataResponse<DisciplineResponse>> UpdateDiscipline(int id, DisciplineUpdateRequest request)
        {
            var response = new DataResponse<DisciplineResponse>();
            try
            {
                var existingDiscipline = await _unitOfWork.Discipline.GetDisciplineById(id);
                if (existingDiscipline == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy Kỷ luật";
                    response.Success = false;
                    return response;
                }

                // Check if StartDate is greater than or equal EndDate
                if (request.StartDate >= request.EndDate)
                {
                    response.Data = "Empty";
                    response.Message = "Ngày bắt đầu không được lớn hơn hoặc bằng Ngày kết thúc";
                    response.Success = false;
                    return response;
                }
                existingDiscipline.PennaltyId = request.PennaltyId ?? existingDiscipline.PennaltyId;
                existingDiscipline.StartDate = request.StartDate ?? existingDiscipline.StartDate;
                existingDiscipline.EndDate = request.EndDate ?? existingDiscipline.EndDate;

                await _unitOfWork.Discipline.UpdateDiscipline(existingDiscipline);

                response.Data = _mapper.Map<DisciplineResponse>(existingDiscipline);
                response.Message = "Kỷ luật được cập nhật thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Cập nhật Kỷ luật không thành công: " + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<DisciplineResponse>> DeleteDiscipline(int id)
        {
            var response = new DataResponse<DisciplineResponse>();
            try
            {
                var existingDiscipline = await _unitOfWork.Discipline.GetDisciplineById(id);
                if (existingDiscipline == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không tìm thấy Kỷ luật";
                    response.Success = false;
                    return response;
                }

                if (existingDiscipline.Status == DisciplineStatusEnums.INACTIVE.ToString())
                {
                    response.Data = null;
                    response.Message = "Kỷ luật đã bị xóa!";
                    response.Success = false;
                    return response;
                }

                await _unitOfWork.Discipline.DeleteDiscipline(id);
                response.Data = "Empty";
                response.Message = "Kỷ luật đã được xóa thành công !!";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Oops!  Đã có lỗi xảy ra.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<List<DisciplineResponse>>> GetDisciplinesBySchoolId(int schoolId)
        {
            var response = new DataResponse<List<DisciplineResponse>>();
            try
            {
                var disciplines = await _unitOfWork.Discipline.GetDisciplinesBySchoolId(schoolId);
                if (disciplines == null || !disciplines.Any())
                {
                    response.Message = "Không tìm thấy Kỷ luật nào cho SchoolId được chỉ định";
                    response.Success = false;
                }
                else
                {
                    var disciplineDTOs = _mapper.Map<List<DisciplineResponse>>(disciplines);
                    response.Data = disciplineDTOs;
                    response.Message = "Kỷ luật được tìm thấy";
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Message = "Oops!  Đã có lỗi xảy ra.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<DisciplineResponse>> ExecutingDiscipline(int disciplineId)
        {
            var response = new DataResponse<DisciplineResponse>();

            try
            {
                var discipline = await _unitOfWork.Discipline.GetDisciplineById(disciplineId); 
                if (discipline == null)
                {
                    response.Message = "Không thể tìm thấy Kỷ luật!!";
                    response.Success = false;
                    return response;
                }

                // Check if the status is already EXECUTING
                if (discipline.Status == DisciplineStatusEnums.EXECUTING.ToString())
                {
                    response.Data = _mapper.Map<DisciplineResponse>(discipline);
                    response.Message = "Kỷ luật đã ở trạng thái EXECUTING";
                    response.Success = false;
                    return response;
                }

                // Check if the status is PENDING
                if (discipline.Status != DisciplineStatusEnums.PENDING.ToString())
                {
                    response.Data = _mapper.Map<DisciplineResponse>(discipline);
                    response.Message = "Trạng thái kỷ luật không phải là PENDING, không thể chuyển thành EXECUTING !!";
                    response.Success = false;
                    return response;
                }

                discipline.Status = DisciplineStatusEnums.EXECUTING.ToString();
                _unitOfWork.Discipline.Update(discipline);
                _unitOfWork.Save(); 

                response.Data = _mapper.Map<DisciplineResponse>(discipline);
                response.Success = true;
                response.Message = "Executing kỷ luật thành công";
            }
            catch (Exception ex)
            {
                response.Message = "Executing kỷ luật thất bại.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<DisciplineResponse>> DoneDiscipline(int disciplineId)
        {
            var response = new DataResponse<DisciplineResponse>();

            try
            {
                var discipline = await _unitOfWork.Discipline.GetDisciplineById(disciplineId);
                if (discipline == null)
                {
                    response.Message = "Không thể tìm thấy Kỷ luật!!";
                    response.Success = false;
                    return response;
                }

                // Check if the status is already DONE
                if (discipline.Status == DisciplineStatusEnums.DONE.ToString())
                {
                    response.Message = "Kỷ luật đã ở trạng thái DONE";
                    response.Success = false;
                    return response;
                }

                // Check if the status is FINALIZED or EXECUTING
                if (discipline.Status != DisciplineStatusEnums.EXECUTING.ToString() && discipline.Status != DisciplineStatusEnums.FINALIZED.ToString())
                {
                    response.Message = "Trạng thái kỷ luật không phải là EXECUTING hoặc FINALIZED, không thể chuyển thành DONE !!";
                    response.Success = false;
                    return response;
                }

                discipline.Status = DisciplineStatusEnums.DONE.ToString();
                _unitOfWork.Discipline.Update(discipline);
                _unitOfWork.Save();

                response.Data = _mapper.Map<DisciplineResponse>(discipline);
                response.Success = true;
                response.Message = "kỷ luật đã được thực hiện xong.";
            }
            catch (Exception ex)
            {
                response.Message = "Done kỷ luật không thành công.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }

        public async Task<DataResponse<DisciplineResponse>> ComplainDiscipline(int disciplineId)
        {
            var response = new DataResponse<DisciplineResponse>();

            try
            {
                var discipline = await _unitOfWork.Discipline.GetDisciplineById(disciplineId);
                if (discipline == null)
                {
                    response.Message = "Không thể tìm thấy Kỷ luật!!";
                    response.Success = false;
                    return response;
                }

                // Kiểm tra trạng thái hiện tại
                if (discipline.Status == DisciplineStatusEnums.COMPLAIN.ToString())
                {
                    response.Message = "Kỷ luật đã ở trạng thái COMPLAIN";
                    response.Success = false;
                    return response;
                }

                // Check if the status is EXECUTING
                if (discipline.Status != DisciplineStatusEnums.PENDING.ToString())
                {
                    response.Message = "Trạng thái kỷ luật không phải là PENDING, không thể chuyển thành COMPLAIN !!";
                    response.Success = false;
                    return response;
                }

                // Cập nhật trạng thái thành COMPLAIN
                _unitOfWork.Discipline.DetachLocal(discipline, discipline.DisciplineId);
                discipline.Status = DisciplineStatusEnums.COMPLAIN.ToString();
                _unitOfWork.Discipline.Update(discipline);

                // Cập nhật trạng thái của Violation tương ứng thành DISCUSSING
                var violation = await _unitOfWork.Violation.GetViolationByDisciplineId(discipline.DisciplineId);
                if (violation != null)
                {
                    _unitOfWork.Violation.DetachLocal(violation, violation.ViolationId);
                    violation.Status = ViolationStatusEnums.DISCUSSING.ToString();
                    await _unitOfWork.Violation.UpdateViolation(violation);
                }

                _unitOfWork.Save();

                response.Data = _mapper.Map<DisciplineResponse>(discipline);
                response.Message = "Phàn nàn kỷ luật thành công";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Phàn nàn kỷ luật thất bại.\n" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }


        public async Task<DataResponse<List<DisciplineResponse>>> GetDisciplinesByUserId(int userId, string sortOrder)
        {
            var response = new DataResponse<List<DisciplineResponse>>();

            try
            {
                var disciplines = await _unitOfWork.Discipline.GetDisciplinesByUserId(userId);
                if (disciplines is null || !disciplines.Any())
                {
                    response.Data = "Empty";
                    response.Message = "Danh sách Kỷ luật trống";
                    response.Success = true;
                    return response;
                }

                var disciplineDTO = _mapper.Map<List<DisciplineResponse>>(disciplines);
                if (sortOrder == "desc")
                {
                    disciplineDTO = disciplineDTO.OrderByDescending(r => r.DisciplineId).ToList();
                }
                else
                {
                    disciplineDTO = disciplineDTO.OrderBy(r => r.DisciplineId).ToList();
                }

                response.Data = disciplineDTO;
                response.Message = "Danh sách các Kỷ luật của lớp";
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
