using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Request.SchoolConfigRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.SchoolConfigResponse;
using StudentSupervisorService.Service;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/school-configs")]
    [ApiController]
    public class SchoolConfigController : ControllerBase
    {
        private readonly SchoolConfigService schoolConfigService;
        public SchoolConfigController(SchoolConfigService schoolConfigService)
        {
            this.schoolConfigService = schoolConfigService;
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<SchoolConfigResponse>>>> GetAllSchoolConfigs(string sortOrder = "asc")
        {
            try
            {
                var schoolConfigsResponse = await schoolConfigService.GetAllSchoolConfigs(sortOrder);
                return Ok(schoolConfigsResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<SchoolConfigResponse>>> GetSchoolConfigById(int id)
        {
            try
            {
                var schoolConfigResponse = await schoolConfigService.GetSchoolConfigById(id);
                return Ok(schoolConfigResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<DataResponse<List<SchoolConfigResponse>>>> SearchSchoolConfigs(
            int? schoolId,
            string? name,
            string? code,
            string? description,
            string? status,
            string sortOrder)
        {
            try
            {
                var schoolConfigsResponse = await schoolConfigService.SearchSchoolConfigs(schoolId, name, code, description, status, sortOrder);
                return Ok(schoolConfigsResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<SchoolConfigResponse>>> CreateSchoolConfig(SchoolConfigCreateRequest schoolConfigRequest)
        {
            try
            {
                var schoolConfigResponse = await schoolConfigService.CreateSchoolConfig(schoolConfigRequest);
                return Ok(schoolConfigResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<DataResponse<SchoolConfigResponse>>> UpdateSchoolConfig(SchoolConfigUpdateRequest schoolConfigRequest)
        {
            try
            {
                var schoolConfigResponse = await schoolConfigService.UpdateSchoolConfig(schoolConfigRequest);
                return Ok(schoolConfigResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DataResponse<SchoolConfigResponse>>> DeleteSchoolConfig(int id)
        {
            try
            {
                var schoolConfigResponse = await schoolConfigService.DeleteSchoolConfig(id);
                return Ok(schoolConfigResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
