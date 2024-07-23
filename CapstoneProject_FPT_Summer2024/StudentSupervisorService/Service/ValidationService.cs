﻿using System;
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
        //            - Update Status của Discipline thành DONE khi EndDate < DateTime.Now
        // 
        // Validate:  - Khi trường tạo SchoolYear mới, sau đó bắt buộc phải đăng ký 1 Package
        //            - Trước khi import Student, phải so total student vs TotalStudent trong
        //              all pakage của School trong năm đó(YearPackage)
        //            - Trước khi import Violation, phải so all Violation của 1 trường rồi so vs TotalViolation
        //              trong all pakage của School trong năm đó(YearPackage)
    }
}
