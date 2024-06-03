using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.ViolationResponse;
using StudentSupervisorService.Models.Request.ViolationRequest;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/violations")]
    [ApiController]
    public class ViolationController : ControllerBase
    {
        private ViolationService _service;
        public ViolationController(ViolationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<ResponseOfViolation>>>> GetViolations(int page = 1, int pageSize = 5, string sortOrder = "asc")
        {
            try
            {
                var violations = await _service.GetAllViolations(page, pageSize, sortOrder);
                return Ok(violations);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> GetviolationById(int id)
        {
            try
            {
                var violation = await _service.GetViolationById(id);
                return Ok(violation);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchViolations(int? classId = null, int? teacherId = null, int? vioTypeId = null, string? code = null, string? name = null, DateTime? date = null, string sortOrder = "asc")
        {
            try
            {
                var violations = await _service.SearchViolations(classId, teacherId, vioTypeId, code, name, date, sortOrder);
                return Ok(violations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> CreateViolation([FromForm] RequestOfCreateViolation request)
        {
            try
            {
                var createdViolation = await _service.CreateViolation(request);
                return Ok(createdViolation);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> UpdateViolation(int id, RequestOfUpdateViolation request)
        {
            var updatedViolation = await _service.UpdateViolation(id, request);
            return updatedViolation == null ? NotFound() : Ok(updatedViolation);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteViolation(int id)
        {
            var deletedViolation = _service.DeleteViolation(id);
            return deletedViolation == null ? NoContent() : Ok(deletedViolation);
        }
    }
}
