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
    public class EvaluationRepository : GenericRepository<Evaluation>, IEvaluationRepository
    {
        public EvaluationRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<Evaluation>> GetAllEvaluations()
        {
            return await _context.Evaluations
                .Include(v => v.Class)
                .ToListAsync();
        }

        public async Task<Evaluation> GetEvaluationById(int id)
        {
            return await _context.Evaluations
                .Include(v => v.Class)
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

        public async Task<List<Evaluation>> GetEvaluationsBySchoolId(int schoolId)
        {
            return await _context.Evaluations
                .Include(v => v.Class)
                    .ThenInclude(s => s.SchoolYear)
                    .ThenInclude(s => s.School)
                .Where(v => v.Class.SchoolYear.SchoolId == schoolId)
                .ToListAsync();
        }
    }
}
