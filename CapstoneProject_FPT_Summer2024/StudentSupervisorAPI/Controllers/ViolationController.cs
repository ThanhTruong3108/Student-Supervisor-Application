using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.ViolationResponse;
using StudentSupervisorService.Models.Request.ViolationRequest;
using Domain.Enums.Status;

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
        public async Task<ActionResult<DataResponse<List<ResponseOfViolation>>>> GetAllViolations(string sortOrder)
        {
            try
            {
                var violations = await _service.GetAllViolations(sortOrder);
                return Ok(violations);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> GetViolationById(int id)
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
        public async Task<IActionResult> SearchViolations(
            int? classId, 
            int? violationTypeId,
            int? studentInClassId,
            int? teacherId, 
            string? name,
            string? description,
            DateTime? date,
            ViolationStatusEnums? status,
            string sortOrder = "asc")
        {
            try
            {
                var violations = await _service.SearchViolations(classId, violationTypeId, studentInClassId, teacherId, name, description, date, status.ToString(), sortOrder);
                return Ok(violations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Create violation for student supervisor
        [HttpPost("student")]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> CreateViolationForStudentSupervisor([FromForm] RequestOfCreateViolation request)
        {
            try
            {
                var createdViolation = await _service.CreateViolationForStudentSupervisor(request);
                return Ok(createdViolation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Create violation for supervisor
        [HttpPost("supervisor")]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> CreateViolationForSupervisor([FromForm] RequestOfCreateViolation request)
        {
            try
            {
                var createdViolation = await _service.CreateViolationForSupervisor(request);
                return Ok(createdViolation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> UpdateViolation(int id ,[FromForm] RequestOfUpdateViolation request)
        {
            try
            {
                var updatedViolation = await _service.UpdateViolation(id, request);
                return Ok(updatedViolation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteViolation(int id)
        {
            try
            {
                var deletedViolation = _service.DeleteViolation(id);
                return Ok(deletedViolation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/approve")]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> ApproveViolation(int id)
        {
            try
            {
                var approvedViolation = await _service.ApproveViolation(id);
                return Ok(approvedViolation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/reject")]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> RejectViolation(int id)
        {
            try
            {
                var rejectedViolation = await _service.RejectViolation(id);
                return Ok(rejectedViolation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("students/{studentCode}")]
        public async Task<ActionResult<DataResponse<List<ResponseOfViolation>>>> GetViolationsByStudentCode(string studentCode)
        {
            try
            {
                var violationsResponse = await _service.GetViolationsByStudentCode(studentCode);
                return Ok(violationsResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{studentCode}/{year}")]
        public async Task<ActionResult<DataResponse<List<ResponseOfViolation>>>> GetViolationsByStudentCodeAndYear(string studentCode, short year)
        {
            try
            {
                var violationsResponse = await _service.GetViolationsByStudentCodeAndYear(studentCode, year);
                return Ok(violationsResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("count/{studentCode}")]
        public async Task<ActionResult<DataResponse<Dictionary<int, int>>>> GetViolationCountByYear(string studentCode)
        {
            try
            {
                var violationCountResponse = await _service.GetViolationCountByYear(studentCode);
                return Ok(violationCountResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
