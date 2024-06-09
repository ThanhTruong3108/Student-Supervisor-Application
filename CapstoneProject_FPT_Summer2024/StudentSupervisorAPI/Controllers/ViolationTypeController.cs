using Microsoft.AspNetCore.Mvc;

using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.ViolationTypeResponse;
using StudentSupervisorService.Models.Request.ViolationTypeRequest;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/violation-types")]
    [ApiController]
    public class ViolationTypeController : ControllerBase
    {
        private ViolationTypeService _service;
        public ViolationTypeController(ViolationTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<ResponseOfVioType>>>> GetVioTypes()
        {
            try
            {
                var vioTypes = await _service.GetAllVioTypes();
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

        [HttpGet("search")]
        public async Task<IActionResult> SearchVioTypes(int? vioGroupId = null, string? name =null, string sortOrder = "asc")
        {
            try
            {
                var vioTypes = await _service.SearchVioTypes(vioGroupId,  name, sortOrder);
                return Ok(vioTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<ResponseOfVioType>>> CreateVioType(RequestOfVioType request)
        {
            var createdVioTypes = await _service.CreateVioType(request);
            return createdVioTypes == null ? NotFound() : Ok(createdVioTypes);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfVioType>>> UpdateVioType(int id, RequestOfVioType request)
        {
            var updatedVioTypes = await _service.UpdateVioType(id, request);
            return updatedVioTypes == null ? NotFound() : Ok(updatedVioTypes);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVioType(int id)
        {
            var deletedVioTypes = _service.DeleteVioType(id);
            return deletedVioTypes == null ? NoContent() : Ok(deletedVioTypes);
        }
    }
}
