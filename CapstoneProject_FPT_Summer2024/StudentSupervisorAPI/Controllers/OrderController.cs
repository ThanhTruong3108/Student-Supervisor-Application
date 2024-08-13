using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSupervisorService.Authentication;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.OrderResponse;
using StudentSupervisorService.Service;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private IAuthentication _authenService;
        public OrderController(OrderService orderService, IAuthentication authenService)
        {
            _orderService = orderService;
            _authenService = authenService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "ADMIN")]
        [HttpGet("admin")]
        public async Task<ActionResult<DataResponse<List<OrderResponse>>>> GetOrdersForAdmin(string sortOrder = "asc")
        {
            try
            {
                var response = await _orderService.GetOrdersForAdmin(sortOrder);
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SCHOOL_ADMIN")]
        [HttpGet("school-admin")]
        public async Task<ActionResult<DataResponse<List<OrderResponse>>>> GetOrdersForSchoolAdmin(string sortOrder = "asc")
        {
            try
            {
                // Lấy userId từ JWT
                var userId = _authenService.GetUserIdFromContext(HttpContext);
                if (userId == null)
                {
                    return Unauthorized("Không lấy được UserID từ JWT");
                }
                var response = await _orderService.GetOrdersForSchoolAdmin((int)userId, sortOrder);
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
