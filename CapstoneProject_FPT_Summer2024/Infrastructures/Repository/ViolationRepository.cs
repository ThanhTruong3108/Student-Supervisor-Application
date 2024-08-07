using Domain.Entity;
using Domain.Entity.DTO;
using Domain.Enums.Role;
using Domain.Enums.Status;
using Infrastructures.Interfaces;
using Infrastructures.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repository
{
    public class ViolationRepository : GenericRepository<Violation>, IViolationRepository
    {
        public ViolationRepository(SchoolRulesContext context) : base(context) { }

        public async Task<List<Violation>> GetAllViolations()
        {
            var violations = await _context.Violations
                .Include(s => s.Schedule)
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                    .ThenInclude(y => y.SchoolYear)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.User)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .ToListAsync();

            return violations;
        }

        public async Task<Violation> GetByIdWithImages(int id)
        {
            return await _context.Violations
                .Include(i => i.ImageUrls)
                .FirstOrDefaultAsync(v => v.ViolationId == id);
        }

        public async Task<Violation> GetViolationById(int id)
        {
            return _context.Violations
                .Include(s => s.Schedule)
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                    .ThenInclude(y => y.SchoolYear)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.User)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
               .FirstOrDefault(s => s.ViolationId == id);
        }

        public async Task<Violation> CreateViolation(Violation violationEntity)
        {
            violationEntity.CreatedAt = DateTime.Now;
            violationEntity.UpdatedAt = DateTime.Now;
            await _context.Violations.AddAsync(violationEntity);
            await _context.SaveChangesAsync();
            return violationEntity;
        }

        public async Task<Violation> UpdateViolation(Violation violationEntity)
        {
            violationEntity.UpdatedAt = DateTime.Now;
            _context.Violations.Update(violationEntity);
            _context.Entry(violationEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return violationEntity;
        }

        // lấy các Violation có status = APPROVED và UpdatedAt > 1 ngày
        public async Task<List<Violation>> GetApprovedViolationsOver1Day()
        {
            DateTime oneDayAgo = DateTime.Now.AddDays(-1);
            return await _context.Violations
                .Where(v => v.Status == ViolationStatusEnums.APPROVED.ToString()
                       && v.UpdatedAt < oneDayAgo)
                .ToListAsync();
        }

        // tự động ACCEPT các violation sau 1 ngày nếu GVCN ko duyệt
        public async Task AcceptMultipleViolations(List<Violation> violations)
        {
            //try
            //{
            //    foreach (var violation in violations)
            //    {
            //        violation.Status = ViolationStatusEnums.ACCEPTED.ToString();
            //        violation.UpdatedAt = DateTime.Now;
            //        _context.Entry(violation).State = EntityState.Modified;
            //    }
            //    await _context.SaveChangesAsync();
            //}
            //catch (Exception e)
            //{
            //    throw new Exception("Error in AcceptMultipleViolations: " + e.Message);
            //}
        }

        public async Task DeleteViolation(int id)
        {
            var violationEntity = await _context.Violations.FindAsync(id);
            violationEntity.Status = ViolationStatusEnums.INACTIVE.ToString();
            violationEntity.UpdatedAt = DateTime.Now;
            _context.Entry(violationEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Violation>> GetViolationsByStudentId(int studentId)
        {
            return await _context.Violations
                .Include(s => s.Schedule)
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                    .ThenInclude(y => y.SchoolYear)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.User)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(v => v.StudentInClass.StudentId == studentId)
                .ToListAsync();

        }

        public async Task<List<Violation>> GetViolationsByStudentIdAndYear(int studentId, int schoolYearId)
        {
            return await _context.Violations
                .Include(s => s.Schedule)
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                    .ThenInclude(y => y.SchoolYear)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.User)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(v => v.StudentInClass.StudentId == studentId && v.Class.SchoolYearId == schoolYearId)
                .ToListAsync();
        }

        public async Task<Dictionary<int, int>> GetViolationCountByYear(int studentId)
        {
            return await _context.Violations
                .Include(s => s.Schedule)
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                    .ThenInclude(y => y.SchoolYear)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.User)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(v => v.StudentInClass.StudentId == studentId)
                .GroupBy(v => v.Class.SchoolYearId)
                .Select(g => new { SchoolYearId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.SchoolYearId, g => g.Count);
        }

        public async Task<List<Violation>> GetViolationsBySchoolId(int schoolId)
        {
            return await _context.Violations
                .Include(s => s.Schedule)
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                    .ThenInclude(y => y.SchoolYear)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.User)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(ed => ed.User.SchoolId == schoolId)
                .ToListAsync();
        }


        public async Task<List<Violation>> GetViolationsByMonthAndWeek(int schoolId, short year, int month, int? weekNumber = null)
        {
            var schoolYear = await _context.SchoolYears.FirstOrDefaultAsync(s => s.Year == year && s.SchoolId == schoolId);

            if (schoolYear == null)
                return new List<Violation>();

            var monthStartDate = new DateTime(year, month, 1);
            var monthEndDate = monthStartDate.AddMonths(1).AddDays(-1);

            if (monthStartDate < schoolYear.StartDate || monthEndDate > schoolYear.EndDate)
            {
                throw new ArgumentException("Tháng này không thuộc năm học " + year);
            }

            DateTime startDate;
            DateTime endDate;

            if (weekNumber.HasValue)
            {
                if (!IsValidWeekNumberInMonth(year, month, weekNumber.Value))
                    throw new ArgumentException("Số tuần không hợp lệ!!!");

                startDate = GetStartOfWeekInMonth(schoolYear.StartDate.Year, month, weekNumber.Value);
                endDate = startDate.AddDays(7);
            }
            else
            {
                startDate = monthStartDate;
                endDate = monthStartDate.AddMonths(1);
            }

            // Ensure the dates are within the school year's timeframe
            startDate = startDate < schoolYear.StartDate ? schoolYear.StartDate : startDate;
            endDate = endDate > schoolYear.EndDate ? schoolYear.EndDate : endDate;

            return await _context.Violations
                .Include(s => s.Schedule)
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                    .ThenInclude(y => y.SchoolYear)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.User)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(v => v.Date >= startDate && v.Date < endDate && v.Status == "APPROVED")
                .ToListAsync();
        }

        private DateTime GetStartOfWeekInMonth(int year, int month, int weekNumber)
        {
            var startOfMonth = new DateTime(year, month, 1);
            return startOfMonth.AddDays((weekNumber - 1) * 7);
        }

        private bool IsValidWeekNumberInMonth(int year, int month, int weekNumber)
        {
            var startOfMonth = new DateTime(year, month, 1);
            var daysInMonth = DateTime.DaysInMonth(year, month);
            var maxWeeksInMonth = (int)Math.Ceiling(daysInMonth / 7.0);
            return weekNumber >= 1 && weekNumber <= maxWeeksInMonth;
        }

        public async Task<List<Violation>> GetViolationsByYearAndClassName(int schoolId, short year, string className, int? month = null, int? weekNumber = null)
        {
            var schoolYear = await _context.SchoolYears.FirstOrDefaultAsync(s => s.Year == year && s.SchoolId == schoolId);

            if (schoolYear == null)
                return new List<Violation>();

            DateTime startDate = schoolYear.StartDate;
            DateTime endDate = schoolYear.EndDate;

            if (month.HasValue)
            {
                var monthStartDate = new DateTime(year, month.Value, 1);
                var monthEndDate = monthStartDate.AddMonths(1).AddDays(-1);

                if (monthStartDate < schoolYear.StartDate || monthEndDate > schoolYear.EndDate)
                {
                    throw new ArgumentException("Tháng này không thuộc năm học " + year);
                }

                if (weekNumber.HasValue)
                {
                    if (!IsValidWeekNumberInMonth(year, month.Value, weekNumber.Value))
                        throw new ArgumentException("Số tuần không hợp lệ!!!");

                    startDate = GetStartOfWeekInMonth(year, month.Value, weekNumber.Value);
                    endDate = startDate.AddDays(7).AddSeconds(-1);
                }
                else
                {
                    startDate = monthStartDate;
                    endDate = monthEndDate;
                }

                startDate = startDate < schoolYear.StartDate ? schoolYear.StartDate : startDate;
                endDate = endDate > schoolYear.EndDate ? schoolYear.EndDate : endDate;
            }

            return await _context.Violations
                .Include(s => s.Schedule)
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                    .ThenInclude(y => y.SchoolYear)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.User)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(v => v.Class.SchoolYearId == schoolYear.SchoolYearId
                    && v.Class.Name == className
                    && v.Date >= startDate
                    && v.Date <= endDate
                    && v.Status == "APPROVED")
                .ToListAsync();
        }

        public async Task<List<ViolationTypeSummary>> GetTopFrequentViolations(int schoolId, short year, int? month = null, int? weekNumber = null)
        {
            var schoolYear = await _context.SchoolYears.FirstOrDefaultAsync(s => s.Year == year && s.SchoolId == schoolId);

            if (schoolYear == null)
                return new List<ViolationTypeSummary>();

            DateTime startDate = schoolYear.StartDate;
            DateTime endDate = schoolYear.EndDate;

            if (month.HasValue)
            {
                var monthStartDate = new DateTime(year, month.Value, 1);
                var monthEndDate = monthStartDate.AddMonths(1).AddDays(-1);

                if (monthStartDate < schoolYear.StartDate || monthEndDate > schoolYear.EndDate)
                {
                    throw new ArgumentException("Tháng này không thuộc năm học " + year);
                }

                if (weekNumber.HasValue)
                {
                    if (!IsValidWeekNumberInMonth(year, month.Value, weekNumber.Value))
                        throw new ArgumentException("Số tuần không hợp lệ!!!");

                    startDate = GetStartOfWeekInMonth(year, month.Value, weekNumber.Value);
                    endDate = startDate.AddDays(7).AddSeconds(-1);
                }
                else
                {
                    startDate = monthStartDate;
                    endDate = monthEndDate;
                }

                startDate = startDate < schoolYear.StartDate ? schoolYear.StartDate : startDate;
                endDate = endDate > schoolYear.EndDate ? schoolYear.EndDate : endDate;
            }

            return await _context.Violations
                .Where(v => v.Date >= startDate && v.Date <= endDate && v.Status == "APPROVED")
                .GroupBy(v => v.ViolationTypeId)
                .Select(g => new ViolationTypeSummary
                {
                    ViolationTypeId = g.Key,
                    ViolationTypeName = g.First().ViolationType.Name,
                    ViolationCount = g.Count()
                })
                .OrderByDescending(vts => vts.ViolationCount)
                .Take(5)
                .ToListAsync();
        }

        public async Task<List<ClassViolationSummary>> GetClassesWithMostViolations(int schoolId, short year, int month, int? weekNumber = null)
        {
            var schoolYear = await _context.SchoolYears.FirstOrDefaultAsync(s => s.Year == year && s.SchoolId == schoolId);

            if (schoolYear == null)
                return new List<ClassViolationSummary>();

            var monthStartDate = new DateTime(year, month, 1);
            var monthEndDate = monthStartDate.AddMonths(1).AddDays(-1);

            if (monthStartDate < schoolYear.StartDate || monthEndDate > schoolYear.EndDate)
            {
                throw new ArgumentException("Tháng này không thuộc năm học " + year);
            }

            DateTime startDate;
            DateTime endDate;

            if (weekNumber.HasValue)
            {
                if (!IsValidWeekNumberInMonth(year, month, weekNumber.Value))
                    throw new ArgumentException("Số tuần không hợp lệ!");

                startDate = GetStartOfWeekInMonth(year, month, weekNumber.Value);
                endDate = startDate.AddDays(7).AddSeconds(-1);
            }
            else
            {
                startDate = monthStartDate;
                endDate = monthEndDate;
            }

            // Ensure the dates are within the school year's timeframe
            startDate = startDate < schoolYear.StartDate ? schoolYear.StartDate : startDate;
            endDate = endDate > schoolYear.EndDate ? schoolYear.EndDate : endDate;

            return await _context.Violations
                .Include(c => c.Class)
                .Where(v => v.Date >= startDate && v.Date <= endDate && v.Status == "APPROVED")
                .GroupBy(v => v.ClassId)
                .Select(g => new ClassViolationSummary
                {
                    ClassId = g.Key,
                    ClassName = g.First().Class.Name,
                    ViolationCount = g.Count()
                })
                .OrderByDescending(cvs => cvs.ViolationCount)
                .Take(3)
                .ToListAsync();
        }

        //lấy top5 học hinh vi phạm nhiều nhất
        public async Task<List<StudentViolationCount>> GetTop5StudentsWithMostViolations(int schoolId, short year, int? month = null, int? weekNumber = null)
        {
            var schoolYear = await _context.SchoolYears.FirstOrDefaultAsync(s => s.Year == year && s.SchoolId == schoolId);

            if (schoolYear == null)
                return new List<StudentViolationCount>();

            DateTime startDate = schoolYear.StartDate;
            DateTime endDate = schoolYear.EndDate;

            if (month.HasValue)
            {
                var monthStartDate = new DateTime(year, month.Value, 1);
                var monthEndDate = monthStartDate.AddMonths(1).AddDays(-1);

                if (monthStartDate < schoolYear.StartDate || monthEndDate > schoolYear.EndDate)
                {
                    throw new ArgumentException("Tháng này không thuộc năm học " + year);
                }

                if (weekNumber.HasValue)
                {
                    if (!IsValidWeekNumberInMonth(year, month.Value, weekNumber.Value))
                        throw new ArgumentException("Số tuần không hợp lệ!!!");

                    startDate = GetStartOfWeekInMonth(year, month.Value, weekNumber.Value);
                    endDate = startDate.AddDays(7).AddSeconds(-1);
                }
                else
                {
                    startDate = monthStartDate;
                    endDate = monthEndDate;
                }

                startDate = startDate < schoolYear.StartDate ? schoolYear.StartDate : startDate;
                endDate = endDate > schoolYear.EndDate ? schoolYear.EndDate : endDate;
            }

            return await _context.Violations
                .Where(v => v.Date >= startDate && v.Date <= endDate && v.Status == "APPROVED")
                .GroupBy(v => v.StudentInClass.Student)
                .Select(g => new StudentViolationCount
                {
                    StudentId = g.Key.StudentId,
                    FullName = g.Key.Name,
                    ClassName = g.First().Class.Name,
                    ViolationCount = g.Count()
                })
                .OrderByDescending(svc => svc.ViolationCount)
                .Take(5)
                .ToListAsync();
        }

        // lấy Lớp có nhiều học sinh vi phạm nhất
        public async Task<List<ClassViolationDetail>> GetClassWithMostStudentViolations(int schoolId, short year, int month, int? weekNumber = null)
        {
            var schoolYear = await _context.SchoolYears.FirstOrDefaultAsync(s => s.Year == year && s.SchoolId == schoolId);

            if (schoolYear == null)
                return new List<ClassViolationDetail>();

            var monthStartDate = new DateTime(year, month, 1);
            var monthEndDate = monthStartDate.AddMonths(1).AddDays(-1);

            if (monthStartDate < schoolYear.StartDate || monthEndDate > schoolYear.EndDate)
            {
                throw new ArgumentException("Tháng này không thuộc năm học " + year);
            }

            DateTime startDate;
            DateTime endDate;

            if (weekNumber.HasValue)
            {
                if (!IsValidWeekNumberInMonth(year, month, weekNumber.Value))
                    throw new ArgumentException("Số tuần không hợp lệ!");

                startDate = GetStartOfWeekInMonth(year, month, weekNumber.Value);
                endDate = startDate.AddDays(7).AddSeconds(-1);
            }
            else
            {
                startDate = monthStartDate;
                endDate = monthEndDate;
            }

            // Ensure the dates are within the school year's timeframe
            startDate = startDate < schoolYear.StartDate ? schoolYear.StartDate : startDate;
            endDate = endDate > schoolYear.EndDate ? schoolYear.EndDate : endDate;

            var violations = await _context.Violations
                .Where(v => v.Date >= startDate && v.Date <= endDate && v.Status == "APPROVED")
                .GroupBy(v => v.Class)
                .Select(g => new ClassViolationDetail
                {
                    ClassId = g.Key.ClassId,
                    ClassName = g.Key.Name,
                    StudentCount = g.Select(v => v.StudentInClass.StudentId).Distinct().Count(),
                    Students = g.Select(v => v.StudentInClass.Student)
                                .Distinct()
                                .Select(s => new StudentDetail
                                {
                                    StudentId = s.StudentId,
                                    StudentCode = s.Code,
                                    FullName = s.Name
                                }).ToList()
                })
                .OrderByDescending(cvc => cvc.StudentCount)
                .ToListAsync();

            return violations;
        }

        // lấy những vi phạm thuộc lớp mà Giáo viên đó chủ nhiệm
        public async Task<List<Violation>> GetViolationsByUserId(int userId)
        {
            return await _context.Violations
                .Include(s => s.Schedule)
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                    .ThenInclude(y => y.SchoolYear)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.User)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(v => v.Class.Teacher.UserId == userId)
                .ToListAsync();
        }

        public async Task<Violation> GetViolationByDisciplineId(int disciplineId)
        {
            return await _context.Violations
                .Include(s => s.Schedule)
                .Include(v => v.Disciplines)
                    .ThenInclude(d => d.Pennalty)
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                    .ThenInclude(y => y.SchoolYear)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.User)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Include(s => s.Schedule)
                .FirstOrDefaultAsync(v => v.Disciplines.Any(d => d.DisciplineId == disciplineId));
        }

        // lấy những vi phạm mà thằng SaoDo đó đã tạo
        public async Task<List<Violation>> GetViolationsByUserRoleStudentSupervisor(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null || user.Role.RoleName != RoleAccountEnum.STUDENT_SUPERVISOR.ToString())
            {
                return new List<Violation>();
            }

            return await _context.Violations
                .Include(s => s.Schedule)
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                    .ThenInclude(y => y.SchoolYear)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.User)
                    .ThenInclude(vr => vr.Role)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Include(s => s.Schedule)
                .Where(v => v.UserId == userId)
                .ToListAsync();
        }

        // lấy những vi phạm mà thằng giám thị đó đã tạo
        public async Task<List<Violation>> GetViolationsByUserRoleSupervisor(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null || user.Role.RoleName != RoleAccountEnum.SUPERVISOR.ToString())
            {
                return new List<Violation>();
            }

            return await _context.Violations
                .Include(s => s.Schedule)
                .Include(i => i.ImageUrls)
                .Include(c => c.Class)
                    .ThenInclude(y => y.SchoolYear)
                .Include(c => c.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(c => c.User)
                    .ThenInclude(vr => vr.Role)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Include(s => s.Schedule)
                .Where(v => v.UserId == userId)
                .ToListAsync();
        }

        // lấy những vi phạm thuộc những lớp mà giám thị đó quản lý
        public async Task<List<Violation>> GetViolationsBySupervisorUserId(int userId)
        {
            return await _context.Violations
                .Include(s => s.Schedule)
                .Include(v => v.Class)
                    .ThenInclude(c => c.ClassGroup)
                    .ThenInclude(g => g.Teacher)
                .Include(c => c.Class)
                    .ThenInclude(y => y.SchoolYear)
                .Include(v => v.ViolationType)
                    .ThenInclude(vr => vr.ViolationGroup)
                .Include(v => v.User)
                    .ThenInclude(vr => vr.School)
                .Include(v => v.StudentInClass)
                    .ThenInclude(vr => vr.Student)
                .Where(v => v.Class.ClassGroup.Teacher.UserId == userId)
                .ToListAsync();
        }
    }
}
