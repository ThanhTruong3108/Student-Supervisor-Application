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

        public async Task<List<Violation>> GetViolationsByClassId(int classId)
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
                .Where(v => v.ClassId == classId)
                .ToListAsync();
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

        public async Task<List<Violation>> GetViolationsBySchoolId(int schoolId, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var query = _context.Violations
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
                .Where(v => v.User.SchoolId == schoolId)
                .AsQueryable();

            if (year.HasValue)
            {
                query = query.Where(v => v.Class.SchoolYear.Year == year.Value);
            }

            if (!string.IsNullOrEmpty(semesterName))
            {
                var semester = _context.Semesters
                    .Where(s => s.SchoolYear.Year == year && s.Name.ToLower() == semesterName.ToLower())
                    .FirstOrDefault();

                if (semester != null)
                {
                    query = query.Where(v => v.Date >= semester.StartDate && v.Date <= semester.EndDate);
                }
            }

            if (month.HasValue)
            {
                var startDate = new DateTime(year ?? DateTime.Now.Year, month.Value, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                if (weekNumber.HasValue)
                {
                    if (!IsValidWeekNumberInMonth(startDate.Year, month.Value, weekNumber.Value))
                        throw new ArgumentException("Số tuần không hợp lệ!");

                    startDate = GetStartOfWeekInMonth(startDate.Year, month.Value, weekNumber.Value);
                    endDate = startDate.AddDays(7).AddSeconds(-1);
                }

                query = query.Where(v => v.Date >= startDate && v.Date <= endDate);
            }

            return await query.ToListAsync();
        }

        public async Task<List<ViolationTypeSummary>> GetTopFrequentViolations(int schoolId, short year, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var schoolYear = await _context.SchoolYears
                .Include(sy => sy.Semesters)
                .FirstOrDefaultAsync(s => s.Year == year && s.SchoolId == schoolId);

            if (schoolYear == null)
                throw new ArgumentException($"Niên khóa {year} không tồn tại cho trường {schoolId}.");

            DateTime startDate = schoolYear.StartDate;
            DateTime endDate = schoolYear.EndDate;

            if (!string.IsNullOrEmpty(semesterName))
            {
                var semester = schoolYear.Semesters.FirstOrDefault(s => s.Name.Equals(semesterName, StringComparison.OrdinalIgnoreCase));
                if (semester == null)
                    throw new ArgumentException($"Học kỳ '{semesterName}' không tồn tại trong niên khóa này.");

                startDate = semester.StartDate;
                endDate = semester.EndDate;
            }

            if (month.HasValue)
            {
                var monthStartDate = new DateTime(startDate.Year, month.Value, 1);
                var monthEndDate = monthStartDate.AddMonths(1).AddDays(-1);

                // Đảm bảo monthStartDate và monthEndDate nằm trong phạm vi học kỳ hoặc năm học
                if (monthStartDate < startDate)
                    monthStartDate = startDate;
                if (monthEndDate > endDate)
                    monthEndDate = endDate;

                if (monthStartDate > monthEndDate)
                    throw new ArgumentException("Tháng này không thuộc niên khóa hoặc học kỳ.");

                if (weekNumber.HasValue)
                {
                    if (!IsValidWeekNumberInMonth(startDate.Year, month.Value, weekNumber.Value))
                        throw new ArgumentException("Số tuần không hợp lệ!");

                    startDate = GetStartOfWeekInMonth(startDate.Year, month.Value, weekNumber.Value);
                    endDate = startDate.AddDays(7).AddSeconds(-1);

                    // Đảm bảo startDate và endDate nằm trong phạm vi tháng
                    if (startDate < monthStartDate)
                        startDate = monthStartDate;
                    if (endDate > monthEndDate)
                        endDate = monthEndDate;
                }
                else
                {
                    startDate = monthStartDate;
                    endDate = monthEndDate;
                }
            }

            return await _context.Violations
                .Where(v => v.Class.SchoolYear.SchoolId == schoolId && v.Date >= startDate && v.Date <= endDate && (v.Status == "APPROVED" || v.Status == "COMPLETED"))
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

        private bool IsValidWeekNumberInMonth(int year, int month, int weekNumber)
        {
            // Logic để kiểm tra số tuần hợp lệ trong tháng
            return weekNumber > 0 && weekNumber <= 5; // Giả định tháng có tối đa 5 tuần
        }

        private DateTime GetStartOfWeekInMonth(int year, int month, int weekNumber)
        {
            // Logic để lấy ngày bắt đầu của tuần trong tháng
            var firstDayOfMonth = new DateTime(year, month, 1);
            var startOfWeek = firstDayOfMonth.AddDays((weekNumber - 1) * 7);

            return startOfWeek;
        }

        // Lấy những lớp có học sinh vi phạm nhiều
        public async Task<List<ClassViolationSummary>> GetClassesWithMostViolations(int schoolId, short year, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var schoolYear = await _context.SchoolYears
                .Include(sy => sy.Semesters)
                .FirstOrDefaultAsync(s => s.Year == year && s.SchoolId == schoolId);

            if (schoolYear == null)
                throw new ArgumentException($"Niên khóa {year} không tồn tại cho trường {schoolId}.");

            DateTime startDate = schoolYear.StartDate;
            DateTime endDate = schoolYear.EndDate;

            if (!string.IsNullOrEmpty(semesterName))
            {
                var semester = schoolYear.Semesters.FirstOrDefault(s => s.Name.Equals(semesterName, StringComparison.OrdinalIgnoreCase));
                if (semester == null)
                    throw new ArgumentException($"Học kỳ '{semesterName}' không tồn tại trong niên khóa này.");

                startDate = semester.StartDate;
                endDate = semester.EndDate;
            }

            if (month.HasValue)
            {
                var monthStartDate = new DateTime(startDate.Year, month.Value, 1);
                var monthEndDate = monthStartDate.AddMonths(1).AddDays(-1);

                if (monthStartDate < startDate)
                    monthStartDate = startDate;
                if (monthEndDate > endDate)
                    monthEndDate = endDate;

                if (monthStartDate > monthEndDate)
                    throw new ArgumentException("Tháng này không thuộc niên khóa hoặc học kỳ.");

                if (weekNumber.HasValue)
                {
                    if (!IsValidWeekNumberInMonth(startDate.Year, month.Value, weekNumber.Value))
                        throw new ArgumentException("Số tuần không hợp lệ!");

                    startDate = GetStartOfWeekInMonth(startDate.Year, month.Value, weekNumber.Value);
                    endDate = startDate.AddDays(7).AddSeconds(-1);

                    if (startDate < monthStartDate)
                        startDate = monthStartDate;
                    if (endDate > monthEndDate)
                        endDate = monthEndDate;
                }
                else
                {
                    startDate = monthStartDate;
                    endDate = monthEndDate;
                }
            }

            return await _context.Violations
                .Include(c => c.Class)
                .Where(v => v.Class.SchoolYear.SchoolId == schoolId && v.Date >= startDate && v.Date <= endDate && (v.Status == "APPROVED" || v.Status == "COMPLETED"))
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

        //lấy top 5 học hinh vi phạm nhiều nhất
        public async Task<List<StudentViolationCount>> GetTop5StudentsWithMostViolations(int schoolId, short year, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var schoolYear = await _context.SchoolYears
                .Include(sy => sy.Semesters)
                .FirstOrDefaultAsync(s => s.Year == year && s.SchoolId == schoolId);

            if (schoolYear == null)
                return new List<StudentViolationCount>();

            DateTime startDate = schoolYear.StartDate;
            DateTime endDate = schoolYear.EndDate;

            if (!string.IsNullOrEmpty(semesterName))
            {
                var semester = schoolYear.Semesters.FirstOrDefault(s => s.Name.Equals(semesterName, StringComparison.OrdinalIgnoreCase));
                if (semester == null)
                    throw new ArgumentException($"Học kỳ '{semesterName}' không tồn tại trong niên khóa này.");

                startDate = semester.StartDate;
                endDate = semester.EndDate;
            }

            if (month.HasValue)
            {
                var monthStartDate = new DateTime(startDate.Year, month.Value, 1);
                var monthEndDate = monthStartDate.AddMonths(1).AddDays(-1);

                if (monthStartDate < startDate)
                    monthStartDate = startDate;
                if (monthEndDate > endDate)
                    monthEndDate = endDate;

                if (weekNumber.HasValue)
                {
                    if (!IsValidWeekNumberInMonth(startDate.Year, month.Value, weekNumber.Value))
                        throw new ArgumentException("Số tuần không hợp lệ!");

                    startDate = GetStartOfWeekInMonth(startDate.Year, month.Value, weekNumber.Value);
                    endDate = startDate.AddDays(7).AddSeconds(-1);

                    if (startDate < monthStartDate)
                        startDate = monthStartDate;
                    if (endDate > monthEndDate)
                        endDate = monthEndDate;
                }
                else
                {
                    startDate = monthStartDate;
                    endDate = monthEndDate;
                }
            }

            return await _context.Violations
                .Where(v => v.Class.SchoolYear.SchoolId == schoolId && v.Date >= startDate && v.Date <= endDate && (v.Status == "APPROVED" || v.Status == "COMPLETED"))
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
        public async Task<List<ClassViolationDetail>> GetClassWithMostStudentViolations(int schoolId, short year, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var schoolYear = await _context.SchoolYears
                .Include(sy => sy.Semesters)
                .FirstOrDefaultAsync(s => s.Year == year && s.SchoolId == schoolId);

            if (schoolYear == null)
                return new List<ClassViolationDetail>();

            DateTime startDate = schoolYear.StartDate;
            DateTime endDate = schoolYear.EndDate;

            if (!string.IsNullOrEmpty(semesterName))
            {
                var semester = schoolYear.Semesters.FirstOrDefault(s => s.Name.Equals(semesterName, StringComparison.OrdinalIgnoreCase));
                if (semester == null)
                    throw new ArgumentException($"Học kỳ '{semesterName}' không tồn tại trong niên khóa này.");

                startDate = semester.StartDate;
                endDate = semester.EndDate;
            }

            if (month.HasValue)
            {
                var monthStartDate = new DateTime(startDate.Year, month.Value, 1);
                var monthEndDate = monthStartDate.AddMonths(1).AddDays(-1);

                if (monthStartDate < startDate)
                    monthStartDate = startDate;
                if (monthEndDate > endDate)
                    monthEndDate = endDate;

                if (weekNumber.HasValue)
                {
                    if (!IsValidWeekNumberInMonth(startDate.Year, month.Value, weekNumber.Value))
                        throw new ArgumentException("Số tuần không hợp lệ!");

                    startDate = GetStartOfWeekInMonth(startDate.Year, month.Value, weekNumber.Value);
                    endDate = startDate.AddDays(7).AddSeconds(-1);

                    if (startDate < monthStartDate)
                        startDate = monthStartDate;
                    if (endDate > monthEndDate)
                        endDate = monthEndDate;
                }
                else
                {
                    startDate = monthStartDate;
                    endDate = monthEndDate;
                }
            }

            // Đảm bảo các ngày nằm trong khung thời gian của năm học
            startDate = startDate < schoolYear.StartDate ? schoolYear.StartDate : startDate;
            endDate = endDate > schoolYear.EndDate ? schoolYear.EndDate : endDate;

            var violations = await _context.Violations
                .Where(v => v.Class.SchoolYear.SchoolId == schoolId && v.Date >= startDate && v.Date <= endDate && (v.Status == "APPROVED" || v.Status == "COMPLETED"))
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
                .Take(5)
                .ToListAsync();

            return violations;
        }

        // lấy những vi phạm thuộc lớp mà Giáo viên đó chủ nhiệm
        public async Task<List<Violation>> GetViolationsByUserId(int userId, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var query = _context.Violations
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
                .AsQueryable();

            if (year.HasValue)
            {
                query = query.Where(v => v.Class.SchoolYear.Year == year.Value);
            }

            if (!string.IsNullOrEmpty(semesterName))
            {
                var semester = _context.Semesters
                    .Where(s => s.SchoolYear.Year == year && s.Name.ToLower() == semesterName.ToLower())
                    .FirstOrDefault();

                if (semester != null)
                {
                    query = query.Where(v => v.Date >= semester.StartDate && v.Date <= semester.EndDate);
                }
            }

            if (month.HasValue)
            {
                var startDate = new DateTime(year ?? DateTime.Now.Year, month.Value, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                if (weekNumber.HasValue)
                {
                    if (!IsValidWeekNumberInMonth(startDate.Year, month.Value, weekNumber.Value))
                        throw new ArgumentException("Số tuần không hợp lệ!");

                    startDate = GetStartOfWeekInMonth(startDate.Year, month.Value, weekNumber.Value);
                    endDate = startDate.AddDays(7).AddSeconds(-1);
                }

                query = query.Where(v => v.Date >= startDate && v.Date <= endDate);
            }

            return await query.ToListAsync();
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
                .Where(v => v.UserId == userId && v.Status == ViolationStatusEnums.PENDING.ToString())
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
        public async Task<List<Violation>> GetViolationsBySupervisorUserId(int userId, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var query = _context.Violations
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
                .AsQueryable();

            if (year.HasValue)
            {
                query = query.Where(v => v.Class.SchoolYear.Year == year.Value);
            }

            if (!string.IsNullOrEmpty(semesterName))
            {
                var semester = _context.Semesters
                    .Where(s => s.SchoolYear.Year == year && s.Name.ToLower() == semesterName.ToLower())
                    .FirstOrDefault();

                if (semester != null)
                {
                    query = query.Where(v => v.Date >= semester.StartDate && v.Date <= semester.EndDate);
                }
            }

            if (month.HasValue)
            {
                var startDate = new DateTime(year ?? DateTime.Now.Year, month.Value, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                if (weekNumber.HasValue)
                {
                    if (!IsValidWeekNumberInMonth(startDate.Year, month.Value, weekNumber.Value))
                        throw new ArgumentException("Số tuần không hợp lệ!");

                    startDate = GetStartOfWeekInMonth(startDate.Year, month.Value, weekNumber.Value);
                    endDate = startDate.AddDays(7).AddSeconds(-1);
                }

                query = query.Where(v => v.Date >= startDate && v.Date <= endDate);
            }

            return await query.ToListAsync();
        }

        public async Task<List<KeyValuePair<string, int>>> CountViolationsByDate(int schoolId, short year, int? month = null, int? weekNumber = null)
        {
            // Tìm kiếm năm học tương ứng với year và schoolId
            var schoolYear = await _context.SchoolYears.FirstOrDefaultAsync(s => s.Year == year && s.SchoolId == schoolId);

            if (schoolYear == null)
                return new List<KeyValuePair<string, int>>();

            DateTime startDate, endDate;

            // Nếu người dùng cung cấp tháng
            if (month.HasValue)
            {
                startDate = new DateTime(year, month.Value, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);

                // Đảm bảo thời gian nằm trong niên khóa
                if (startDate < schoolYear.StartDate || endDate > schoolYear.EndDate)
                {
                    throw new ArgumentException($"Tháng {month} không thuộc năm học {year}");
                }

                if (weekNumber.HasValue)
                {
                    if (!IsValidWeekNumberInMonth(year, month.Value, weekNumber.Value))
                        throw new ArgumentException("Số tuần không hợp lệ!!!");

                    startDate = GetStartOfWeekInMonth(year, month.Value, weekNumber.Value);
                    endDate = startDate.AddDays(6);
                }
            }
            else
            {
                startDate = schoolYear.StartDate;
                endDate = schoolYear.EndDate;
            }

            // Điều chỉnh startDate và endDate để đảm bảo nằm trong phạm vi của SchoolYear
            startDate = startDate < schoolYear.StartDate ? schoolYear.StartDate : startDate;
            endDate = endDate > schoolYear.EndDate ? schoolYear.EndDate : endDate;

            var violationsGroupedByDate = await _context.Violations
                .Where(v => v.Date >= startDate && v.Date <= endDate && (v.Status == "APPROVED" || v.Status == "COMPLETED"))
                .GroupBy(v => year) // Tất cả các vi phạm đều được nhóm vào năm học 2023
                .Select(g => new KeyValuePair<string, int>(year.ToString(), g.Count()))
                .ToListAsync();

            return violationsGroupedByDate;
        }

        public async Task<List<KeyValuePair<string, int>>> GetMonthlyViolationCounts(int schoolId, short year)
        {
            var schoolYear = await _context.SchoolYears
                .FirstOrDefaultAsync(s => s.Year == year && s.SchoolId == schoolId);

            if (schoolYear == null)
                return new List<KeyValuePair<string, int>>();

            var violationsGroupedByMonth = await _context.Violations
                .Where(v => v.Date >= schoolYear.StartDate && v.Date <= schoolYear.EndDate && (v.Status == "APPROVED" || v.Status == "COMPLETED"))
                .GroupBy(v => new { v.Date.Year, v.Date.Month })
                .Select(g => new
                {
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    Count = g.Count()
                })
                .ToListAsync();

            var result = new List<KeyValuePair<string, int>>();

            var currentDate = schoolYear.StartDate;
            while (currentDate <= schoolYear.EndDate)
            {
                var monthName = GetVietnameseMonthName(currentDate.Month);
                var monthYear = new DateTime(currentDate.Year, currentDate.Month, 1);

                var monthData = violationsGroupedByMonth
                    .FirstOrDefault(v => v.Year == monthYear.Year && v.Month == monthYear.Month);

                result.Add(new KeyValuePair<string, int>(monthName, monthData?.Count ?? 0));

                currentDate = currentDate.AddMonths(1);
            }

            return result;
        }

        private string GetVietnameseMonthName(int month)
        {
            return month switch
            {
                1 => "Tháng 1",
                2 => "Tháng 2",
                3 => "Tháng 3",
                4 => "Tháng 4",
                5 => "Tháng 5",
                6 => "Tháng 6",
                7 => "Tháng 7",
                8 => "Tháng 8",
                9 => "Tháng 9",
                10 => "Tháng 10",
                11 => "Tháng 11",
                12 => "Tháng 12",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
