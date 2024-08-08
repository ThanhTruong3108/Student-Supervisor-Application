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

        // Xếp hạng
        public async Task<List<ClassRankResponse>> GetEvaluationRanks(int schoolId, DateTime fromDate, DateTime toDate)
        {
            var evaluations = await _context.Evaluations
                .Include(e => e.Class)
                .Where(e => e.From >= fromDate && e.To <= toDate && e.Class.SchoolYear.SchoolId == schoolId)
                .GroupBy(e => e.ClassId)
                .Select(group => new ClassRankResponse
                {
                    ClassId = group.Key,
                    ClassName = group.FirstOrDefault().Class.Name,
                    TotalPoints = group.Sum(e => e.Points),
                    Rank = 0
                })
                .OrderByDescending(c => c.TotalPoints)
                .ToListAsync();

            return evaluations;
        }

    }
}
