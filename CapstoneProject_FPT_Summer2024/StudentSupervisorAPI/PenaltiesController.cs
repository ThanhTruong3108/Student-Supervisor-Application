using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Models.Commons;
using StudentSupervisorService.Models.PenaltyViewModels;
using StudentSupervisorService.Service;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/highschools")]
    [ApiController]
    public class PenaltiesController : ControllerBase
    {
        private static PenaltyService _penaltyService;
        public PenaltiesController(PenaltyService penaltyService)
        {
            _penaltyService = penaltyService;
        }

        [HttpGet("GetAll")]
        public async Task<Response> GetAll(int pageIndex = 0, int pageSize = 10) => await _penaltyService.GetAll(pageIndex, pageSize);

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreatePenaltyRequest request)
        {
            try
            {
                var result = await _penaltyService.Create(request);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Create Successfully");
        }

        [HttpGet("GetById/{Id}")]
        public async Task<Response> GetById(int Id) => await _penaltyService.GetById(Id);

        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> Update(int Id, UpdatePenaltyRequest request)
        {
            var check = await _penaltyService.GetById(Id);
            if (check == null)
            {
                return NotFound();
            }
            var result = await _penaltyService.Update(Id, request);
            return Ok(result);

        }

        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> Update(int Id)
        {
            var model = await _penaltyService.GetById(Id);
            if (model == null)
            {
                return NotFound();
            }
            var obj = model.Result;
           await  _penaltyService.Delete((Penalty)obj);
            return Ok();

        }
        }
    }


