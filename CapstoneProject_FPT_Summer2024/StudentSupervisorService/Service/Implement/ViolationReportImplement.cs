using AutoMapper;
using Domain.Entity;
using Infrastructures.Interfaces.IUnitOfWork;
using StudentSupervisorService.Models.Request.ViolationReportRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.HighschoolResponse;
using StudentSupervisorService.Models.Response.ViolationReportResponse;


namespace StudentSupervisorService.Service.Implement
{
    public class ViolationReportImplement : ViolationReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ViolationReportImplement(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DataResponse<ResponseOfVioReport>> CreateVioReport(RequestOfVioReport request)
        {
            var response = new DataResponse<ResponseOfVioReport>();

            try
            {
                var createVioReport = _mapper.Map<ViolationReport>(request);
                _unitOfWork.ViolationReport.Add(createVioReport);
                _unitOfWork.Save();
                response.Data = _mapper.Map<ResponseOfVioReport>(createVioReport);
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

        public async Task DeleteVioReport(int id)
        {
            var vioReport = _unitOfWork.ViolationReport.GetById(id);
            if (vioReport is null)
            {
                throw new Exception("Can not found by" + id);
            }

            _unitOfWork.ViolationReport.Remove(vioReport);
            _unitOfWork.Save();
        }

        public async Task<DataResponse<List<ResponseOfVioReport>>> GetAllVioReports(string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfVioReport>>();

            try
            {
                var vioReports = await _unitOfWork.ViolationReport.GetAllVioReports();
                if (vioReports is null || !vioReports.Any())
                {
                    response.Message = "The ViolationReport list is empty";
                    response.Success = true;
                    return response;
                }
                // Sắp xếp danh sách sản phẩm theo yêu cầu
                var vioReportDTO = _mapper.Map<List<ResponseOfVioReport>>(vioReports);
                if (sortOrder == "desc")
                {
                    vioReportDTO = vioReportDTO.OrderByDescending(r => r.StudentInClassId).ToList();
                }
                else
                {
                    vioReportDTO = vioReportDTO.OrderBy(r => r.StudentInClassId).ToList();
                }
                response.Data = vioReportDTO;
                response.Message = "List ViolationReports";
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

        public async Task<DataResponse<ResponseOfVioReport>> GetVioReportById(int id)
        {
            var response = new DataResponse<ResponseOfVioReport>();

            try
            {
                var vioReport = await _unitOfWork.ViolationReport.GetVioReportById(id);
                if (vioReport is null)
                {
                    throw new Exception("The ViolationReport does not exist");
                }
                response.Data = _mapper.Map<ResponseOfVioReport>(vioReport);
                response.Message = $"ViolationReportId {vioReport.ViolationReportId}";
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

        public async Task<DataResponse<List<ResponseOfVioReport>>> SearchVioReports(int? studentInClassId, int? violationId, string sortOrder)
        {
            var response = new DataResponse<List<ResponseOfVioReport>>();

            try
            {
                var violations = await _unitOfWork.ViolationReport.SearchVioReports(studentInClassId, violationId);
                if (violations is null || violations.Count == 0)
                {
                    response.Message = "No ViolationReport found matching the criteria";
                    response.Success = true;
                }
                else
                {
                    var vioReportDTO = _mapper.Map<List<ResponseOfVioReport>>(violations);

                    // Thực hiện sắp xếp
                    if (sortOrder == "desc")
                    {
                        vioReportDTO = vioReportDTO.OrderByDescending(p => p.ViolationReportId).ToList();
                    }
                    else
                    {
                        vioReportDTO = vioReportDTO.OrderBy(p => p.ViolationReportId).ToList();
                    }

                    response.Data = vioReportDTO;
                    response.Message = "ViolationReports found";
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

        public async Task<DataResponse<ResponseOfVioReport>> UpdateVioReport(int id, RequestOfVioReport request)
        {
            var response = new DataResponse<ResponseOfVioReport>();

            try
            {
                var vioReport = _unitOfWork.ViolationReport.GetById(id);
                if (vioReport is null)
                {
                    response.Message = "Can not found ViolationReport";
                    response.Success = false;
                    return response;
                }

                vioReport.StudentInClassId = request.StudentInClassId;
                vioReport.ViolationId = request.ViolationId;

                _unitOfWork.ViolationReport.Update(vioReport);
                _unitOfWork.Save();

                response.Data = _mapper.Map<ResponseOfVioReport>(vioReport);
                response.Success = true;
                response.Message = "Update Successfully.";
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
