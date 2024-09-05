using Domain.Entity;
using Domain.Entity.DTO;
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
    public class EvaluationRepository : GenericRepository<Evaluation>, IEvaluationRepository
    {
        public EvaluationRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<Evaluation>> GetAllEvaluations()
        {
            return await _context.Evaluations
                .Include(v => v.Class)
                    .ThenInclude(s => s.SchoolYear)
                .ToListAsync();
        }

        public async Task<Evaluation> GetEvaluationById(int id)
        {
            return await _context.Evaluations
                .Include(v => v.Class)
                    .ThenInclude(s => s.SchoolYear)
                .FirstOrDefaultAsync(x => x.EvaluationId == id);
        }

        public async Task<Evaluation> CreateEvaluation(Evaluation evaluationEntity)
        {
            await _context.Evaluations.AddAsync(evaluationEntity);
            await _context.SaveChangesAsync();
            return evaluationEntity;
        }

        public async Task<Evaluation> UpdateEvaluation(Evaluation evaluationEntity)
        {
            _context.Evaluations.Update(evaluationEntity);
            await _context.SaveChangesAsync();
            return evaluationEntity;
        }

        public async Task<List<Evaluation>> GetEvaluationsByClassIdAndDateRange(int classId, DateTime from, DateTime to)
        {
            return await _context.Evaluations
                .Include(v => v.Class)
                    .ThenInclude(s => s.SchoolYear)
                .Where(e => e.ClassId == classId && e.From <= to && e.To >= from)
                .ToListAsync();
        }

        public async Task<List<Evaluation>> GetEvaluationsBySchoolId(int schoolId, short? year = null, string? semesterName = null, int? month = null, int? weekNumber = null)
        {
            var query = _context.Evaluations
                .Include(v => v.Class)
                    .ThenInclude(s => s.SchoolYear)
                    .ThenInclude(s => s.School)
                .Where(v => v.Class.SchoolYear.SchoolId == schoolId)
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
                    query = query.Where(v => v.From >= semester.StartDate && v.To <= semester.EndDate);
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

                query = query.Where(v => v.From >= startDate && v.To <= endDate);
            }

            return await query.ToListAsync();
        }

        private bool IsValidWeekNumberInMonth(int year, int month, int weekNumber)
        {
            // Logic để kiểm tra số tuần hợp lệ trong tháng
            return weekNumber > 0 && weekNumber <= 4; // Giả định tháng có tối đa 5 tuần
        }

        private DateTime GetStartOfWeekInMonth(int year, int month, int weekNumber)
        {
            // Logic để lấy ngày bắt đầu của tuần trong tháng
            var firstDayOfMonth = new DateTime(year, month, 1);
            var startOfWeek = firstDayOfMonth.AddDays((weekNumber - 1) * 7);

            return startOfWeek;
        }

        public async Task<List<EvaluationRanking>> GetEvaluationRankings(int schoolId, short year, string? semesterName = null, int? month = null, int? week = null)
        {
            var schoolYear = await _context.SchoolYears
                .Include(sy => sy.Semesters)
                .FirstOrDefaultAsync(sy => sy.SchoolId == schoolId && sy.Year == year);

            if (schoolYear == null)
                throw new Exception("SchoolYear không tồn tại.");

            IQueryable<Evaluation> evaluationsQuery = _context.Evaluations
                .Where(e => e.Class.SchoolYear.SchoolId == schoolId && e.Class.SchoolYear.Year == year);

            if (!string.IsNullOrEmpty(semesterName))
            {
                var semester = schoolYear.Semesters
                    .FirstOrDefault(s => s.Name.Equals(semesterName, StringComparison.OrdinalIgnoreCase));

                if (semester == null)
                    throw new Exception($"Học kỳ '{semesterName}' không tồn tại trong niên khóa {year}.");

                evaluationsQuery = evaluationsQuery.Where(e => e.From >= semester.StartDate && e.From <= semester.EndDate);
            }

            if (month.HasValue)
            {
                evaluationsQuery = evaluationsQuery.Where(e => e.From.Month == month.Value);

                if (week.HasValue)
                {
                    var startDate = new DateTime(year, month.Value, 1);
                    var firstDayOfWeek = startDate.AddDays((week.Value - 1) * 7);
                    var endDate = firstDayOfWeek.AddDays(6);

                    if (endDate > schoolYear.EndDate)
                        endDate = schoolYear.EndDate;

                    evaluationsQuery = evaluationsQuery.Where(e => e.From >= firstDayOfWeek && e.From <= endDate);
                }
            }

            var rankings = await evaluationsQuery
                .GroupBy(e => e.ClassId)
                .Select(g => new EvaluationRanking
                {
                    ClassId = g.Key,
                    ClassName = g.FirstOrDefault().Class.Name,
                    TotalPoints = g.Sum(e => e.Points ?? 0)
                })
                .OrderByDescending(r => r.TotalPoints)
                .ToListAsync();

            return rankings;
        }
    }
}
