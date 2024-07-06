﻿using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Service;
using StudentSupervisorService.Models.Response.PackageTypeResponse;
using StudentSupervisorService.Models.Request.PackageTypeRequest;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/package-types")]
    [ApiController]
    public class PackageTypeController : ControllerBase
    {
        private PackageTypeService _service;
        public PackageTypeController(PackageTypeService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<DataResponse<List<PackageTypeResponse>>>> GetPackageTypes(string sortOrder = "asc")
        {
            try
            {
                var packageTypes = await _service.GetAllPackageTypes(sortOrder);
                return Ok(packageTypes);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<PackageTypeResponse>>> GetPackageTypeById(int id)
        {
            try
            {
                var packageType = await _service.GetPackageTypeById(id);
                return Ok(packageType);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPackageTypes(string? name = null, string sortOrder = "asc")
        {
            try
            {
                var packageTypes = await _service.SearchPackageTypes(name, sortOrder);
                return Ok(packageTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<PackageTypeResponse>>> CreatePackageType(PackageTypeRequest request)
        {
            var createdPackageTypes = await _service.CreatePackageType(request);
            return createdPackageTypes == null ? NotFound() : Ok(createdPackageTypes);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DataResponse<PackageTypeResponse>>> UpdatePackageType(int id, PackageTypeRequest request)
        {
            var updatedPackageType = await _service.UpdatePackageType(id, request);
            return updatedPackageType == null ? NotFound() : Ok(updatedPackageType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePackageType(int id)
        {
            var deletedPackageTypes = _service.DeletePackageType(id);
            return deletedPackageTypes == null ? NoContent() : Ok(deletedPackageTypes);
        }
    }
}
