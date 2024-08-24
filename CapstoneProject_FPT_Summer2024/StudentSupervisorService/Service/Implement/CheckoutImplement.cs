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
using StudentSupervisorService.Models.Response.OrderResponse;
using StudentSupervisorService.Models.Request.OrderRequest;
using Domain.Enums.Status;
using Domain.Entity;


namespace StudentSupervisorService.Service.Implement
{
    public class CheckoutImplement : CheckoutService
    {
        private readonly OrderService _orderService;
        private readonly PayOS _payOS;
        private readonly PayOSConfig.PayOSConfig _payOSConfig;
        private readonly IUnitOfWork _unitOfWork;
        public CheckoutImplement(IUnitOfWork unitOfWork, OrderService orderService,
            PayOS payOS, PayOSConfig.PayOSConfig payOSConfig)
        {
            _unitOfWork = unitOfWork;
            _payOS = payOS;
            _payOSConfig = payOSConfig;
            _orderService = orderService;
        }

        public async Task<DataResponse<CreatePaymentResult>> CreateCheckout(int? userIdFromJWT, CreateCheckoutRequest request)
        {
            var response = new DataResponse<CreatePaymentResult>();
            try
            {
                // kiểm tra xem package có tồn tại không
                var existingPackage = await _unitOfWork.Package.GetPackageById(request.PackageID);
                if (existingPackage is null)
                {
                    response.Message = "Gói Package không tồn tại";
                    response.Success = false;
                    return response;
                }

                // phải tạo SchoolYear trước khi mua Package
                // lấy schoolId từ userId
                var schoolId = (await _unitOfWork.User.GetUserById((int)userIdFromJWT)).SchoolId;

                var anySchoolYear = await _unitOfWork.SchoolYear.GetOngoingSchoolYearBySchoolIdAndYear((int)schoolId, DateTime.Now.Year);

                // nếu chưa tạo SchoolYear năm nay thì trả về thông báo lỗi
                if (anySchoolYear == null)
                {
                    response.Data = "Empty";
                    response.Message = "Không có Niên khóa năm " + DateTime.Now.Year + ", vui lòng tạo niên khóa trước";
                    response.Success = false;
                    return response;
                }

                // nếu SchoolYear đã thanh toán gói Package này rồi thì không cho thanh toán nữa
                if (await _unitOfWork.YearPackage.GetValidYearPackageBySchoolYearIdAndPackageId(anySchoolYear.SchoolYearId, request.PackageID) != null)
                {
                    response.Data = "Empty";
                    response.Message = "Niên khóa năm " + DateTime.Now.Year + " đã thanh toán " + existingPackage.Name + " rồi";
                    response.Success = false;
                    return response;
                }

                // tạo thông tin thanh toán
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
                
                // tạo object OrderCreateRequest để insert Order xuống DB
                OrderCreateRequest orderCreateRequest = new OrderCreateRequest
                {
                    UserId = userIdFromJWT,
                    PackageId = request.PackageID,
                    OrderCode = orderCode,
                    Description = "Thanh toán cho " + existingPackage.Name,
                    Total = existingPackage.Price,
                    AmountPaid = 0,
                    AmountRemaining = existingPackage.Price,
                    CounterAccountBankName = null,
                    CounterAccountNumber = null,
                    CounterAccountName = null
                };
                await _orderService.CreateOrder(orderCreateRequest);

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

        public async Task<DataResponse<OrderResponse>> VerifyTransaction(CheckoutResponse queryParams)
        {
            var response = new DataResponse<OrderResponse>();
            try
            {
                if (queryParams.OrderCode == null)
                {
                    response.Data = "Empty";
                    response.Message = "OrderCode không tồn tại";
                    response.Success = false;
                    return response;
                }

                PaymentLinkInformation paymentLinkInfomation = await _payOS.getPaymentLinkInformation(queryParams.OrderCode);
                if (paymentLinkInfomation is null)
                {
                    response.Data = "Empty";
                    response.Message = "PayOS không tìm thấy thông tin thanh toán";
                    response.Success = false;
                    return response;
                }

                // người dùng thanh toán thành công
                if (queryParams.Status == "PAID")
                {
                    // cập nhật status Order thành PAID
                    OrderUpdateRequest orderUpdateRequest = new OrderUpdateRequest
                    {
                        OrderCode = (int)queryParams.OrderCode,
                        AmountPaid = paymentLinkInfomation.amountPaid,
                        AmountRemaining = 0,
                        CounterAccountBankName = paymentLinkInfomation.transactions[0].counterAccountBankName,
                        CounterAccountNumber = paymentLinkInfomation.transactions[0].counterAccountNumber,
                        CounterAccountName = paymentLinkInfomation.transactions[0].counterAccountName,
                        Status = OrderStatusEnum.PAID.ToString()
                    };

                    // lấy order hiện tại
                    var currentOrder = await _unitOfWork.Order.GetOrderByOrderCode((int)queryParams.OrderCode);
                    // lấy schoolId từ currentOrder
                    var schoolId = currentOrder.User.School.SchoolId;
                    // lấy SchoolYear đang ONGOING theo schoolId và year (từ currentOrder)
                    var schoolYear = await _unitOfWork.SchoolYear.GetOngoingSchoolYearBySchoolIdAndYear(schoolId, (short)currentOrder.Date.Value.Year);
                    // nếu không có SchoolYear nào đang ONGOING thì trả về thông báo lỗi
                    if (schoolYear == null)
                    {
                        response.Data = "Empty";
                        response.Message = "Không có SchoolYear năm "+ currentOrder.Date.Value.Year + " đang ONGOING";
                        response.Success = false;
                        return response;
                    }
                    // nếu có => thêm 1 Package vào YearPackage của HighSchool
                    // xử lý trường hợp PayOS gọi lại return_url 2 lần
                    if (await _unitOfWork.YearPackage.GetValidYearPackageBySchoolYearIdAndPackageId(schoolYear.SchoolYearId, currentOrder.PackageId) == null)
                    {
                        await _unitOfWork.YearPackage.CreateYearPackage(
                            new YearPackage
                            {
                                SchoolYearId = schoolYear.SchoolYearId,
                                PackageId = currentOrder.PackageId,
                                Status = YearPackageStatusEnums.VALID.ToString()
                            }
                        );
                    }

                    var updated = await _orderService.UpdateOrder(orderUpdateRequest);
                    response.Data = "https://school-fe-admin-main.vercel.app/payment/success";
                    response.Message = "Thanh toán thành công";
                    response.Success = true;
                }
                // người dùng hủy thanh toán
                else if (queryParams.Status == "CANCELLED")
                {
                    // cập nhật status Order thành CANCELLED
                    OrderUpdateRequest orderUpdateRequest = new OrderUpdateRequest
                    {
                        OrderCode = (int)queryParams.OrderCode,
                        AmountPaid = 0,
                        AmountRemaining = paymentLinkInfomation.amountRemaining,
                        CounterAccountBankName = null,
                        CounterAccountNumber = null,
                        CounterAccountName = null,
                        Status = OrderStatusEnum.CANCELLED.ToString()
                    };

                    var updated = await _orderService.UpdateOrder(orderUpdateRequest);
                    response.Data = "https://school-fe-admin-main.vercel.app/payment/failure";
                    response.Message = "Đã hủy thanh toán";
                    response.Success = true;
                }

            } catch (Exception ex)
            {
                response.Data = "Empty";
                response.Message = "Lỗi phần thanh toán" + ex.Message
                    + (ex.InnerException != null ? ex.InnerException.Message : "");
                response.Success = false;
            }
            return response;
        }
    }
}
