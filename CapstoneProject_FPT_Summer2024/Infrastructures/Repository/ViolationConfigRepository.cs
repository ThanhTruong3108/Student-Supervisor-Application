using Domain.Entity;
using Infrastructures.Interfaces;
using Infrastructures.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructures.Repository
{
    public class ViolationConfigRepository : GenericRepository<ViolationConfig>, IViolationConfigRepository
    {
        public ViolationConfigRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<ViolationConfig>> GetAllViolationConfigs()
        {
            var violationConnfig = await _context.ViolationConfigs
                .Include(e => e.Evaluation)
                .Include(e => e.ViolationType)
                .ToListAsync();
            return violationConnfig;
        }

        public async Task<ViolationConfig> GetViolationConfigById(int id)
        {
            return _context.ViolationConfigs
                .Include(e => e.Evaluation)
                .Include(e => e.ViolationType)
                .FirstOrDefault(s => s.ViolationConfigId == id);
        }

        public async Task<List<ViolationConfig>> SearchViolationConfigs(int? evaluationId, int? vioTypeId, string? code, string? name)
        {
            var query = _context.ViolationConfigs.AsQueryable();

            if (evaluationId.HasValue)
            {
                query = query.Where(p => p.EvaluationId == evaluationId.Value);
            }

            if (vioTypeId.HasValue)
            {
                query = query.Where(p => p.ViolationTypeId == vioTypeId.Value);
            }

            if (!string.IsNullOrEmpty(code))
            {
                query = query.Where(p => p.Code.Contains(code));
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            return await query
                .Include(e => e.Evaluation)
                .Include(e => e.ViolationType)
                .ToListAsync();
        }
    }
}
