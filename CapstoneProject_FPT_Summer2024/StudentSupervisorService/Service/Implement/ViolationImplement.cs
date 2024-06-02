using AutoMapper;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.ViolationRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ViolationResponse;


namespace StudentSupervisorService.Service.Implement
{
    public class ViolationImplement : ViolationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ViolationImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DataResponse<ResponseOfViolation>> CreateViolation(RequestOfViolation request)
        {
            var response = new DataResponse<ResponseOfViolation>();

            try
            {
                var createviolation = _mapper.Map<Violation>(request);
                createviolation.CreatedAt = DateTime.Now;
                createviolation.Status = ViolationEnum.ACTIVE.ToString();
                _unitOfWork.Violation.Add(createviolation);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfViolation>(createviolation);
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

        public async Task DeleteViolation(int id)
        {
            var violation = _unitOfWork.Violation.GetById(id);
            if (violation is null)
            {
                throw new Exception("Can not found by" + id);
            }
            violation.Status = SchoolYearEnum.INACTIVE.ToString();

            _unitOfWork.Violation.Update(violation);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<List<ResponseOfViolation>>> GetAllViolations(int page, int pageSize, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfViolation>>();

            try
            {
                var violations = await _unitOfWork.Violation.GetAllViolations();
                if (violations is null)
                {
                    response.Message = "The Violation list is empty";
                    response.Success = true;
                }

                // Sắp xếp danh sách Violation theo yêu cầu
                var violationDTO = _mapper.Map<List<ResponseOfViolation>>(violations);
                if (sortOrder == "desc")
                {
                    violationDTO = violationDTO.OrderByDescending(r => r.Code).ToList();
                }
                else
                {
                    violationDTO = violationDTO.OrderBy(r => r.Code).ToList();
                }

                // Thực hiện phân trang
                var startIndex = (page - 1) * pageSize;
                var pageViolations = violationDTO.Skip(startIndex).Take(pageSize).ToList();

                response.Data = pageViolations;
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

        public async Task<DataResponse<ResponseOfViolation>> UpdateViolation(int id, RequestOfViolation request)
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
                violation.CreatedAt = request.CreatedAt;
                violation.CreatedBy = request.CreatedBy;
                violation.UpdatedAt = DateTime.Now;
                violation.UpdatedBy = request.UpdatedBy;

                _unitOfWork.Violation.Update(violation);
                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfViolation>(violation);
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
