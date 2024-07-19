using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Net.payOS;
using Net.payOS.Types;
using StudentSupervisorService.Models.Request.CheckoutRequest;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.CheckoutResponse;
using StudentSupervisorService.PayOSConfig;
using StudentSupervisorService.Service;
using System;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/checkout")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly PayOS _payOS;
        private CheckoutService _checkoutService;

        public CheckoutController(PayOS payOS, CheckoutService checkoutService)
        {
            _payOS = payOS;
            _checkoutService = checkoutService;
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
                var checkoutResponse = await _checkoutService.CreateCheckout(request);
                return Ok(checkoutResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
