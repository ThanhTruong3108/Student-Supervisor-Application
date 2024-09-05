using Domain.Entity;
using Domain.Enums.Status;
using Infrastructures.Interfaces;
using Infrastructures.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repository
{
    public class DisciplineRepository : GenericRepository<Discipline>, IDisciplineRepository
    {
        public DisciplineRepository(SchoolRulesContext context) : base(context) { }

        public async Task<List<Discipline>> GetAllDisciplines()
        {
            return await _context.Disciplines
                .Include(d => d.Violation)
                    .ThenInclude(c => c.Class)
                        .ThenInclude(y => y.SchoolYear)
                .Include(v => v.Violation)
                    .ThenInclude(c => c.StudentInClass)
                    .ThenInclude(c => c.Student)
                .Include(v => v.Pennalty)
                .ToListAsync();
        }

        public async Task<Discipline> GetDisciplineById(int id)
        {
            return await _context.Disciplines
                .Include(d => d.Violation)
                    .ThenInclude(c => c.Class)
                        .ThenInclude(y => y.SchoolYear)
                .Include(v => v.Violation)
                    .ThenInclude(c => c.StudentInClass)
                    .ThenInclude(c => c.Student)
                .Include(v => v.Pennalty)
                .FirstOrDefaultAsync(x => x.DisciplineId == id);
        }

        public async Task<Discipline> CreateDiscipline(Discipline disciplineEntity)
        {
            await _context.Disciplines.AddAsync(disciplineEntity);
            await _context.SaveChangesAsync();
            return disciplineEntity;
        }

        public async Task<Discipline> UpdateDiscipline(Discipline disciplineEntity)
        {
            _context.Disciplines.Update(disciplineEntity);
            await _context.SaveChangesAsync();
            return disciplineEntity;
        }

        public async Task DeleteDiscipline(int id)
        {
            var disciplineEntity = await _context.Disciplines.FindAsync(id);
            disciplineEntity.Status = DisciplineStatusEnums.INACTIVE.ToString();
            _context.Entry(disciplineEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Discipline>> GetDisciplinesBySchoolId(int schoolId, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var query = _context.Disciplines
                .Include(d => d.Violation)
                    .ThenInclude(v => v.Class)
                        .ThenInclude(y => y.SchoolYear)
                .Include(d => d.Violation)
                    .ThenInclude(v => v.StudentInClass)
                        .ThenInclude(s => s.Student)
                .Include(d => d.Pennalty)
                .Where(d => d.Pennalty.SchoolId == schoolId)
                .AsQueryable();

            if (year.HasValue)
            {
                query = query.Where(d => d.Violation.Class.SchoolYear.Year == year.Value);
            }

            if (!string.IsNullOrEmpty(semesterName))
            {
                var semester = _context.Semesters
                    .Where(s => s.SchoolYear.Year == year && s.Name.ToLower() == semesterName.ToLower())
                    .FirstOrDefault();

                if (semester != null)
                {
                    query = query.Where(d => d.Violation.Date >= semester.StartDate && d.Violation.Date <= semester.EndDate);
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

                query = query.Where(d => d.Violation.Date >= startDate && d.Violation.Date <= endDate);
            }

            return await query.ToListAsync();
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

        public async Task<Discipline> GetDisciplineByViolationId(int violationId)
        {
            return await _context.Disciplines
                .Include(d => d.Violation)
                    .ThenInclude(c => c.Class)
                        .ThenInclude(y => y.SchoolYear)
                .Include(v => v.Violation)
                    .ThenInclude(c => c.StudentInClass)
                    .ThenInclude(c => c.Student)
                .Include(v => v.Pennalty)
                .FirstOrDefaultAsync(x => x.ViolationId == violationId);
        }

        public async Task<List<Discipline>> GetDisciplinesByUserId(int userId, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var query = _context.Disciplines
                .Include(d => d.Violation)
                    .ThenInclude(v => v.Class)
                        .ThenInclude(y => y.SchoolYear)
                .Include(d => d.Violation)
                    .ThenInclude(v => v.StudentInClass)
                        .ThenInclude(sic => sic.Student)
                .Include(d => d.Pennalty)
                .Where(d => d.Violation.Class.Teacher.UserId == userId)
                .AsQueryable();

            // Filter theo niên khóa
            if (year.HasValue)
            {
                query = query.Where(d => d.Violation.Class.SchoolYear.Year == year.Value);
            }

            // Filter theo kỳ
            if (!string.IsNullOrEmpty(semesterName))
            {
                var semester = _context.Semesters
                    .Where(s => s.SchoolYear.Year == year && s.Name.ToLower() == semesterName.ToLower())
                    .FirstOrDefault();

                if (semester != null)
                {
                    query = query.Where(d => d.Violation.Date >= semester.StartDate && d.Violation.Date <= semester.EndDate);
                }
            }

            // Filter theo tháng và tuần
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

                query = query.Where(d => d.Violation.Date >= startDate && d.Violation.Date <= endDate);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Discipline>> GetDisciplinesBySupervisorUserId(int userId, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var query = _context.Disciplines
                .Include(d => d.Violation)
                    .ThenInclude(v => v.Class)
                        .ThenInclude(y => y.SchoolYear)
                .Include(d => d.Violation)
                    .ThenInclude(v => v.StudentInClass)
                        .ThenInclude(sic => sic.Student)
                .Include(d => d.Pennalty)
                .Where(v => v.Violation.Class.ClassGroup.Teacher.UserId == userId)
                .AsQueryable();

            // Filter theo niên khóa
            if (year.HasValue)
            {
                query = query.Where(d => d.Violation.Class.SchoolYear.Year == year.Value);
            }

            // Filter theo kỳ
            if (!string.IsNullOrEmpty(semesterName))
            {
                var semester = _context.Semesters
                    .Where(s => s.SchoolYear.Year == year && s.Name.ToLower() == semesterName.ToLower())
                    .FirstOrDefault();

                if (semester != null)
                {
                    query = query.Where(d => d.Violation.Date >= semester.StartDate && d.Violation.Date <= semester.EndDate);
                }
            }

            // Filter theo tháng và tuần
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

                query = query.Where(d => d.Violation.Date >= startDate && d.Violation.Date <= endDate);
            }

            return await query.ToListAsync();
        }
    }
}
