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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructures.Repository
{
    public class ViolationConfigRepository : GenericRepository<ViolationConfig>, IViolationConfigRepository
    {
        public ViolationConfigRepository(SchoolRulesContext context): base(context) { }

        public async Task<List<ViolationConfig>> GetAllViolationConfigs()
        {
            var violationConnfig = await _context.ViolationConfigs
                .Include(e => e.ViolationType)
                .ToListAsync();
            return violationConnfig;
        }

        public async Task<ViolationConfig> GetViolationConfigById(int id)
        {
            return _context.ViolationConfigs
                .Include(e => e.ViolationType)
                .FirstOrDefault(s => s.ViolationConfigId == id);
        }

        public async Task<List<ViolationConfig>> GetViolationConfigsBySchoolId(int schoolId)
        {
            return await _context.ViolationConfigs
                .Include(e => e.ViolationType)
                    .ThenInclude(e => e.ViolationGroup)
                .Where(ed => ed.ViolationType.ViolationGroup.SchoolId == schoolId)
                .ToListAsync();
        }

        public async Task<ViolationConfig> GetConfigByViolationTypeId(int violationTypeId)
        {
            return await _context.ViolationConfigs
                .FirstOrDefaultAsync(vc => vc.ViolationTypeId == violationTypeId && vc.Status == ViolationConfigStatusEnums.ACTIVE.ToString());
        }

        public async Task<List<ViolationConfig>> GetActiveViolationConfigsBySchoolId(int schoolId)
        {
            return await _context.ViolationConfigs
                .Include(e => e.ViolationType)
                    .ThenInclude(e => e.ViolationGroup)
                .Where(v => v.ViolationType.ViolationGroup.SchoolId == schoolId && v.Status == ViolationConfigStatusEnums.ACTIVE.ToString())
                .ToListAsync();
        }
    }
}
