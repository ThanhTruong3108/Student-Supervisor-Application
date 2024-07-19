using AutoMapper;
using Infrastructures.Interfaces.IUnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using StudentSupervisorService.Models.Response.AdminResponse;
using StudentSupervisorService.Models.Response;
using StudentSupervisorService.Models.Response.CheckoutResponse;
using StudentSupervisorService.PayOSConfig;
using Net.payOS.Types;
using StudentSupervisorService.Models.Request.CheckoutRequest;


namespace StudentSupervisorService.Service.Implement
{
    public class CheckoutImplement : CheckoutService
    {
        private readonly PayOS _payOS;
        private readonly PayOSConfig.PayOSConfig _payOSConfig;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CheckoutImplement(IUnitOfWork unitOfWork, IMapper mapper, 
            PayOS payOS, IHttpContextAccessor httpContextAccessor, PayOSConfig.PayOSConfig payOSConfig)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _payOS = payOS;
            _payOSConfig = payOSConfig;
        }

        public async Task<DataResponse<CreatePaymentResult>> CreateCheckout(CreateCheckoutRequest request)
        {
            var response = new DataResponse<CreatePaymentResult>();
            try
            {
                var existingPackage = await _unitOfWork.Package.GetPackageById(request.PackageID);
                if (existingPackage is null)
                {
                    response.Message = "The Package does not exist";
                    response.Success = false;
                    return response;
                }
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                List<ItemData> items = new List<ItemData> { new ItemData(existingPackage.Name, 1, existingPackage.Price) };
                PaymentData paymentData = new PaymentData(
                    orderCode,
                    existingPackage.Price,
                    "Thanh toan don hang",
                    items,
                    _payOSConfig.GetCancelUrl(),
                    _payOSConfig.GetReturnUrl());
                CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

                response.Data = createPayment;
                response.Message = "success";
                response.Success = true;
            } catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Oops! Some thing went wrong.\n" + ex.Message;
                response.Success = false;
            }
            return response;
        }
    }
}
