using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Request.ClassRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.ClassResponse;
using StudentSupervisorService.Service;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/classes")]
    [ApiController]
   // [Authorize]
    public class ClassController : ControllerBase
    {
        private readonly ClassService classService;
        public ClassController(ClassService classService)
        {
            this.classService = classService;
        }

        [HttpGet]
        public async Task<ActionResult<DataResponse<List<ClassResponse>>>> GetAllClasses(string sortOrder = "asc")
        {
            try
            {
                var classesResponse = await classService.GetAllClasses(sortOrder);
                return Ok(classesResponse);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataResponse<ClassResponse>>> GetClassById(int id)
        {
            try
            {
                var classReponse = await classService.GetClassById(id);
                return Ok(classReponse);
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DataResponse<ClassResponse>>> CreateClass(ClassCreateRequest request)
        {
            try
            {
                var classResponse = await classService.CreateClass(request);
                return Created("", classResponse);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<DataResponse<ClassResponse>>> UpdateClass(int id, ClassUpdateRequest request)
        {
            try
            {
                var classResponse = await classService.UpdateClass(id, request);
                return Ok(classResponse);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DataResponse<ClassResponse>>> DeleteClass(int id)
        {
            try
            {
                var classResponse = await classService.DeleteClass(id);
                return Ok(classResponse);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("school/{schoolId}")]
        public async Task<ActionResult<DataResponse<List<ClassResponse>>>> GetClassesBySchoolId(int schoolId)
        {
            try
            {
                var classes = await classService.GetClassesBySchoolId(schoolId);
                return Ok(classes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("schedule/{scheduleId}")]
        public async Task<ActionResult<DataResponse<ClassResponse>>> GetClassByScheduleId(int scheduleId)
        {
            try
            {
                var classResponse = await classService.GetClassByScheduleId(scheduleId);
                return Ok(classResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("classes/{userId}")]
        public async Task<IActionResult> GetClassesByUserId(int userId)
        {
            var result = await classService.GetClassesByUserId(userId);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
    }
}
