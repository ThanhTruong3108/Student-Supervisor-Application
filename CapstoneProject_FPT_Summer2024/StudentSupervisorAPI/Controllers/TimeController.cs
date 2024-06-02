using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Request.SchoolYearRequest;
using StudentSupervisorService.Models.Response.SchoolYearResponse;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.TimeResponse;
using StudentSupervisorService.Models.Request.TimeRequest;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/times")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private TimeService _service;
        public TimeController(TimeService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<DataResponse<List<ResponseOfTime>>>> GetTimes(int page = 1, int pageSize = 5, string sortOrder = "asc")
        {
            try
            {
                var times = await _service.GetAllTimes(page, pageSize, sortOrder);
                return Ok(times);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfTime>>> GetTimeById(int id)
        {
            try
            {
                var time = await _service.GetTimeById(id);
                return Ok(time);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchTimes(byte? slot = null, TimeSpan? time = null, string sortOrder = "asc")
        {
            try
            {
                var times = await _service.SearchTimes(slot, time, sortOrder);
                return Ok(times);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<ResponseOfTime>>> CreateTime(RequestOfTime request)
        {
            var createdTime = await _service.CreateTime(request);
            return createdTime == null ? NotFound() : Ok(createdTime);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfTime>>> UpdateTime(int id, RequestOfTime request)
        {
            var updatedTime = await _service.UpdateTime(id, request);
            return updatedTime == null ? NotFound() : Ok(updatedTime);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTime(int id)
        {
            var deletedTime = _service.DeleteTime(id);
            return deletedTime == null ? NoContent() : Ok(deletedTime);
        }
    }
}
