using Net.payOS.Types;
using StudentSupervisorService.Models.Request.CheckoutRequest;
using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service
{
    public interface CheckoutService
    {
        Task<DataResponse<CreatePaymentResult>> CreateCheckout(CreateCheckoutRequest request);
    }
}
