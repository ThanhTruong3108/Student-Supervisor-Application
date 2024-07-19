using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.PayOSConfig;

namespace StudentSupervisorAPI.Controllers
{
    [Route("api/checkout")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly PayOS _payOS;

        public CheckoutController(PayOS payOS)
        {
            _payOS = payOS;
        }

        [HttpGet("create")]
        public async Task<IActionResult> CreatePaymentLink()
        {
            try
            {
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                ItemData item = new ItemData("Mì tôm hảo hảo ly", 1, 2000);
                List<ItemData> items = new List<ItemData>();
                items.Add(item);
                PaymentData paymentData = new PaymentData(orderCode, 2000, "Thanh toan don hang", items, PayOSConfig.CANCEL_URL, PayOSConfig.RETURN_URL);

                CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

                return Redirect(createPayment.checkoutUrl);
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception);
                return Ok(new Response(-1, "fail", null));
            }
        }
    }
}
