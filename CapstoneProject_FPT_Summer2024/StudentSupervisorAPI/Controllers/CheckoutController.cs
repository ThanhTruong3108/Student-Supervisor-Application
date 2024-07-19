using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;
using StudentSupervisorService.Models.Request.CheckoutRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.CheckoutResponse;
using StudentSupervisorService.PayOSConfig;
using System;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/checkout")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PayOS _payOS;
        private readonly PayOSConfig _payOSConfig;

        public CheckoutController(PayOS payOS, IHttpContextAccessor httpContextAccessor, PayOSConfig payOSConfig)
        {
            _payOS = payOS;
            _httpContextAccessor = httpContextAccessor;
            _payOSConfig = payOSConfig;
        }

        [HttpGet("history")]
        public async Task<IActionResult> Get([FromQuery] CheckoutResponse queryParams)
        {
            Console.WriteLine($"Code: {queryParams.Code}");
            Console.WriteLine($"ID: {queryParams.Id}");
            Console.WriteLine($"Cancel: {queryParams.Cancel}");
            Console.WriteLine($"Status: {queryParams.Status}");
            Console.WriteLine($"Order Code: {queryParams.OrderCode}");
            if (queryParams.OrderCode != null)
            {
                PaymentLinkInformation paymentLinkInfomation = await _payOS.getPaymentLinkInformation(queryParams.OrderCode);
                return Ok("PaymentLinkInformation: " + paymentLinkInfomation);
            }
            return BadRequest("OrderCode is null");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentLink([FromBody] CreateCheckoutRequest request)
        {
            try
            {
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                List<ItemData> items = new List<ItemData> { new ItemData(request.Item.name, 1, request.Item.price) };
                PaymentData paymentData = new PaymentData(
                    orderCode,
                    request.Item.price * 1,
                    "Thanh toan don hang",
                    items,
                    _payOSConfig.GetCancelUrl(),
                    _payOSConfig.GetReturnUrl());
                CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

                Console.WriteLine("orderCode: " + orderCode);
                return Ok(new DataResponse<CreatePaymentResult> { Data = createPayment, Message = "success", Success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(new DataResponse<string> { Data = "Empty", Message = ex.Message, Success = false });
            }
        }
    }
}
