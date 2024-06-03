using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.YearPackageResponse;
using StudentSupervisorService.Models.Request.YearPackageRequest;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/year-packages")]
    [ApiController]
    public class YearPackageController : ControllerBase
    {
        private YearPackageService _service;
        public YearPackageController(YearPackageService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<DataResponse<List<ResponseOfYearPackage>>>> GetYearPackages(int page = 1, int pageSize = 5, string sortOrder = "asc")
        {
            try
            {
                var yearPackages = await _service.GetAllYearPackages(page, pageSize, sortOrder);
                return Ok(yearPackages);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfYearPackage>>> GetYearPackageById(int id)
        {
            try
            {
                var yearPackage = await _service.GetYearPackageById(id);
                return Ok(yearPackage);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchYearPackages(int? schoolYearId = null, int? packageId = null, int? minNumberOfStudent = null, int? maxNumberOfStudent = null, string sortOrder = "asc")
        {
            try
            {
                var yearPackages = await _service.SearchYearPackages(schoolYearId, packageId, minNumberOfStudent, maxNumberOfStudent, sortOrder);
                return Ok(yearPackages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<ResponseOfYearPackage>>> CreateYearPackage(RequestOfYearPackage request)
        {
            var createdYearPackage = await _service.CreateYearPackage(request);
            return createdYearPackage == null ? NotFound() : Ok(createdYearPackage);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfYearPackage>>> UpdateYearPackage(int id, RequestOfYearPackage request)
        {
            var updatedYearPackage = await _service.UpdateYearPackage(id, request);
            return updatedYearPackage == null ? NotFound() : Ok(updatedYearPackage);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteYearPackage(int id)
        {
            var deletedYearPackage = _service.DeleteYearPackage(id);
            return deletedYearPackage == null ? NoContent() : Ok(deletedYearPackage);
        }
    }
}
