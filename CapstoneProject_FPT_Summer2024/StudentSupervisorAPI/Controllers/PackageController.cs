using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Request.ViolationGroupRequest;
using StudentSupervisorService.Models.Response.ViolationGroupResponse;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.PackageResponse;
using StudentSupervisorService.Models.Request.PackageRequest;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/packages")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private PackageService _service;
        public PackageController(PackageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<ResponseOfPackage>>>> GetPackages(string sortOrder)
        {
            try
            {
                var packages = await _service.GetAllPackages(sortOrder);
                return Ok(packages);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfPackage>>> GetPackageById(int id)
        {
            try
            {
                var package = await _service.GetPackageById(id);
                return Ok(package);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPackages(string? name = null, int? minPrice = null, int? maxPrice = null, string? Type = null, string sortOrder = "asc")
        {
            try
            {
                var vioGroup = await _service.SearchPackages(name, minPrice, maxPrice, Type,  sortOrder);
                return Ok(vioGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<ResponseOfPackage>>> CreatePackage(PackageRequest request)
        {
            var createdPackage = await _service.CreatePackage(request);
            return createdPackage == null ? NotFound() : Ok(createdPackage);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DataResponse<ResponseOfPackage>>> UpdatePackage(int id, PackageRequest request)
        {
            var updatedPackage = await _service.UpdatePackage(id, request);
            return updatedPackage == null ? NotFound() : Ok(updatedPackage);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePackage(int id)
        {
            var deletedPackage = _service.DeletePackage(id);
            return deletedPackage == null ? NoContent() : Ok(deletedPackage);
        }
    }
}
