using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.ViolationReportResponse;
using StudentSupervisorService.Models.Request.ViolationReportRequest;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/violation-reports")]
    [ApiController]
    public class ViolationReportController : ControllerBase
    {
        private ViolationReportService _service;
        public ViolationReportController(ViolationReportService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<DataResponse<List<ResponseOfVioReport>>>> GetVioReports(string sortOrder)
        {
            try
            {
                var vioReports = await _service.GetAllVioReports(sortOrder);
                return Ok(vioReports);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfVioReport>>> GetVioReportById(int id)
        {
            try
            {
                var vioReport = await _service.GetVioReportById(id);
                return Ok(vioReport);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchVioReports(int? studentInClassId, int? violationId, string sortOrder = "asc")
        {
            try
            {
                var vioReports = await _service.SearchVioReports(studentInClassId, violationId,  sortOrder);
                return Ok(vioReports);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<ResponseOfVioReport>>> CreateVioReport(RequestOfVioReport request)
        {
            var createdVioReport = await _service.CreateVioReport(request);
            return createdVioReport == null ? NotFound() : Ok(createdVioReport);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfVioReport>>> UpdateViolation(int id, RequestOfVioReport request)
        {
            var updatedVioReport = await _service.UpdateVioReport(id, request);
            return updatedVioReport == null ? NotFound() : Ok(updatedVioReport);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteViolation(int id)
        {
            var deletedVioReport = _service.DeleteVioReport(id);
            return deletedVioReport == null ? NoContent() : Ok(deletedVioReport);
        }
    }
}
