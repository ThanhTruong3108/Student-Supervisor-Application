﻿using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.ViolationResponse;
using StudentSupervisorService.Models.Request.ViolationRequest;
using Domain.Enums.Status;
using Microsoft.AspNetCore.Authorization;
using StudentSupervisorService.Authentication;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/violations")]
    [ApiController]
    [RequestSizeLimit(104857600)]
    [Authorize]
    public class ViolationController : ControllerBase
    {
        private ViolationService _service;
        private IAuthentication _authenService;
        public ViolationController(ViolationService service, IAuthentication authenService)
        {
            _service = service;
            _authenService = authenService;
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<ResponseOfViolation>>>> GetAllViolations(string sortOrder = "asc")
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

        [HttpGet("by-class/{classId}")]
        public async Task<ActionResult<DataResponse<List<ResponseOfViolation>>>> GetViolationsByClassId(int classId, string sortOrder = "asc")
        {
            try
            {
                var violations = await _service.GetViolationsByClassId(classId, sortOrder);
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

        // Create violation for student supervisor
        [Authorize(Roles = "STUDENT_SUPERVISOR")]
        [HttpPost("student")]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> CreateViolationForStudentSupervisor([FromForm] RequestOfStuSupervisorCreateViolation request)
        {
            try
            {
                // Lấy userId từ JWT
                var userId = _authenService.GetUserIdFromContext(HttpContext);
                if (userId == null)
                {
                    return Unauthorized("Không lấy được UserID từ JWT");
                }
                var createdViolation = await _service.CreateViolationForStudentSupervisor((int)userId, request);
                return Ok(createdViolation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Create violation for supervisor
        [Authorize(Roles = "SUPERVISOR")]
        [HttpPost("supervisor")]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> CreateViolationForSupervisor([FromForm] RequestOfSupervisorCreateViolation request)
        {
            try
            {
                // Lấy userId từ JWT
                var userId = _authenService.GetUserIdFromContext(HttpContext);
                if (userId == null)
                {
                    return Unauthorized("Không lấy được UserID từ JWT");
                }
                var createdViolation = await _service.CreateViolationForSupervisor((int)userId, request);
                return Ok(createdViolation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update violation for student supervisor
        [Authorize(Roles = "STUDENT_SUPERVISOR")]
        [HttpPut("student-supervisor")]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> UpdateViolationForStudentSupervisor(int id, [FromForm] RequestOfUpdateViolationForStudentSupervisor request)
        {
            try
            {
                var updatedViolation = await _service.UpdateViolationForStudentSupervisor(id, request);
                return Ok(updatedViolation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update violation for supervisor
        [Authorize(Roles = "SUPERVISOR")]
        [HttpPut("supervisor")]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> UpdateViolationForSupervisor(int id, [FromForm] RequestOfUpdateViolationForSupervisor request)
        {
            try
            {
                var updatedViolation = await _service.UpdateViolationForSupervisor(id, request);
                return Ok(updatedViolation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "SUPERVISOR")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> DeleteViolation(int id)
        {
            try
            {
                var deletedViolation = await _service.DeleteViolation(id);
                return Ok(deletedViolation);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [Authorize(Roles = "SUPERVISOR")]
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

        [Authorize(Roles = "SUPERVISOR")]
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

        [Authorize(Roles = "SUPERVISOR")]
        [HttpPut("{id}/complete")]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> CompleteViolation(int id)
        {
            try
            {
                var completedViolation = await _service.CompleteViolation(id);
                return Ok(completedViolation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("school/{schoolId}")]
        public async Task<ActionResult<DataResponse<List<ResponseOfViolation>>>> GetViolationsBySchoolId(int schoolId, string sortOrder = "asc", [FromQuery] short? year = null, [FromQuery] string? semesterName = null, [FromQuery] int? month = null, [FromQuery] int? weekNumber = null)
        {
            try
            {
                var violations = await _service.GetViolationsBySchoolId(schoolId, sortOrder, year, semesterName, month, weekNumber);
                return Ok(violations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{userId}/teachers")]
        public async Task<ActionResult<DataResponse<List<ResponseOfViolation>>>> GetViolationsByUserId(int userId, string sortOrder = "asc", [FromQuery] short? year = null, [FromQuery] string? semesterName = null, [FromQuery] int? month = null, [FromQuery] int? weekNumber = null)
        {
            try
            {
                var violation = await _service.GetViolationsByUserId(userId, sortOrder, year, semesterName, month, weekNumber);
                return Ok(violation);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("discipline/{disciplineId}")]
        public async Task<ActionResult<DataResponse<ResponseOfViolation>>> GetViolationByDisciplineId(int disciplineId)
        {
            try
            {
                var violation = await _service.GetViolationByDisciplineId(disciplineId);
                return Ok(violation);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("user/{userId}/student-supervisors")]
        public async Task<ActionResult<DataResponse<List<ResponseOfViolation>>>> GetViolationsByUserRoleStudentSupervisor(int userId, string sortOrder = "asc")
        {
            try
            {
                var violations = await _service.GetViolationsByUserRoleStudentSupervisor(userId, sortOrder);
                return Ok(violations);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("user/{userId}/supervisors")]
        public async Task<ActionResult<DataResponse<List<ResponseOfViolation>>>> GetViolationsByUserRoleSupervisor(int userId, string sortOrder = "asc")
        {
            try
            {
                var violations = await _service.GetViolationsByUserRoleSupervisor(userId, sortOrder);
                return Ok(violations);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("supervisor/{userId}")]
        public async Task<ActionResult<DataResponse<List<ResponseOfViolation>>>> GetViolationsBySupervisorUserId(int userId, string sortOrder = "asc", [FromQuery] short? year = null, [FromQuery] string? semesterName = null, [FromQuery] int? month = null, [FromQuery] int? weekNumber = null)
        {
            try
            {
                var violations = await _service.GetViolationsBySupervisorUserId(userId, sortOrder, year, semesterName, month, weekNumber);
                return Ok(violations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
