using Microsoft.AspNetCore.Mvc;

using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.ViolationTypeResponse;
using StudentSupervisorService.Models.Request.ViolationTypeRequest;
using Microsoft.AspNetCore.Authorization;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/violation-types")]
    [ApiController]
    [Authorize]
    public class ViolationTypeController : ControllerBase
    {
        private ViolationTypeService _service;
        public ViolationTypeController(ViolationTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<ResponseOfVioType>>>> GetVioTypes(string sortOrder = "asc")
        {
            try
            {
                var vioTypes = await _service.GetAllVioTypes(sortOrder);
                return Ok(vioTypes);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfVioType>>> GetVioTypeById(int id)
        {
            try
            {
                var vioType = await _service.GetVioTypeById(id);
                return Ok(vioType);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<ResponseOfVioType>>> CreateVioType(RequestOfVioType request)
        {
            try
            {
                var createdVioTypes = await _service.CreateVioType(request);
                return createdVioTypes == null ? NotFound() : Ok(createdVioTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfVioType>>> UpdateVioType(int id, RequestOfVioType request)
        {
            try
            {
                var updatedVioTypes = await _service.UpdateVioType(id, request);
                return updatedVioTypes == null ? NotFound() : Ok(updatedVioTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfVioType>>> DeleteVioType(int id)
        {
            try
            {
                var deletedVioTypes = await _service.DeleteVioType(id);
                return deletedVioTypes == null ? NoContent() : Ok(deletedVioTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("school/{schoolId}")]
        public async Task<ActionResult<DataResponse<List<ResponseOfVioType>>>> GetViolationTypesBySchoolId(int schoolId)
        {
            try
            {
                var vioTypes = await _service.GetViolationTypesBySchoolId(schoolId);
                return Ok(vioTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("school/{schoolId}/active")]
        public async Task<ActionResult<DataResponse<List<ResponseOfVioType>>>> GetActiveViolationTypesBySchoolId(int schoolId)
        {
            try
            {
                var vioTypes = await _service.GetActiveViolationTypesBySchoolId(schoolId);
                return Ok(vioTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("violation-group/{violationGroupId}")]
        public async Task<ActionResult<DataResponse<List<ResponseOfVioType>>>> GetViolationTypesByViolationGroupId(int violationGroupId)
        {
            try
            {
                var vioTypes = await _service.GetViolationTypesByViolationGroupId(violationGroupId);
                return Ok(vioTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("api/violation-types/by-group-for-student-supervisor/{violationGroupId}")]
        public async Task<IActionResult> GetViolationTypesByGroupForStudentSupervisor(int violationGroupId)
        {
            var response = await _service.GetViolationTypesByGroupForStudentSupervisor(violationGroupId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }
    }
}
