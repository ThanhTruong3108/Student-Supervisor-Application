using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.ViolationConfigResponse;
using StudentSupervisorService.Models.Request.ViolationConfigRequest;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/violation-configs")]
    [ApiController]
    public class ViolationConfigController : ControllerBase
    {
        private ViolationConfigService _service;
        public ViolationConfigController(ViolationConfigService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<DataResponse<List<ViolationConfigResponse>>>> GetViolationConfigs(string sortOrder)
        {
            try
            {
                var violationConfigs = await _service.GetAllViolationConfigs(sortOrder);
                return Ok(violationConfigs);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<ViolationConfigResponse>>> GetviolationConfigById(int id)
        {
            try
            {
                var violationConfig = await _service.GetViolationConfigById(id);
                return Ok(violationConfig);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchViolationConfigs(int? evaluationId = null, int? vioTypeId = null, string? code = null, string? name = null, string sortOrder = "asc")
        {
            try
            {
                var violationConfigs = await _service.SearchViolationConfigs(evaluationId, vioTypeId, code, name, sortOrder);
                return Ok(violationConfigs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<ViolationConfigResponse>>> CreateViolationConfig(RequestOfViolationConfig request)
        {
            var createdViolationConfig = await _service.CreateViolationConfig(request);
            return createdViolationConfig == null ? NotFound() : Ok(createdViolationConfig);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DataResponse<ViolationConfigResponse>>> UpdateViolationConfig(int id, RequestOfViolationConfig request)
        {
            var updatedViolationConfig = await _service.UpdateViolationConfig(id, request);
            return updatedViolationConfig == null ? NotFound() : Ok(updatedViolationConfig);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteViolation(int id)
        {
            var deletedViolationConfig = _service.DeleteViolationConfig(id);
            return deletedViolationConfig == null ? NoContent() : Ok(deletedViolationConfig);
        }
    }
}
