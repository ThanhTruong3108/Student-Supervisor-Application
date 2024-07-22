using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service.Implement
{
    public class ValidationImplement : ValidationService
    {
        // Batch job: - Xóa các Order đang PENDING quá 1 ngày chưa thanh toán
        //            - Update Status của SchoolYear thành FINISHED khi hết năm đó
        //            - Update Status của YearPackage thành EXPIRED khi hết hạn
        //            - Update Status của Discipline thành DONE khi EndDate < DateTime.Now
    }
}
