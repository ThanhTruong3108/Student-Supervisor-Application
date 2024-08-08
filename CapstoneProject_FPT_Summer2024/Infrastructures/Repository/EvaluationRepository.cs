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

        public async Task<List<Evaluation>> GetEvaluationsBySchoolId(int schoolId)
        {
            return await _context.Evaluations
                .Include(v => v.Class)
                    .ThenInclude(s => s.SchoolYear)
                    .ThenInclude(s => s.School)
                .Where(v => v.Class.SchoolYear.SchoolId == schoolId)
                .ToListAsync();
        }

        public async Task<List<EvaluationRanking>> GetEvaluationRankingsByYear(int schoolId, short year)
        {
            var rankings = await _context.Evaluations
                .Where(e => e.Class.SchoolYear.SchoolId == schoolId && e.Class.SchoolYear.Year == year)
                .GroupBy(e => e.ClassId)
                .Select(g => new EvaluationRanking
                {
                    ClassId = g.Key,
                    ClassName = g.FirstOrDefault().Class.Name,
                    TotalPoints = g.Sum(e => e.Points ?? 0)
                })
                .OrderByDescending(r => r.TotalPoints)
                .ToListAsync();

            return CalculateRank(rankings);
        }

        public async Task<List<EvaluationRanking>> GetEvaluationRankingsByMonth(int schoolId, short year, int month)
        {
            var rankings = await _context.Evaluations
                .Where(e => e.Class.SchoolYear.SchoolId == schoolId &&
                            e.Class.SchoolYear.Year == year &&
                            e.From.Month == month)
                .GroupBy(e => e.ClassId)
                .Select(g => new EvaluationRanking
                {
                    ClassId = g.Key,
                    ClassName = g.FirstOrDefault().Class.Name,
                    TotalPoints = g.Sum(e => e.Points ?? 0)
                })
                .OrderByDescending(r => r.TotalPoints)
                .ToListAsync();

            return CalculateRank(rankings);
        }

        public async Task<List<EvaluationRanking>> GetEvaluationRankingsByWeek(int schoolId, short year, int month, int week)
        {
            var schoolYear = await _context.SchoolYears
                .FirstOrDefaultAsync(sy => sy.SchoolId == schoolId && sy.Year == year);

            if (schoolYear == null)
                throw new Exception("SchoolYear không tồn tại.");

            var startDate = new DateTime(year, month, 1);
            var firstDayOfWeek = startDate.AddDays((week - 1) * 7);

            var endDate = firstDayOfWeek.AddDays(6);
            if (endDate > schoolYear.EndDate)
            {
                endDate = schoolYear.EndDate;
            }

            var rankings = await _context.Evaluations
                .Where(e => e.Class.SchoolYear.SchoolId == schoolId &&
                            e.Class.SchoolYear.Year == year &&
                            e.From >= firstDayOfWeek &&
                            e.From <= endDate)
                .GroupBy(e => e.ClassId)
                .Select(g => new EvaluationRanking
                {
                    ClassId = g.Key,
                    ClassName = g.FirstOrDefault().Class.Name,
                    TotalPoints = g.Sum(e => e.Points ?? 0)
                })
                .OrderByDescending(r => r.TotalPoints)
                .ToListAsync();

            return CalculateRank(rankings);
        }



        private List<EvaluationRanking> CalculateRank(List<EvaluationRanking> rankings)
        {
            int rank = 1;
            int prevPoints = -1;
            int sameRankCount = 0;

            foreach (var ranking in rankings)
            {
                if (ranking.TotalPoints == prevPoints)
                {
                    ranking.Rank = rank - sameRankCount;
                    sameRankCount++;
                }
                else
                {
                    rank += sameRankCount;
                    ranking.Rank = rank++;
                    sameRankCount = 0;
                }
                prevPoints = ranking.TotalPoints;
            }
            return rankings;
        }


    }
}
