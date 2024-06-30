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

        //[HttpGet]
        //public async Task<ActionResult<DataResponse<List<ResponseOfViolation>>>> GetViolations(string sortOrder)
        //{
        //    try
        //    {
        //        var violations = await _service.GetAllViolations(sortOrder);
        //        return Ok(violations);
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<DataResponse<ResponseOfViolation>>> GetviolationById(int id)
        //{
        //    try
        //    {
        //        var violation = await _service.GetViolationById(id);
        //        return Ok(violation);
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

        //[HttpGet("search")]
        //public async Task<IActionResult> SearchViolations(int? classId = null, int? teacherId = null, int? vioTypeId = null, string? code = null, string? name = null, DateTime? date = null, string sortOrder = "asc")
        //{
        //    try
        //    {
        //        var violations = await _service.SearchViolations(classId, teacherId, vioTypeId, code, name, date, sortOrder);
        //        return Ok(violations);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //// Create violation for student supervisor
        //[HttpPost("student")]
        //public async Task<ActionResult<DataResponse<ResponseOfViolation>>> CreateViolationForStudentSupervisor([FromForm] RequestOfCreateViolation request)
        //{
        //    try
        //    {
        //        var createdViolation = await _service.CreateViolationForStudentSupervisor(request);
        //        return Ok(createdViolation);
        //    } catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //// Create violation for supervisor
        //[HttpPost("supervisor")]
        //public async Task<ActionResult<DataResponse<ResponseOfViolation>>> CreateViolationForSupervisor([FromForm] RequestOfCreateViolation request)
        //{
        //    try
        //    {
        //        var createdViolation = await _service.CreateViolationForSupervisor(request);
        //        return Ok(createdViolation);
        //    } catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<DataResponse<ResponseOfViolation>>> UpdateViolation(int id, [FromForm] RequestOfUpdateViolation request)
        //{
        //    try
        //    {
        //        var updatedViolation = await _service.UpdateViolation(id, request);
        //        return Ok(updatedViolation);
        //    } catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteViolation(int id)
        //{
        //    try
        //    {
        //        var deletedViolation = _service.DeleteViolation(id);
        //        return Ok(deletedViolation);
        //    } catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPut("{id}/approve")]
        //public async Task<ActionResult<DataResponse<ResponseOfViolation>>> ApproveViolation(int id)
        //{
        //    try
        //    {
        //        var approvedViolation = await _service.ApproveViolation(id);
        //        return Ok(approvedViolation);
        //    } catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpPut("{id}/reject")]
        //public async Task<ActionResult<DataResponse<ResponseOfViolation>>> RejectViolation(int id)
        //{
        //    try
        //    {
        //        var rejectedViolation = await _service.RejectViolation(id);
        //        return Ok(rejectedViolation);
        //    } catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
