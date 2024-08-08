using Coravel.Invocable;
using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service.Implement
{
    public class DailyScheduleImplement : IInvocable
    {
        private readonly IUnitOfWork _unitOfWork;

        public DailyScheduleImplement(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Invoke()
        {
            var deletePendingOrdersResult = await DeletePendingOrders();
            await Console.Out.WriteLineAsync(deletePendingOrdersResult ? "DeletePendingOrders success" : "DeletePendingOrders failed");

            var autoAcceptViolationResult = await AutoAcceptViolation();
            await Console.Out.WriteLineAsync(autoAcceptViolationResult ? "AutoAcceptViolation success" : "AutoAcceptViolation failed");

            var updateSchoolYearAndYearPackageResult = await UpdateSchoolYearAndYearPackage();
            await Console.Out.WriteLineAsync(updateSchoolYearAndYearPackageResult ? "UpdateSchoolYearAndYearPackage success" : "UpdateSchoolYearAndYearPackage failed");

            var deleteClassIfOverSchoolYearResult = await DeleteClassIfOverSchoolYear();
            await Console.Out.WriteLineAsync(deleteClassIfOverSchoolYearResult ? "DeleteClassIfOverSchoolYear success" : "DeleteClassIfOverSchoolYear failed");
        }

        // Xóa các order đang PENDING quá 1 ngày chưa thanh toán
        private async Task<bool> DeletePendingOrders()
        {
            try
            {
                var orders = await _unitOfWork.Order.GetPendingOrdersOver1Day();
                await Console.Out.WriteLineAsync("ORDER COUNT: " + orders.Count);
                if (orders != null)
                {
                    //  xóa các order đang PENDING quá 1 ngày chưa thanh toán
                    await _unitOfWork.Order.DeleteMultipleOrders(orders);
                }
                return true;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("Error in DeletePendingOrders: " + e.Message);
                return false;
            }
        }

        // Update Status của Violation thành ACCEPTED khi GVCN ko duyệt sau 1 ngày
        private async Task<bool> AutoAcceptViolation()
        {
            try
            {
                var violations = await _unitOfWork.Violation.GetApprovedViolationsOver1Day();
                await Console.Out.WriteLineAsync("violations COUNT: " + violations.Count);
                if (violations != null)
                {
                    //  Update Status của Violation thành ACCEPTED khi GVCN ko duyệt sau 1 ngày
                    await _unitOfWork.Violation.AcceptMultipleViolations(violations);
                }
                return true;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("Error in AutoAcceptViolation: " + e.Message);
                return false;
            }
        }

        // Update Status của SchoolYear thành FINISHED khi hết năm đó
        // Update Status của YearPackage thành EXPIRED khi hết hạn
        private async Task<bool> UpdateSchoolYearAndYearPackage()
        {
            try
            {
                var schoolYears = await _unitOfWork.SchoolYear.GetOngoingSchoolYearsOver1Day();
                await Console.Out.WriteLineAsync("schoolYears COUNT: " + schoolYears.Count);
                if (schoolYears != null)
                {
                    foreach (var schoolYear in schoolYears)
                    {
                        foreach (var yearPackage in schoolYear.YearPackages)
                        {
                            yearPackage.Status = YearPackageStatusEnums.EXPIRED.ToString();
                        }
                        schoolYear.Status = SchoolYearStatusEnums.FINISHED.ToString();
                    }
                    await _unitOfWork.SchoolYear.UpdateMultipleSchoolYears(schoolYears);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("Error in UpdateStatus: " + e.Message);
                return false;
            }
        }

        // Xóa các Class khi hết năm học
        private async Task<bool> DeleteClassIfOverSchoolYear()
        {
            bool result = false;
            try
            {
                var classes = await _unitOfWork.Class.GetActiveClassesBySchoolYearId();
                await Console.Out.WriteLineAsync("classes COUNT: " + classes.Count);
                if (classes != null)
                {
                    foreach (var classEntity in classes)
                    {
                        classEntity.Status = ClassStatusEnums.INACTIVE.ToString();
                        await _unitOfWork.Class.UpdateClass(classEntity);
                        result = true;
                    }
                }
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("Error in DeleteClassIfOverSchoolYear: " + e.Message);
                return false;
            }
            return result;
        }
    }
}
