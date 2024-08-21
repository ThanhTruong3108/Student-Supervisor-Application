using StudentSupervisorService.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSupervisorService.Service
{
    public interface ValidationService
    {
        // Batch job: - Xóa các Order đang PENDING quá 1 ngày chưa thanh toán
        //            - Update Status của SchoolYear thành FINISHED khi hết năm đó
        //            - Update Status của YearPackage thành EXPIRED khi hết hạn
        //            - Update Status của Violation thành ACCEPTED khi GVCN ko duyệt sau 1 ngày
        // 
        // Validate:  - Khi trường tạo SchoolYear mới, sau đó bắt buộc phải đăng ký 1 Package

        Task<bool> IsAnyValidPackageInSpecificYear(int schoolId, int year);
        // lấy jwt userid => lấy schoolid => lấy schoolyearid đang ONGOING theo schoolid và year từ violation
        // => kiếm trong yearpackage bằng schoolyearid xem có package nào VALID ko

        Task<bool> IsHighSchoolDuplicated(int registeredId, string? code, string? name);

        // chỉ được tạo Violation trong vòng 1 tiếng tính từ Time trong PatrolSchedule
        Task<bool> IsViolationCreatedOver1HourFromTimeInPatrolSchedule(int patrolScheduleId);
    }
}
