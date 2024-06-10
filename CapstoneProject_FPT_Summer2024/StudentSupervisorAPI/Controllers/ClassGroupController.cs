using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response.ClassResponse;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.ClassGroupResponse;
using StudentSupervisorService.Models.Request.ClassRequest;
using StudentSupervisorService.Models.Request.ClassGroupRequest;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/class-groups")]
    [ApiController]
    public class ClassGroupController : ControllerBase
    {
        private readonly ClassGroupService classGroupService;
        public ClassGroupController(ClassGroupService classGroupService)
        {
            this.classGroupService = classGroupService;
        }

        [HttpGet]

        public async Task<ActionResult<DataResponse<List<ClassGroupResponse>>>> GetAllClassGroups(string sortOrder = "asc")
        {
            try
            {
                var classGroupsResponse = await classGroupService.GetAllClassGroups(sortOrder);
                return Ok(classGroupsResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<ClassGroupResponse>>> GetClassGroupById(int id)
        {
            try
            {
                var classGroupResponse = await classGroupService.GetClassGroupById(id);
                return Ok(classGroupResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<DataResponse<List<ClassGroupResponse>>>> SearchClassGroups(
            string? classGroupName,
            string? hall,
            string? status,
            string sortOrder)
        {
            try
            {
                var classGroupsResponse = await classGroupService.SearchClassGroups(classGroupName, hall, status, sortOrder);
                return Ok(classGroupsResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<ClassGroupResponse>>> CreateClassGroup(ClassGroupCreateRequest request)
        {
            try
            {
                var classGroupResponse = await classGroupService.CreateClassGroup(request);
                return Created("", classGroupResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<DataResponse<ClassGroupResponse>>> UpdateClassGroup(ClassGroupUpdateRequest request)
        {
            try
            {
                var classGroupResponse = await classGroupService.UpdateClassGroup(request);
                return Ok(classGroupResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DataResponse<ClassGroupResponse>>> DeleteClassGroup(int id)
        {
            try
            {
                var classGroupResponse = await classGroupService.DeleteClassGroup(id);
                return Ok(classGroupResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
