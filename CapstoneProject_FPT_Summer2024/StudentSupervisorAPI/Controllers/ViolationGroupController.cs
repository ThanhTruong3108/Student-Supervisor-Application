using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.ViolationGroupResponse;
using StudentSupervisorService.Models.Request.ViolationGroupRequest;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/violation-groups")]
    [ApiController]
    public class ViolationGroupController : ControllerBase
    {
        private ViolationGroupService _service;
        public ViolationGroupController(ViolationGroupService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<DataResponse<List<ResponseOfVioGroup>>>> GetVioGroups()
        {
            try
            {
                var vioGroups = await _service.GetAllVioGroups();
                return Ok(vioGroups);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfVioGroup>>> GetVioGroupById(int id)
        {
            try
            {
                var vioGroup = await _service.GetVioGroupById(id);
                return Ok(vioGroup);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchVioGroups(string? code = null, string? name = null, string sortOrder = "asc")
        {
            try
            {
                var vioGroup = await _service.SearchVioGroups(code, name, sortOrder);
                return Ok(vioGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<ResponseOfVioGroup>>> CreateVioGroup(RequestOfVioGroup request)
        {
            var createdVioGroup = await _service.CreateVioGroup(request);
            return createdVioGroup == null ? NotFound() : Ok(createdVioGroup);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfVioGroup>>> UpdateVioGroup(int id, RequestOfVioGroup request)
        {
            var updatedVioGroup = await _service.UpdateVioGroup(id, request);
            return updatedVioGroup == null ? NotFound() : Ok(updatedVioGroup);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVioGroup(int id)
        {
            var deletedVioGroup = _service.DeleteVioGroup(id);
            return deletedVioGroup == null ? NoContent() : Ok(deletedVioGroup);
        }
    }
}
